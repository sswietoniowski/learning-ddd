﻿using Shopway.Domain.Common;
using Shopway.Domain.Utilities;
using System.Linq.Dynamic.Core;
using Shopway.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Shopway.Domain.BaseTypes;

namespace Shopway.Persistence.Utilities;

public static class QueryableUtilities
{
    /// <summary>
    /// Get page items and total count
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TEntityId">Entity id type</typeparam>
    /// <typeparam name="TResponse">Response type to map to</typeparam>
    /// <param name="queryable">Queryable</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Tuple of  items and total count</returns>
    public static async Task<(IList<TResponse> Responses, int TotalCount)> PageAsync<TResponse>
    (
        this IQueryable<TResponse> queryable,
        IPage page,
        CancellationToken cancellationToken
    )
    {
        if (page is null)
        {
            throw new ArgumentNullException($"Page is null");
        }

        var totalCount = await queryable.CountAsync(cancellationToken);

        var responses = await queryable
            .Page(page)
            .ToListAsync(cancellationToken);

        return (responses, totalCount);
    }

    internal static IQueryable<TResponse> Sort<TResponse>
    (
        this IQueryable<TResponse> queryable,
        IEnumerable<SortByEntry> sortProperties
    )
    {
        var sortedProperties = sortProperties
            .Distinct()
            .OrderBy(x => x.SortPriority);

        var firstElement = sortedProperties.FirstOrDefault();

        if (firstElement is null)
        {
            return queryable;
        }

        queryable = queryable
            .SortByValueObjectName(firstElement.SortDirection, firstElement.PropertyName);

        foreach (var item in sortedProperties.Skip(1))
        {
            queryable = ((IOrderedQueryable<TResponse>)queryable)
                .ThenSortByValueObjectName(item.SortDirection, item.PropertyName);
        }

        return queryable;
    }

    public static async Task<bool> AnyAsync<TEntity, TEntityId>
    (
        this IQueryable<TEntity> queryable,
        TEntityId entityId,
        CancellationToken cancellationToken
    )
        where TEntity : Entity<TEntityId>
        where TEntityId : struct, IEntityId
    {
        return await queryable
           .Where(entity => entity.Id.Equals(entityId))
           .AnyAsync(cancellationToken);
    }

    /// <summary>
    /// Using EF.Property
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TEntityId"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="entityId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<bool> AnyAsyncUsingEFProperty<TEntity, TEntityId>
    (
        this IQueryable<TEntity> queryable,
        TEntityId entityId,
        CancellationToken cancellationToken
    )
        where TEntity : IEntity
        where TEntityId : struct, IEntityId
    {
        return await queryable
           .Where(entity => EF.Property<TEntityId>(entity, IEntityId.Id).Equals(entityId))
           .AnyAsync(cancellationToken);
    }

    /// <summary>
    /// Using dynamic linq
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="entityId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<bool> AnyAsyncUsingDynamicLinq<TEntity>
    (
        this IQueryable<TEntity> queryable,
        IEntityId entityId,
        CancellationToken cancellationToken
    )
        where TEntity : IEntity
    {
        return await queryable
           .Where($"{IEntityId.Id} == \"{entityId.Value}\"")
           .AnyAsync(cancellationToken);
    }
}