﻿using Shopway.Domain.Common.DataProcessing.Abstractions;
using Shopway.Domain.Common.Utilities;
using System.Collections.Frozen;

namespace Shopway.Application.Cache;

public static partial class ApplicationCache
{
    public static readonly FrozenDictionary<Type, IReadOnlyCollection<string>> AllowedFilterOperationsCache;

    private static FrozenDictionary<Type, IReadOnlyCollection<string>> CreateAllowedFilterOperationsCache()
    {
        Dictionary<Type, IReadOnlyCollection<string>> allowedFilterOperations = [];

        var dynamicFilterTypes = Domain.AssemblyReference.Assembly
            .GetTypesWithAnyMatchingInterface(i => i.Name.Contains(nameof(IDynamicFilter)))
            .Where(type => type.IsInterface is false);

        foreach (var type in dynamicFilterTypes)
        {
            if (type.IsGenericType)
            {
                continue;
            }

            var typeAllowedFilterOperations = type!.GetProperty(nameof(IDynamicFilter.AllowedFilterOperations))
                !.GetValue(null) as IReadOnlyCollection<string>;
            allowedFilterOperations.TryAdd(type, typeAllowedFilterOperations!);
        }

        return allowedFilterOperations.ToFrozenDictionary();
    }
}
