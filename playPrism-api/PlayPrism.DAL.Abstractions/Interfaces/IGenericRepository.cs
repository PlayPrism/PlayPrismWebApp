// <copyright file="IGenericRepository.cs" company="PlayPrism">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace PlayPrism.DAL.Abstractions.Interfaces;

using Core.Models;
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
    /// Asynchronously returns TEntity from database requested by id parameter and category.
    /// </summary>
    /// <param name="predicate">Predicate that accepts equation and returns bool value that indicates if the expression is true.</param>
    /// <param name="cancellationToken">Cancellation token to cancel task.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task<TEntity> GetByIdAndCategoryAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously returns IEnumerable of entities that pass the predicate condition.
    /// </summary>
    /// <param name="predicate">Predicate that accepts equation an return bool value that indicates if the expression is true.</param>
    /// <param name="cancellationToken">Cancellation token to cancel task.</param>
    /// <param name="selector">Selector value that accepts the expression that defines what tables should be joined ot the requested object.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task<IList<TEntity>> GetByConditionAsync(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TEntity>> selector = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously iterates through predicates and than returns page according to pageInfo.
    /// </summary>
    /// <param name="predicates">List of predicates.</param>
    /// <param name="pageInfo">Pagination properties.</param>
    /// <param name="selector">Selector to load corresponding data from database.</param>
    /// <param name="cancellationToken">Cancellation token to cancel task.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task<IList<TEntity>> GetPageWithMultiplePredicatesAsync(
        IEnumerable<Expression<Func<TEntity, bool>>> predicates,
        PageInfo pageInfo,
        Expression<Func<TEntity, TEntity>> selector,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously checks if enitity with predicate value exists in database.
    /// </summary>
    /// <param name="predicate">Predicate that accepts equation an return bool value that indicates if the expression is true.</param>
    /// <param name="cancellationToken">Cancellation token to cancel task.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously adds object of TEntity to database.
    /// </summary>
    /// <param name="obj">TEntity obj param.</param>
    /// <param name="cancellationToken">Cancellation token to cancel task.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    Task AddAsync(TEntity obj, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates TEntity, passed as parameter.
    /// </summary>
    /// <param name="obj">TEntity object to be updated.</param>
    void Update(TEntity obj);

    /// <summary>
    /// Removes entity from database.
    /// </summary>
    /// <param name="obj">TEntity obj param.</param>
    void Delete(TEntity obj);
}