﻿using NetArchTest.Rules;
using Shopway.Domain.Abstractions;
using Shopway.Tests.Unit.Constants;
using static Shopway.Tests.Unit.Constants.NamingConvention;

namespace Shopway.Tests.Unit.ArchitectureTests.NamingConventions;

public partial class NamingConventionsTests
{
    [Fact]
    [Trait(TraitConstants.Category, TraitConstants.Architecture)]
    public void EntityIdNames_ShouldEndWithId()
    {
        //Arrange
        var assembly = Shopway.Domain.AssemblyReference.Assembly;

        //Act
        var result = Types
            .InAssembly(assembly)
            .That()
            .AreNotInterfaces()
            .And()
            .ImplementInterface(typeof(IEntityId))
            .Should()
            .HaveNameEndingWith(Id)
            .GetResult();

        //Assert
        result.IsSuccessful.Should().BeTrue();
    }
}