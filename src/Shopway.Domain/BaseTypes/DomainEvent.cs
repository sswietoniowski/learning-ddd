﻿using Shopway.Domain.Abstractions;

namespace Shopway.Domain.BaseTypes;

/// <summary>
/// DomainEvents are records of something that already occurred in our system
/// Event names should be in the past
/// </summary>
/// <param name="Id">DomainEvent id</param>
public abstract record class DomainEvent(Guid Id) : IDomainEvent;

//We rise the domain event in the Entity methods, for instance after something has succeeded.
//The DomainEvents are handled using MediatR (they are just a notifications with a guid Id)
//"<EventName>DomainEventHandler" is present in the Application layer, in Events folder
//(Shop.Application -> Events)