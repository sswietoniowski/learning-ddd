﻿using Shopway.Application.Abstractions.CQRS;
using Shopway.Domain.Common.BaseTypes;
using Shopway.Domain.Common.BaseTypes.Abstractions;
using Shopway.Domain.Common.DataProcessing.Proxy;

namespace Shopway.Application.Features.Proxy.GenericQuery.QueryById;

public sealed record GenericByIdQuery<TEntity, TEntityId>(TEntityId EntityId) : IQuery<DataTransferObjectResponse>
    where TEntity : Entity<TEntityId>
    where TEntityId : struct, IEntityId<TEntityId>
{
    public DynamicMapping<TEntity, TEntityId>? Mapping { get; init; }

    public static GenericByIdQuery<TEntity, TEntityId> From(GenericProxyByIdQuery proxyQuery)
    {
        var mapping = DynamicMapping<TEntity, TEntityId>.From(proxyQuery.Mapping);

        return new GenericByIdQuery<TEntity, TEntityId>(TEntityId.Create(proxyQuery.Id))
        {
            Mapping = mapping,
        };
    }
}
