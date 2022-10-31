﻿using Microsoft.EntityFrameworkCore;

namespace Shopway.Persistence;

//TODO use Bogus to seed database
public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }
}