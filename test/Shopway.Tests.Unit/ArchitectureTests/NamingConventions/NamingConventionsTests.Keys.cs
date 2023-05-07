﻿using NetArchTest.Rules;
using Shopway.Domain.Abstractions;
using static Shopway.Tests.Unit.Constants.NamingConvention;

namespace Shopway.Tests.Unit.ArchitectureTests.NamingConventions;

public partial class NamingConventionsTests
{
    [Fact]
    public void EntityKeyNames_ShouldEndWithKey()
    {
        //Arrange
        var assembly = Shopway.Domain.AssemblyReference.Assembly;

        //Act
        var result = Types
            .InAssembly(assembly)
            .That()
            .AreNotInterfaces()
            .And()
            .ImplementInterface(typeof(IBusinessKey))
            .Should()
            .HaveNameEndingWith(Key)
            .GetResult();

        //Assert
        result.IsSuccessful.Should().BeTrue();
    }
}