# Current State

## Solution Shape

The solution contains four packable class library projects:

- `src/Kensho.CleanArchitecture.Domain/Kensho.CleanArchitecture.Domain.csproj`
- `src/Kensho.CleanArchitecture.Application/Kensho.CleanArchitecture.Application.csproj`
- `src/Kensho.CleanArchitecture.Infrastructure/Kensho.CleanArchitecture.Infrastructure.csproj`
- `src/Kensho.CleanArchitecture.Infrastructure.MediatR/Kensho.CleanArchitecture.Infrastructure.MediatR.csproj`

Dependency direction:

```text
Domain
  <- Application
      <- Infrastructure
          <- Infrastructure.MediatR
```

`Kensho.CleanArchitecture.slnx` includes all four projects.

## Intent

The packages provide a reusable Clean Architecture foundation for .NET APIs:

- `Domain`: base entity, aggregate, value object, and domain event abstractions.
- `Application`: persistence contracts such as repository, specification, factory, and unit of work interfaces.
- `Infrastructure`: EF Core implementation pieces, DbContext base, repository, specification implementation, and HTTP-context-backed domain event queue.
- `Infrastructure.MediatR`: optional MediatR adapter that dispatches queued domain events as MediatR notifications.

## Recent Architectural Changes

MediatR was removed from `Domain` and base `Infrastructure`.

`IDomainEvent` is now a clean domain marker:

```csharp
public interface IDomainEvent;
```

The old unused `src/Kensho.CleanArchitecture.Domain/SparkyCodeStudios.Core.Domain.csproj` was deleted because it was not in the solution or pipeline and could confuse maintenance.

`ApplicationDbContext.AcceptChangesAsync` now saves changes first, then pops and queues domain events. This avoids queuing events for changes that fail to persist.

## Known Worktree Context

At the time this documentation was written, the current work includes:

- pipeline cleanup for deterministic NuGet publishing;
- MediatR decoupling from `Domain` and `Infrastructure`;
- new `Infrastructure.MediatR` project;
- removal of the leftover `SparkyCodeStudios.Core.Domain.csproj`.

Do not assume these changes are committed. Check `git status --short --branch`.

## Build Notes

The solution has been validated with:

```powershell
dotnet build .\Kensho.CleanArchitecture.slnx -c Release -p:GeneratePackageOnBuild=false
dotnet test .\Kensho.CleanArchitecture.slnx -c Release --no-build
```

Both passed with 0 warnings and 0 errors after the MediatR decoupling.
