﻿using Shopway.Application.Abstractions;
using Shopway.Domain.Common.DataProcessing;
using Shopway.Domain.Products.DataProcessing.Sorting;
using Shopway.Domain.Products.DataProcessing.Filtering;

namespace Shopway.Application.Features.Products.Queries.DynamicOffsetProductQuery;

internal sealed class ProductOffsetPageDynamicQueryValidator : OffsetPageQueryValidator<ProductOffsetPageDynamicQuery, ProductResponse, ProductDynamicFilter, ProductDynamicSortBy, OffsetPage>
{
    public ProductOffsetPageDynamicQueryValidator()
        : base()
    {
    }
}