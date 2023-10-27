﻿using Shopway.Domain.Abstractions;
using System.Reflection;

namespace Shopway.Domain.Utilities;

public static class ReflectionUtilities
{
    public static Type GetEntityTypeFromEntityIdType(Type entityIdType)
    {
        var skipAmount = IEntityId.Id.Length;
        var typeName = entityIdType.Name[0..^skipAmount];

        return Domain.AssemblyReference.Assembly
            .GetTypes()
            .Where(type => type.Name == typeName)
            .Single();
    }

    public static Type[] GetEntityIdTypes()
    {
        return Domain.AssemblyReference.Assembly
            .GetTypes()
            .Where(type => type.Implements<IEntityId>())
            .Where(type => type.IsValueType)
            .ToArray();
    }

    public static Type GetEntityTypeFromEntityId(this IEntityId entityId)
    {
        var assembly = Shopway.Domain.AssemblyReference.Assembly;

        var skipAmount = IEntityId.Id.Length;

        var typeName = entityId.GetType().Name[0..^skipAmount];

        return assembly
            .GetTypes()
            .Where(type => type.Name == typeName)
            .Single();
    }

    public static MethodInfo GetSingleGenericMethod(this Type baseType, string methodName, params Type[] genericType)
    {
        var methodFormBaseType = baseType
            .GetMethods()
            .Where(method => method.Name == methodName)
            .Single();

        return methodFormBaseType.MakeGenericMethod(genericType);
    }

    public static IEntityId GetEntityIdFromEntity(this IEntity baseType)
    {
        return (IEntityId)baseType
                .GetType()
                .GetProperty(IEntityId.Id)!
                .GetValue(baseType)!;
    }

    public static bool Implements<TInterface>(this PropertyInfo property)
    {
        return property
            .PropertyType
            .Implements<TInterface>();
    }

    public static bool Implements<TInterface>(this Type baseType)
    {
        return baseType
            .GetInterfaces()
            .Any(interfaceType => interfaceType == typeof(TInterface));
    }

    public static Type[] GetTypesWithAnyMatchingInterface(this Assembly assembly, Func<Type, bool> typeInterfaceMatch)
    {
        return assembly
            .GetTypes()
            .Where(type => type.GetInterfaces().Any(typeInterfaceMatch))
            .ToArray();
    }
}