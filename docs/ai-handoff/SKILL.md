---
name: kensho-clean-architecture-handoff
description: Use when working on the Kensho.CleanArchitecture repository, especially to continue architecture cleanup, package publishing, MediatR decoupling, domain events, EF Core infrastructure, or Azure NuGet pipeline changes. Load this skill to understand the current solution shape, recent decisions, validation commands, and known follow-up work before editing code.
---

# Kensho Clean Architecture Handoff

## First Steps

1. Read [references/current-state.md](references/current-state.md) before making changes.
2. Read [references/domain-events-mediatr.md](references/domain-events-mediatr.md) when touching domain events, MediatR, middleware, or event handlers.
3. Read [references/pipeline.md](references/pipeline.md) when touching packaging or Azure DevOps pipeline behavior.
4. Run `rg -n "MediatR|INotification|IPublisher|IMediator" src pipelines Kensho.CleanArchitecture.slnx` before and after MediatR-related edits.

## Repository Rules

- Preserve dependency direction: `Domain <- Application <- Infrastructure <- Infrastructure.MediatR`.
- Keep `Domain` and base `Infrastructure` free from MediatR.
- Treat `Infrastructure.MediatR` as an optional integration package.
- Keep pipeline package output deterministic: `pack` writes to `$(PackageOutputPath)` and `push` reads from that same path.
- Prefer focused changes. This repo is small and meant to publish reusable NuGet packages.

## Validation

Use:

```powershell
dotnet build .\Kensho.CleanArchitecture.slnx -c Release -p:GeneratePackageOnBuild=false
dotnet test .\Kensho.CleanArchitecture.slnx -c Release --no-build
git diff --check
```

If local sandbox permissions block `bin` or `obj` writes, rerun the relevant `dotnet` command outside the sandbox with approval.
