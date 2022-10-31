﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shopway.Application.Products.Commands.CreateProduct;
using Shopway.Application.Products.Queries.GetProductById;
using Shopway.Domain.Results;
using Shopway.Presentation.Abstractions;
using Shopway.Presentation.Requests.Products;

namespace Shopway.Presentation.Controllers;

[Route("api/product")]
public sealed class ProductController : ApiController
{
    public ProductController(ISender sender)
        : base(sender)
    {
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetProductById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetProductByIdQuery(id);

        Result<ProductResponse> response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(
        [FromBody] CreateProductRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateProductCommand(
            request.ProductName,
            request.Price,
            request.UomCode,
            request.Revision);

        Result<Guid> result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return CreatedAtAction(
            nameof(GetProductById),
            new { id = result.Value },
            result.Value);
    }

    [HttpGet()]
    public IActionResult Ping()
    {
        return Ok("Pong");
    }
}