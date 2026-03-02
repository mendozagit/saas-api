# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is the **SaasApi Solution Template** for .NET — a `dotnet new` template that scaffolds enterprise ASP.NET Core Web API applications with SQL Server. It targets **.NET 10** (SDK 10.0.103) and is published to NuGet as `SaasApi.Solution.Template`.

## Build & Run Commands

```bash
# Build the solution
dotnet build SaasApi.slnx --configuration Release

# Run the web app (from repo root)
dotnet run --project src/Web

# Run all tests
dotnet test --configuration Release

# Run a single test by name
dotnet test --configuration Release --filter "FullyQualifiedName~CreateTodoItemTests"

# Run tests for a specific project
dotnet test tests/Application.UnitTests
dotnet test tests/Domain.UnitTests
dotnet test tests/Application.FunctionalTests
dotnet test tests/Infrastructure.IntegrationTests

# Scaffold a new CQRS command (run from src/Application/)
dotnet new saas-usecase --name CreateTodo --feature-name TodoItems --usecase-type command --return-type int

# Scaffold a new CQRS query (run from src/Application/)
dotnet new saas-usecase -n GetTodos -fn TodoLists -ut query -rt TodosVm
```

## Architecture

Four-layer Clean Architecture with strict dependency inversion:

```
Web → Application → Domain
         ↑
  Infrastructure (implements Application interfaces)
```

### Domain (`src/Domain`)
Pure business logic with zero external dependencies. Contains `BaseEntity` (with domain event tracking), `BaseAuditableEntity` (adds Created/LastModified audit fields), `ValueObject` base class, domain events (`BaseEvent` → MediatR `INotification`), and entities (`TodoList`, `TodoItem`). Business state changes raise domain events (e.g., setting `TodoItem.Done = true` raises `TodoItemCompletedEvent`).

### Application (`src/Application`)
CQRS use cases organized by feature folder:
```
Application/{Feature}/
├── Commands/{CommandName}/
│   ├── {CommandName}.cs          (handler)
│   ├── {CommandName}Command.cs   (request)
│   └── {CommandName}Validator.cs (FluentValidation)
└── Queries/{QueryName}/
    └── ...
```

MediatR pipeline behaviors execute in order: Logging → UnhandledException → Authorization → Validation → Performance. The `[Authorize]` attribute on commands/queries triggers `AuthorizationBehaviour` to check roles/policies. FluentValidation validators are auto-discovered by assembly scanning.

AutoMapper profiles are defined as **nested private classes inside DTOs** (not in separate profile files).

DI registration: `builder.AddApplicationServices()` in `DependencyInjection.cs`.

### Infrastructure (`src/Infrastructure`)
Implements `IApplicationDbContext`, `IIdentityService`, and `IUser`. EF Core interceptors auto-populate audit fields (`AuditableEntityInterceptor`) and dispatch domain events after `SaveChangesAsync` (`DispatchDomainEventsInterceptor`). Entity configurations use `IEntityTypeConfiguration<T>`.

Uses **SQL Server** as the database provider. In development, the database is **deleted and recreated on every startup** via `ApplicationDbContextInitialiser`.

DI registration: `builder.AddInfrastructureServices()`.

### Web (`src/Web`)
ASP.NET Core Minimal API endpoints (not controllers). Each endpoint group extends `EndpointGroupBase` and is auto-discovered. `CurrentUser` implements `IUser` from `HttpContext`. `CustomExceptionHandler` maps application exceptions to HTTP status codes (ValidationException→400, NotFoundException→404, ForbiddenAccessException→403).

Uses Bearer token authentication (API-only, no SPA). API docs at `/scalar` (Scalar UI). OpenAPI spec generated at build time to `wwwroot/openapi/v1.json`.

DI registration: `builder.AddWebServices()`.

## Test Architecture

| Project | Type | Key Details |
|---------|------|-------------|
| `Domain.UnitTests` | Unit | Pure domain logic (value objects, entities) |
| `Application.UnitTests` | Unit | Behaviors, validators, mappings |
| `Application.FunctionalTests` | Integration | Uses `CustomWebApplicationFactory`, `Testing` static helper class, Testcontainers, Respawn for DB reset |
| `Infrastructure.IntegrationTests` | Integration | EF Core data access |

**Testing patterns**: Tests inherit from `BaseTestFixture` which calls `ResetState()` between tests. Use `Testing.SendAsync()` to send MediatR commands/queries. Use `Testing.RunAsDefaultUserAsync()` / `RunAsAdministratorAsync()` to set up authenticated user context.

**Test stack**: NUnit, Shouldly (assertions), Moq, Respawn, Testcontainers.

## Code Style

- **TreatWarningsAsErrors** is enabled — all warnings must be resolved
- File-scoped namespaces, nullable reference types enabled, implicit usings
- 4-space indentation for C#, 2-space for XML/JSON
- LF line endings
- Build artifacts output to `./artifacts/`
- Central package management via `Directory.Packages.props`

## Template System

This repo is itself a `dotnet new` template. Files contain template directives (`#if`, `#endif`) for conditional code based on template parameters (Aspire support, pipeline provider). These directives are **not comments to clean up** — they are intentional template syntax.
