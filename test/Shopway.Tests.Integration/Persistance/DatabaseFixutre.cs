﻿using Microsoft.EntityFrameworkCore;
using Shopway.Persistence.Framework;
using static Shopway.Persistence.Constants.ConnectionConstants;

namespace Shopway.Tests.Integration.Persistance;

public sealed class DatabaseFixture : IDisposable, IAsyncLifetime
{
    private readonly ShopwayDbContext _context;
    private readonly TestDataGenerator _testDataGenerator;

    public DatabaseFixture()
    {
        var factory = new ShopwayDbContextFactory();
        _context = factory.CreateDbContext(new[] { TestConnection });
        _context.Database.Migrate();

        var testContext = new TestContextService();
        var unitOfWork = new UnitOfWork<ShopwayDbContext>(_context, testContext);
        _testDataGenerator = new TestDataGenerator(unitOfWork);
    }

    public TestDataGenerator DataGenerator => _testDataGenerator;
    public ShopwayDbContext Context => _context;

    public void Dispose()
    {
        Context.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task DisposeAsync()
    {
        try
        {
            await DataGenerator.CleanDatabaseFromTestData();
        }
        catch
        {
            Console.WriteLine("CleanTestData.Integration.Api.Tests failed.");
        }
    }

    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }
}