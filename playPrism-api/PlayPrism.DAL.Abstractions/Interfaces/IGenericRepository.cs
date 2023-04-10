// <copyright file="IGenericRepository.cs" company="PlayPrism">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace PlayPrism.DAL.Abstractions.Interfaces;

using System.Linq.Expressions;
using Core.Domain;

/// <summary>
/// IGeneric repository interface.
/// </summary>
/// <typeparam name="TEntity">Param that represents the entity of database table.</typeparam>
public interface IGenericRepository<TEntity>
    where TEntity : BaseEntity
{
    /// <summary>
    /// Asynchronously returns TEntity from database requested by id parameter.
    /// </summary>
    /// <param name="id">Parameter that represents id of the entity ib database.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task<TEntity> GetByIdAsync(Guid id);

    /// <summary>
    /// Asynchronously returns IEnumerable of entities that pass the predicate condition.
    /// </summary>
    /// <param name="predicate">Predicate that accepts equation an return bool value that indicates if the expression is true.</param>
    /// <param name="selector">Selector value that accepts the expression that defines what tables should be joined ot the requested object.</param>
    /// <typeparam name="TResult">Represents the object with loaded dependencies from other tables.</typeparam>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task<IEnumerable<TResult>> GetByConditionAsync<TResult>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TResult>> selector = null);

    /// <summary>
    /// Asynchronously adds object of TEntity to database.
    /// </summary>
    /// <param name="obj">TEntity obj param.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    Task AddAsync(TEntity obj);

    /// <summary>
    /// Asynchronously removes entity from database.
    /// </summary>
    /// <param name="obj">TEntity obj param.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task<bool> DeleteAsync(TEntity obj);
}