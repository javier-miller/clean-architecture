# NuGet Pipeline

## Current Shape

Pipeline path:

```text
pipelines/nuget.yaml
```

It is manual only:

```yaml
trigger: none
pr: none
```

It uses:

- .NET SDK `10.x`
- Azure Artifacts feed variable `VstsFeed`
- version pattern `0.<minorVersion>.<Build.BuildId>`
- package output variable `PackageOutputPath: '$(Build.ArtifactStagingDirectory)/nupkgs'`

## Projects Packed

The pipeline packs four projects:

- `DomainProject`
- `ApplicationProject`
- `InfrastructureProject`
- `InfrastructureMediatRProject`

Each `pack` step writes to:

```yaml
-o $(PackageOutputPath)
```

The `push` step reads from:

```yaml
searchPatternPush: '$(PackageOutputPath)/*.nupkg'
```

This is intentional. Do not change push back to `src/**/bin/Release/*.nupkg`, because that can publish packages generated implicitly during build rather than packages created by the explicit `pack` steps.

## Build Step

The pipeline build uses:

```yaml
dotnet build -c $(BuildConfiguration) --no-restore -p:GeneratePackageOnBuild=false
```

This prevents `GeneratePackageOnBuild=True` in project files from producing extra packages before the explicit pack steps.

## If Editing Pipeline

Keep these invariants:

- build should not generate packages;
- pack should be the only package creation step;
- push should publish exactly from `$(PackageOutputPath)`;
- artifact publication should publish `$(PackageOutputPath)`;
- comments should say four packages while `Infrastructure.MediatR` exists.
