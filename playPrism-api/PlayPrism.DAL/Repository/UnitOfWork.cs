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
        Lazy<IGenericRepository<VariationOption>> variationRepository)
    {
        this._context = context;
        this._productRepository = productRepository;
        this._orderRepository = orderRepository;
        this._orderItemsRepository = orderItemsRepository;
        this._paymentRepository = paymentRepository;
        this._productCategoryRepository = productCategoryRepository;
        this._productConfigRepository = productConfigRepository;
        this._productItemRepository = productItemRepository;
        this._userRepository = userRepository;
        this._reviewRepository = reviewRepository;
        this._variationRepository = variationRepository;
    }

    /// <inheritdoc />
    public IGenericRepository<Product> Products => this._productRepository.Value;

    /// <inheritdoc />
    public IGenericRepository<Order> Orders => this._orderRepository.Value;

    /// <inheritdoc />
    public IGenericRepository<OrderItem> OrderItems => this._orderItemsRepository.Value;

    /// <inheritdoc />
    public IGenericRepository<PaymentMethod> PaymentMethods => this._paymentRepository.Value;

    /// <inheritdoc />
    public IGenericRepository<ProductCategory> Categories => this._productCategoryRepository.Value;

    /// <inheritdoc />
    public IGenericRepository<ProductConfiguration> ProductConfigurations => this._productConfigRepository.Value;

    /// <inheritdoc />
    public IGenericRepository<ProductItem> ProductItems => this._productItemRepository.Value;

    /// <inheritdoc />
    public IGenericRepository<UserProfile> Users => this._userRepository.Value;

    /// <inheritdoc />
    public IGenericRepository<UserReview> Reviews => this._reviewRepository.Value;

    /// <inheritdoc />
    public IGenericRepository<VariationOption> Variations => this._variationRepository.Value;


    /// <inheritdoc />
    public async Task BeginTransactionAsync()
    {
        this._transactionObj = await this._context.Database.BeginTransactionAsync();
    }

    /// <inheritdoc />
    public async Task CommitAsync()
    {
        await this._context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task RollbackAsync()
    {
        await this._transactionObj.RollbackAsync();
        await this._transactionObj.DisposeAsync();
    }

    /// <inheritdoc />
    public async Task SaveAsync()
    {
        await this._context.SaveChangesAsync();
        await this._transactionObj.DisposeAsync();
    }
}