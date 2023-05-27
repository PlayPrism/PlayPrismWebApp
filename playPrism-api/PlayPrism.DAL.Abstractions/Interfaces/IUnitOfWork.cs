// <copyright file="IUnitOfWork.cs" company="PlayPrism">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Microsoft.EntityFrameworkCore.Storage;

namespace PlayPrism.DAL.Abstractions.Interfaces;

using Core.Domain;

/// <summary>
/// Interface for UnitOfWork design pattern that works with business transactions.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Gets products repository.
    /// </summary>
    IGenericRepository<Product> Products { get; }

    /// <summary>
    /// Gets orders repository.
    /// </summary>
    IGenericRepository<Order> Orders { get; }

    /// <summary>
    /// Gets order items repository.
    /// </summary>
    IGenericRepository<OrderItem> OrderItems { get; }

    /// <summary>
    /// Gets payment methods repository.
    /// </summary>
    IGenericRepository<PaymentMethod> PaymentMethods { get; }

    /// <summary>
    /// Gets product categories repository.
    /// </summary>
    IGenericRepository<ProductCategory> Categories { get; }

    /// <summary>
    /// Gets product configuration repository.
    /// </summary>
    IGenericRepository<ProductConfiguration> ProductConfigurations { get; }

    /// <summary>
    /// Gets product items repository.
    /// </summary>
    IGenericRepository<ProductItem> ProductItems { get; }

    /// <summary>
    /// Gets users repository.
    /// </summary>
    IGenericRepository<UserProfile> Users { get; }

    /// <summary>
    /// Gets reviews repository.
    /// </summary>
    IGenericRepository<UserReview> Reviews { get; }

    /// <summary>
    /// Gets variations repository.
    /// </summary>
    IGenericRepository<VariationOption> Variations { get; }

    /// <summary>
    /// Asynchronously begins database transaction.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Obsolete]
    Task BeginTransactionAsync();
    
    /// <summary>
    /// Asynchronously commits changes made in transaction.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Obsolete]
    Task CommitAsync();
    
    /// <summary>
    /// Asynchronously rollbacks changes in transaction.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Obsolete]
    Task RollbackAsync();
    
    /// <summary>
    /// Asynchronously saves changes made in transaction to database.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    
   
    Task SaveAsync();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Task<IDbContextTransaction> CreateTransactionAsync();

    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Task CommitTransactionAsync();


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Task RollbackTransactionAsync();

}