# Persistence Layer

In this project we store components connected to the ORM and database operations:

- DatabaseContext
- UnitOfWork
- Configurations
- Repositories
- Migrations
- SaveChanges Interceptors
- Outbox pattern classes

## Outbox pattern

This pattern is used to publish a domain events. 

This pattern is useful if we want to be sure that our transaction completes in anatomic way.

Inside the transaction we generate one or more outbox messages and we save them in the outbox. 
Later, we process the outbox and publish the messages one by one, so they are handled by they respective consumers.

 Only AggregateRoots can rise domain events, so we need just:

 ```csharp
var outboxMessages = dbContext.ChangeTracker
	.Entries<AggregateRoot>()
```

## Serialize the domain event

We should store the type of the serialized entity. This can be done by JsonSerializerSettings.TypeNameHandling set to All:

```csharp
Content = JsonConvert.SerializeObject(
    domainEvent,
    new JsonSerializerSettings
    {
        TypeNameHandling = TypeNameHandling.All
    })
```

This will help to deserialize object (when we are consuming the OutboxMessages) to IDomainEvent object that we can publish using mediator.

## Process the domain events in the background

We create a background job using the Quartz NuGet Package. This will be a part of a Shopway.Infrastructure layer.