﻿using Shopway.Application.Abstractions.CQRS;
using Shopway.Domain.StronglyTypedIds;

namespace Shopway.Application.Products.Queries.GetProductById;

public sealed record GetProductByIdQuery(ProductId Id) : IQuery<ProductResponse>;