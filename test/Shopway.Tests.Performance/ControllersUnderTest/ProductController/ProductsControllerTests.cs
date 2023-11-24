﻿using Xunit.Abstractions;
using NBomber.Contracts.Stats;
using Microsoft.EntityFrameworkCore;
using static System.Threading.CancellationToken;
using static Shopway.Tests.Performance.Constants.Constants;
using static Shopway.Tests.Performance.Constants.Constants.OutputHelper;
using Shopway.Domain.Products;

namespace Shopway.Tests.Performance.ControllersUnderTest.ProductController;

[Trait(nameof(IntegrationTest), IntegrationTest.Performance)]
public sealed partial class ProductsControllerTests
{
    private readonly ITestOutputHelper _outputHelper;
    private const string ControllerUri = "products";
    private const string GetApiKey = "d3f72374-ef67-42cb-b25b-fbfee58b1054";

    public ProductsControllerTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    private async Task InsertProduct(ProductId productId)
    {
        await fixture.DataGenerator.AddProduct(productId);
    }

    private async Task DeleteProduct(ProductId productId)
    {
        var entity = await fixture.Context
            .Set<Product>()
            .Where(product => product.Id == productId)
            .FirstAsync(None);

        fixture.Context
            .Set<Product>()
            .Remove(entity);

        await fixture.Context.SaveChangesAsync();
    }

    private void DisplayStatistics(NodeStats stats)
    {
        _outputHelper.WriteLine($"{OkCount}{stats.AllOkCount}");
        _outputHelper.WriteLine($"{FailCount}{stats.AllFailCount}");
        _outputHelper.WriteLine($"{AllCount}{stats.AllRequestCount}");
        _outputHelper.WriteLine($"{FailPercentage}{stats.AllFailCount / stats.AllRequestCount}");
    }
}