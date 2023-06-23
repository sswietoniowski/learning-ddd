﻿using Shopway.Domain.Entities;
using Shopway.Domain.Utilities;
using Shopway.Domain.Abstractions.Common;

namespace Shopway.Persistence.Specifications.Products.Filtering;

public sealed record ProductDictionaryStaticFilter : IStaticFilter<Product>
{
    public string? LikeQuery { get; init; }
    private bool ByLikeQuery => LikeQuery.NotNullOrEmptyOrWhiteSpace();

    public IQueryable<Product> Apply(IQueryable<Product> queryable)
    {
        return queryable
            .Filter(ByLikeQuery, product => ((string)(object)product.ProductName).Contains(LikeQuery!)
                                         || ((string)(object)product.Revision).Contains(LikeQuery!));
    }
}