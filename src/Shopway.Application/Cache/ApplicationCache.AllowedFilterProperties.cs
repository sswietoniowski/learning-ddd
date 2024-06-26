﻿using Shopway.Domain.Common.DataProcessing.Abstractions;
using Shopway.Domain.Common.Utilities;
using System.Collections.Frozen;

namespace Shopway.Application.Cache;

public static partial class ApplicationCache
{
    public static readonly FrozenDictionary<Type, IReadOnlyCollection<string>> AllowedFilterPropertiesCache;

    private static FrozenDictionary<Type, IReadOnlyCollection<string>> CreateAllowedFilterPropertiesCache()
    {
        Dictionary<Type, IReadOnlyCollection<string>> allowedFilterPropertiesCache = [];

        var dynamicFilterTypes = Domain.AssemblyReference.Assembly
            .GetTypesWithAnyMatchingInterface(i => i.Name.Contains(nameof(IDynamicFilter)))
            .Where(type => type.IsInterface is false);

        foreach (var type in dynamicFilterTypes)
        {
            if (type.IsGenericType)
            {
                continue;
            }

            var typeAllowedFilterProperties = type!.GetProperty(nameof(IDynamicFilter.AllowedFilterProperties))
                !.GetValue(null) as IReadOnlyCollection<string>;
            allowedFilterPropertiesCache.TryAdd(type, typeAllowedFilterProperties!);
        }

        return allowedFilterPropertiesCache.ToFrozenDictionary();
    }
}
