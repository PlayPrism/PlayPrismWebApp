// <copyright file="IUnitOfWork.cs" company="PlayPrism">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace PlayPrism.DAL.Abstractions.Interfaces;

/// <summary>
/// Interface for UnitOfWork design pattern that works with business transactions.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Asynchronously begins database transaction.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task BeginTransactionAsync();

    /// <summary>
    /// Asynchronously commits changes made in transaction.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task CommitAsync();

    /// <summary>
    /// Asynchronously rollbacks changes in transaction.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task RollbackAsync();

    /// <summary>
    /// Asynchronously saves changes made in transaction to database.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task SaveAsync();
}