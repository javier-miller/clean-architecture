# Domain Events And MediatR

## Core Rule

Do not add MediatR back to `Domain` or base `Infrastructure`.

The only project that should reference MediatR is:

```text
src/Kensho.CleanArchitecture.Infrastructure.MediatR
```

## Domain Event Flow

1. Domain entities raise `IDomainEvent` instances through `Entity.AddEvent`.
2. `ApplicationDbContext.AcceptChangesAsync` captures tracked `IEntity` instances.
3. `SaveChangesAsync` persists the unit of work.
4. Domain events are popped from the persisted entities.
5. `IDomainEventsManager.SubscribeEvents` queues the events.
6. `Infrastructure.MediatR` middleware dispatches queued events via MediatR.

## MediatR Adapter

`Infrastructure.MediatR` contains:

- `DomainEventNotification<TDomainEvent>`: wraps a pure `IDomainEvent` as an `INotification`.
- `IDomainEventsDispatcher`: abstraction for dispatching queued events.
- `MediatRDomainEventsDispatcher`: publishes wrapped events via `IPublisher`.
- `MediatRDomainEventsMiddleware`: dispatches after the request pipeline completes successfully.
- `DependencyInjection`: exposes `AddMediatRDomainEvents(...)` and `UseMediatRDomainEvents()`.

## Expected API Usage

In the API host:

```csharp
builder.Services.AddMediatRDomainEvents(cfg =>
    cfg.RegisterServicesFromAssemblyContaining<SomeHandler>());

app.UseMediatRDomainEvents();
```

Handlers should target the wrapper:

```csharp
public sealed class MyDomainEventHandler
    : INotificationHandler<DomainEventNotification<MyDomainEvent>>
{
    public Task Handle(
        DomainEventNotification<MyDomainEvent> notification,
        CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;
        return Task.CompletedTask;
    }
}
```

## Important Behavior

`MediatRDomainEventsDispatcher` peeks before publishing and dequeues only after publish succeeds. If a handler throws, the failing event remains in the queue for the current request context.

`HttpContextDomainEventsManager` stores the queue in `HttpContext.Items`. This is suitable for request-scoped API flows, but background workers will need a different manager implementation or explicit dispatch strategy.

## Checks

Use this search to ensure MediatR stays isolated:

```powershell
rg -n "MediatR|INotification|IPublisher|IMediator" src pipelines Kensho.CleanArchitecture.slnx
```

Only `Infrastructure.MediatR`, pipeline variables, and solution path references should mention MediatR.
