﻿using Shopway.Domain.EntityIds;
using Shopway.Application.Abstractions.CQRS;
using static Shopway.Application.Features.Products.Commands.UpdateProduct.UpdateProductCommand;

namespace Shopway.Application.Features.Products.Commands.UpdateProduct;

public sealed record UpdateProductCommand
(
    ProductId Id,
    UpdateRequestBody Body

) : ICommand<UpdateProductResponse>
{
    public sealed record UpdateRequestBody(decimal Price);
}