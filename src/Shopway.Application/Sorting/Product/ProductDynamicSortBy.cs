﻿using Shopway.Domain.Common;
using Shopway.Domain.Entities;
using Shopway.Domain.Utilities;
using Shopway.Domain.Abstractions.Common;
using static Shopway.Domain.Constants.Constants.Sorting.Product;

namespace Shopway.Application.Sorting.Products;

public sealed record ProductDynamicSortBy : IDynamicSortBy<Product>
{
    public static IReadOnlyCollection<string> AllowedSortProperties { get; } = AllowedProductSortProperties;

    public required IList<SortByEntry> SortProperties { get; init; }

    public IQueryable<Product> Apply(IQueryable<Product> queryable)
    {
        return queryable.Sort(SortProperties);
    }
}