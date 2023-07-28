﻿using Shopway.Application.Abstractions;
using Shopway.Application.Exceptions;

namespace Shopway.Application.CQRS;

public sealed class CursorPageResponse<TValue> : IResponse
{
    /// <summary>
    /// Generic list that stores the pagination result
    /// </summary>
    public IList<TValue> Items { get; private init; }

    /// <summary>
    /// Current page cursor. Id from which we query records
    /// </summary>
    public Ulid CurrentCursor { get; private init; }

    /// <summary>
    /// Next page cursor. Id of next record to query or Ulid.Empty if the last record was reached
    /// </summary>
    public Ulid NextCursor { get; private init; }

    public CursorPageResponse(IList<TValue> items, Ulid currentCursor, Ulid nextCursor)
    {
        Items = items;

        var notLastPage = nextCursor != Ulid.Empty;
        var invalidCursor = currentCursor.CompareTo(nextCursor) > 0;

        if (notLastPage && invalidCursor)
        {
            throw new BadRequestException($"Selected cursor '{currentCursor}' is greater then next cursor '{nextCursor}'");
        }

        CurrentCursor = currentCursor;
        NextCursor = nextCursor;
    }
}