﻿using NetArchTest.Rules;
using Shopway.Application.Abstractions.CQRS;
using Shopway.Tests.Unit.Constants;
using static Shopway.Tests.Unit.Constants.NamingConvention;

namespace Shopway.Tests.Unit.ArchitectureTests.NamingConventions;

public sealed partial class NamingConventionsTests
{
    [Fact]
    [Trait(TraitConstants.Category, TraitConstants.Architecture)]
    public void CommandNames_ShouldEndWithCommand()
    {
        //Arrange
        var assembly = Shopway.Application.AssemblyReference.Assembly;

        //Act
        var result = Types
            .InAssembly(assembly)
            .That()
            .AreClasses()
            .And()
            .ImplementInterface(typeof(ICommand))
            .Should()
            .HaveNameEndingWith(Command)
            .GetResult();

        //Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    [Trait(TraitConstants.Category, TraitConstants.Architecture)]
    public void GenericCommandNames_ShouldEndWithCommand()
    {
        //Arrange
        var assembly = Shopway.Application.AssemblyReference.Assembly;

        //Act
        var result = Types
            .InAssembly(assembly)
            .That()
            .AreClasses()
            .And()
            .ImplementInterface(typeof(ICommand<>))
            .Should()
            .HaveNameEndingWith(Command)
            .GetResult();

        //Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    [Trait(TraitConstants.Category, TraitConstants.Architecture)]
    public void CommandHandlerNames_ShouldEndWithCommandHandler()
    {
        //Arrange
        var assembly = Shopway.Application.AssemblyReference.Assembly;

        //Act
        var result = Types
            .InAssembly(assembly)
            .That()
            .AreClasses()
            .And()
            .ImplementInterface(typeof(ICommandHandler<>))
            .Should()
            .HaveNameEndingWith(CommandHandler)
            .GetResult();

        //Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    [Trait(TraitConstants.Category, TraitConstants.Architecture)]
    public void GenericCommandHandlerNames_ShouldEndWithCommandHandler()
    {
        //Arrange
        var assembly = Shopway.Application.AssemblyReference.Assembly;

        //Act
        var result = Types
            .InAssembly(assembly)
            .That()
            .AreClasses()
            .And()
            .ImplementInterface(typeof(ICommandHandler<,>))
            .Should()
            .HaveNameEndingWith(CommandHandler)
            .GetResult();

        //Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    [Trait(TraitConstants.Category, TraitConstants.Architecture)]
    public void QueryNames_ShouldEndWithQuery()
    {
        //Arrange
        var assembly = Shopway.Application.AssemblyReference.Assembly;

        //Act
        var result = Types
            .InAssembly(assembly)
            .That()
            .AreClasses()
            .And()
            .ImplementInterface(typeof(IQuery<>))
            .Should()
            .HaveNameEndingWith(Query)
            .GetResult();

        //Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    [Trait(TraitConstants.Category, TraitConstants.Architecture)]
    public void QueryHandlerNames_ShouldEndWithQueryHandler()
    {
        //Arrange
        var assembly = Shopway.Application.AssemblyReference.Assembly;

        //Act
        var result = Types
            .InAssembly(assembly)
            .That()
            .AreClasses()
            .And()
            .ImplementInterface(typeof(IQueryHandler<,>))
            .Should()
            .HaveNameEndingWith(QueryHandler)
            .GetResult();

        //Assert
        result.IsSuccessful.Should().BeTrue();
    }
}