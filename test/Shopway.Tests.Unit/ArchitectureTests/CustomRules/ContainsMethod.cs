﻿using NetArchTest.Rules;
using Mono.Cecil;

namespace Shopway.Tests.Unit.ArchitectureTests.CustomRules;

public sealed class ContainsMethod : ICustomRule
{
    private readonly Func<TypeDefinition, bool> _test;

    public ContainsMethod(string methodName)
    {
        _test = x => x.Methods.Any(x => x.Name == methodName);
    }

    public bool MeetsRule(TypeDefinition type)
    {
        return _test(type);
    }
}