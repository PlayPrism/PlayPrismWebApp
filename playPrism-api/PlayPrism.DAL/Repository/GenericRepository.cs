// <copyright file="GenericRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using PlayPrism.Core.Domain.Filters;

namespace PlayPrism.DAL.Repository;

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Core.Domain;
using Abstractions.Interfaces;

/// <inheritdoc />
public class GenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly DbSet<TEntity> _dbSet;

    /// <summary>
    /// Initializes a new instance of the <see cref="GenericRepository{TEntity}"/> class.
    /// </summary>
    /// <param name="appContext">PlayPrismContext database context.</param>
    public GenericRepository(PlayPrismContext appContext)
    {
        _dbSet = appContext.Set<TEntity>();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
       var entities =  await _dbSet.ToListAsync();
       return entities;
    }

    /// <inheritdoc />
    public async Task<TEntity> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    /// <inheritdoc />
    public async Task<TEntity> GetByIdAndCategoryAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = _dbSet;

        query = query
            .Where(predicate);

        return await query.FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IList<TEntity>> GetByConditionAsync(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TEntity>> selector = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = _dbSet;

        if (selector != null)
        {
            query = query
                .Where(predicate)
                .Select(selector);
        }
        else
        {
            query = query.Where(predicate);
        }

        return await query.ToListAsync(cancellationToken: cancellationToken);
    }


    /// <inheritdoc />
    public async Task<IList<TEntity>> GetPageWithMultiplePredicatesAsync(
        IEnumerable<Expression<Func<TEntity, bool>>> predicates,
        PageInfo pageInfo,
        Expression<Func<TEntity, TEntity>> selector,
        CancellationToken cancellationToken = default)
    {
        var skip = pageInfo.Size * (pageInfo.Number - 1);

        IQueryable<TEntity> query = _dbSet.AsQueryable();

        if (selector != null)
        {
            query = query.Select(selector);
        }

        foreach (var predicate in predicates)
        {
            query = query.Where(predicate).Skip(skip).Take(pageInfo.Size);
        }

        return await query.ToListAsync(cancellationToken: cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> ExistAsync(
        Expression<Func<TEntity, bool>> predicate = null,
        CancellationToken cancellationToken = default)
    {
        if (predicate is not null)
        {
            return await _dbSet.AnyAsync(predicate, cancellationToken);

        }
        
        return await _dbSet.AnyAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task AddAsync(TEntity obj, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(obj, cancellationToken: cancellationToken);
    }
    
    /// <inheritdoc />
    public async Task AddManyAsync(IEnumerable<TEntity> obj, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddRangeAsync(obj, cancellationToken: cancellationToken);
    }

    /// <inheritdoc />
    public void Update(TEntity obj)
    {
        _dbSet.Update(obj);
    }

    /// <inheritdoc />
    public void Delete(TEntity obj)
    {
        try
        {
            _dbSet.Remove(obj);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}