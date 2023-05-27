// <copyright file="UnitOfWork.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace PlayPrism.DAL.Repository;

using Core.Domain;
using Microsoft.EntityFrameworkCore.Storage;
using Abstractions.Interfaces;

/// <summary>
/// Implementation of IUnitOfWork.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly PlayPrismContext _context;
    private readonly Lazy<IGenericRepository<Product>> _productRepository;
    private readonly Lazy<IGenericRepository<Order>> _orderRepository;
    private readonly Lazy<IGenericRepository<OrderItem>> _orderItemsRepository;
    private readonly Lazy<IGenericRepository<PaymentMethod>> _paymentRepository;
    private readonly Lazy<IGenericRepository<ProductCategory>> _productCategoryRepository;
    private readonly Lazy<IGenericRepository<ProductConfiguration>> _productConfigRepository;
    private readonly Lazy<IGenericRepository<ProductItem>> _productItemRepository;
    private readonly Lazy<IGenericRepository<UserProfile>> _userRepository;
    private readonly Lazy<IGenericRepository<UserReview>> _reviewRepository;
    private readonly Lazy<IGenericRepository<RefreshToken>> _refreshTokenRepository;
    private readonly Lazy<IGenericRepository<VariationOption>> _variationRepository;
    private IDbContextTransaction _transactionObj;


    /// <summary>
    /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
    /// </summary>
    /// <param name="context">PlayPrism database context.</param>
    /// <param name="productRepository">Repository for product.</param>
    /// <param name="orderRepository">Repository for order.</param>
    /// <param name="orderItemsRepository">Repository for order items.</param>
    /// <param name="paymentRepository">Repository for payment method.</param>
    /// <param name="productCategoryRepository">Repository for product category.</param>
    /// <param name="productConfigRepository">Repository for product config.</param>
    /// <param name="productItemRepository">Repository for product item.</param>
    /// <param name="userRepository">Repository for user profile.</param>
    /// <param name="reviewRepository">Repository for user reviews.</param>
    /// <param name="variationRepository">Repository for product variation.</param>
    public UnitOfWork(
        PlayPrismContext context,
        Lazy<IGenericRepository<Product>> productRepository,
        Lazy<IGenericRepository<Order>> orderRepository,
        Lazy<IGenericRepository<OrderItem>> orderItemsRepository,
        Lazy<IGenericRepository<PaymentMethod>> paymentRepository,
        Lazy<IGenericRepository<ProductCategory>> productCategoryRepository,
        Lazy<IGenericRepository<ProductConfiguration>> productConfigRepository,
        Lazy<IGenericRepository<ProductItem>> productItemRepository,
        Lazy<IGenericRepository<UserProfile>> userRepository,
        Lazy<IGenericRepository<UserReview>> reviewRepository,
        Lazy<IGenericRepository<RefreshToken>> refreshTokenRepository,
        Lazy<IGenericRepository<VariationOption>> variationRepository)
    {
        _context = context;
        _productRepository = productRepository;
        _orderRepository = orderRepository;
        _orderItemsRepository = orderItemsRepository;
        _paymentRepository = paymentRepository;
        _productCategoryRepository = productCategoryRepository;
        _productConfigRepository = productConfigRepository;
        _productItemRepository = productItemRepository;
        _userRepository = userRepository;
        _reviewRepository = reviewRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _variationRepository = variationRepository;
    }

    /// <inheritdoc />
    public IGenericRepository<Product> Products => _productRepository.Value;

    /// <inheritdoc />
    public IGenericRepository<Order> Orders => _orderRepository.Value;

    /// <inheritdoc />
    public IGenericRepository<OrderItem> OrderItems => _orderItemsRepository.Value;

    /// <inheritdoc />
    public IGenericRepository<PaymentMethod> PaymentMethods => _paymentRepository.Value;

    /// <inheritdoc />
    public IGenericRepository<ProductCategory> Categories => _productCategoryRepository.Value;

    /// <inheritdoc />
    public IGenericRepository<ProductConfiguration> ProductConfigurations => _productConfigRepository.Value;

    /// <inheritdoc />
    public IGenericRepository<ProductItem> ProductItems => _productItemRepository.Value;

    /// <inheritdoc />
    public IGenericRepository<UserProfile> Users => _userRepository.Value;

    /// <inheritdoc />
    public IGenericRepository<UserReview> Reviews => _reviewRepository.Value;

    /// <inheritdoc />
    public IGenericRepository<VariationOption> Variations => _variationRepository.Value;
    
    /// <inheritdoc />
    public IGenericRepository<RefreshToken> RefreshTokens => _refreshTokenRepository.Value;
    


    /// <inheritdoc />
    [Obsolete]
    public async Task BeginTransactionAsync()
    {
        _transactionObj = await _context.Database.BeginTransactionAsync();
    }
    
    /// <inheritdoc />
    [Obsolete]
    public async Task CommitAsync()
    {
        try
        {
            //await _context.SaveChangesAsync();
            
            if(_transactionObj != null)
                await _transactionObj.CommitAsync();
        }
        catch
        {
            await RollbackAsync();
        }
    }
    
    /// <inheritdoc />
    [Obsolete]
    public async Task RollbackAsync()
    {
        await _transactionObj.RollbackAsync();
        _transactionObj.Dispose();
    }
    
    /// <inheritdoc />
    
    
    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
        
        // if(_transactionObj != null)
        //     _transactionObj.Dispose();
    }
    
    
    /// <inheritdoc />
    public Task<IDbContextTransaction> CreateTransactionAsync()
    {    return _context.Database.BeginTransactionAsync();
    }
    
    /// <inheritdoc />
    public Task CommitTransactionAsync()
    {    return _context.Database.CommitTransactionAsync();
    }
    
    /// <inheritdoc />
    public Task RollbackTransactionAsync()
    {    return _context.Database.RollbackTransactionAsync();
    }
}