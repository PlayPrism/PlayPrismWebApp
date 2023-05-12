// <copyright file="GenericRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace PlayPrism.DAL.Repository;

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Core.Domain;
using Core.Models;
using Abstractions.Interfaces;

/// <inheritdoc />
public class GenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly PlayPrismContext _appContext;
    private readonly DbSet<TEntity> _dbSet;

    /// <summary>
    /// Initializes a new instance of the <see cref="GenericRepository{TEntity}"/> class.
    /// </summary>
    /// <param name="appContext">PlayPrismContext database context.</param>
    public GenericRepository(PlayPrismContext appContext)
    {
        this._appContext = appContext;
        this._dbSet = appContext.Set<TEntity>();
    }

    /// <inheritdoc />
    public async Task<TEntity> GetByIdAsync(Guid id)
    {
        return await this._dbSet.FindAsync(id);
    }

    /// <inheritdoc />
    public async Task<TEntity> GetByIdAndCategoryAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = this._dbSet;

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
        IQueryable<TEntity> query = this._dbSet;

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

        IQueryable<TEntity> query = this._dbSet.AsQueryable();

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
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await this._dbSet.AnyAsync(predicate, cancellationToken: cancellationToken);
    }

    /// <inheritdoc />
    public async Task AddAsync(TEntity obj, CancellationToken cancellationToken = default)
    {
        await this._dbSet.AddAsync(obj, cancellationToken: cancellationToken);
    }

    /// <inheritdoc />
    public void Update(TEntity obj)
    {
        this._dbSet.Update(obj);
    }

    /// <inheritdoc />
    public void Delete(TEntity obj)
    {
        try
        {
            this._dbSet.Remove(obj);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}