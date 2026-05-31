# Open Questions And Follow-Ups

## Domain Events

- Decide whether `HttpContextDomainEventsManager` should remain in base `Infrastructure` or move to an HTTP-specific package later.
- Decide whether a background-service-safe domain events manager is needed.
- Consider whether dispatch should happen after every successful unit of work, through middleware, or explicitly by application code.

## Infrastructure Registration

`AddInfrastructureCore<TDbContext>` currently registers:

- `TDbContext`
- `IUnitOfWork`
- `ISpecificationFactory`

It does not register repository factories, repositories, or domain event managers. That may be intentional to keep consumers explicit, but another pass should decide the desired consumer experience.

## Specification Pattern

`ISpecification<TEntity>.Apply(IQueryable<TEntity>)` lives in `Application`. This avoids EF Core references in Application but still exposes query-provider semantics. Consider whether this is acceptable or whether query specifications should be more provider-neutral.

## NuGet Metadata

The projects are packable but have minimal NuGet metadata. Consider adding package id, description, authors, repository URL, license/readme, and symbol package configuration if these packages are meant to be shared broadly.

## Tests

There are no test projects yet. Good first tests:

- `IDomainEvent` does not depend on MediatR.
- `ApplicationDbContext.AcceptChangesAsync` queues events only after successful save.
- `MediatRDomainEventsDispatcher` wraps events in `DomainEventNotification<T>`.
- pipeline YAML references the same package output path for pack, push, and artifact publication.
