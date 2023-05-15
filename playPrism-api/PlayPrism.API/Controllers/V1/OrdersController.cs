using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayPrism.Contracts.Extensions;
using PlayPrism.Contracts.V1.Requests.Orders;
using PlayPrism.Contracts.V1.Responses.Api;
using PlayPrism.Core.Domain;
using PlayPrism.Core.Enums;
using PlayPrism.DAL.Abstractions.Interfaces;
using Rise.Contracts.CsvImportData.Records;
using Rise.Contracts.CsvImportData.Response;
using Rise.Contracts.Requests.Goods;
using Rise.Contracts.Requests.Orders;
using Rise.Contracts.Responses.Orders;
using Rise.Core.Domain.Email;
using Rise.Core.External.NovaPoshta;

namespace PlayPrism.API.Controllers.V1;

/// <summary>
///     Controller for order management
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly ILogger<OrdersController> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderManager _orderManager;

    /// <summary>
    ///     Initializes a new instance of the <see cref="OrdersController" /> class.
    /// </summary>
    /// <param name="mapper">The <see cref="IMapper" /></param>
    /// <param name="unitOfWork">The <see cref="IUnitOfWork" /></param>
    /// <param name="helperService">The <see cref="IHelperService" /></param>
    /// <param name="logger">The <see cref="ILogger{TCategoryName}" /></param>
    /// <param name="novaPoshtaService">Nova poshta service</param>
    /// <param name="emailService">Email service</param>
    /// <param name="orderManager">The <see cref="IOrderManager"/></param>
    /// <param name="backgroundJobClient">The <see cref="IBackgroundJobClient"/></param>
    /// <param name="importManager">Import manager</param>
    public OrdersController(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IHelperService helperService,
        ILogger<OrdersController> logger,
        INovaPoshtaService novaPoshtaService,
        IEmailService emailService,
        IOrderManager orderManager, IBackgroundJobClient backgroundJobClient,
        IImportManager<ImportResponse<OrderRecord>> importManager)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _orderManager = orderManager;
    }

    /// <summary>
    ///     Get order by id
    /// </summary>
    /// <param name="id">Order id.</param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ApiListResponse<AuditEventResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserOrders([FromRoute] int id)
    {
        var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
        if (order == null)
        {
            return NotFound();
        }

        var result = _mapper.Map<ApiListResponse<AuditEventResponse>>(events);

        return Ok(result);
    }
    
    //TODO: Write Get orders by user id
    // [HttpGet("{id:int}/orders")]

    /// <summary>
    ///     Create new order
    /// </summary>
    /// <param name="request">A <see cref="AddOrderRequest" /></param>
    /// <returns>A <see cref="OrderResponse" /></returns>
    [HttpPost]
    [ProducesResponseType(typeof(OrderResponse), 200)]
    public async Task<IActionResult> CreateOrder([FromBody] AddOrderRequest request)
    {
        _logger.LogInformation("User is trying to create order");

        var orderItemsIds = request.OrderItems.Select(x => x.GoodId).Distinct().ToArray();
        var orderProducts = (await _unitOfWork.GoodRepository.GetRangeByIdsAsync(orderItemsIds)).ToArray();
        if (orderProducts.Length != orderItemsIds.Length)
        {
            var actualOrderItemsIds = orderProducts.Select(x => x.Id);
            var expectedOrderItems = orderItemsIds.Where(x => !actualOrderItemsIds.Contains(x));
            var expectedResult = string.Join(" ", expectedOrderItems);
            _logger.LogDebug("Order items with current ids are not exist: {OrderItemId}", expectedResult);
            return NotFound($"Order items with current ids are not exist: {expectedResult}".ToErrorResponse());
        }

        var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
        var balance = await _unitOfWork.TransactionRepository.GetTotalAmountByUserIdAsync(user.Id);
        var totalAmount = request.OrderItems.Sum(x =>
        {
            return x.Quantity * orderProducts.First(item => item.Id == x.GoodId).Price;
        });
        if (totalAmount > balance)
        {
            _logger.LogWarning("User tried to create order but balance is not enough");
            return BadRequest("User have not enough points".ToErrorResponse());
        }

        var response = new List<OrderResponse>();
        
       // await using var t = await _unitOfWork.CreateTransactionAsync();
       //TODO: Add orderItem to order
        foreach (var orderItem in request.OrderItems)
        {
            var orderItemGood = orderProducts.First(x => x.Id == orderItem.GoodId);
            var order = _mapper.Map<Order>(request);
            order.UserId = userId;
            order.OrderItem = _mapper.Map<OrderItem>(orderItem);
            order.OrderItem.Price = orderItemGood.Price;
            var totalPrice = orderItemGood.Price * order.OrderItem.Quantity;

            if (order.OrderDetails is not null)
            {
                order.OrderDetails.City = city?.Description;
                order.OrderDetails.Warehouse = warehouse?.Description;
            }

                //TODO: There will be payment operation with service
            try
            {

                var transaction = new Transaction
                {
                    Amount = -totalPrice,
                    UserId = user.Id,
                    SourceType = SourceType.Purchase,
                    OldBalance = balance,
                    NewBalance = balance - totalPrice
                };
                transaction = await _unitOfWork.TransactionRepository.AddAsync(transaction);
                _logger.LogInformation("Transaction with id {TransactionId} created", transaction.Id);

                order.TransactionId = transaction.Id;
                order = await _unitOfWork.OrderRepository.AddAsync(order);
                _logger.LogInformation("Order with id {OrderId} created and {TransactionId}", order.Id,
                    transaction.Id);
                response.Add(_mapper.Map<OrderResponse>(order));
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(e, "Error while creating order");
                throw;
            }
        }
        
        await _unitOfWork.CommitTransactionAsync();
        return Ok(response.ToApiResponse());
    }
}