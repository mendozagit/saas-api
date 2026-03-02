# SaasApi Solution Template

A Clean Architecture Solution Template for creating Web API applications with ASP.NET Core and SQL Server.

## Getting Started

The following prerequisites are required to build and run the solution:

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) (latest version)

The easiest way to get started is to install the [.NET template](https://www.nuget.org/packages/SaasApi.Solution.Template):
```
dotnet new install SaasApi.Solution.Template
```

Once installed, create a new solution using the template:

```bash
dotnet new saas-sln --output YourProjectName
```

To create a solution with .NET Aspire support:
```bash
dotnet new saas-sln --UseAspire true --output YourProjectName
```

Launch the app:
```bash
cd src/Web
dotnet run
```

To learn more, run the following command:
```bash
dotnet new saas-sln --help
```

You can create use cases (commands or queries) by navigating to `./src/Application` and running `dotnet new saas-usecase`. Here are some examples:

To create a new command:
```bash
dotnet new saas-usecase --name CreateTodoList --feature-name TodoLists --usecase-type command --return-type int
```

To create a query:
```bash
dotnet new saas-usecase -n GetTodos -fn TodoLists -ut query -rt TodosVm
```

To learn more, run the following command:
```bash
dotnet new saas-usecase --help
```

## Database

The template uses [SQL Server](https://learn.microsoft.com/en-us/sql/sql-server/what-is-sql-server) as the database provider.

On application startup, the database is automatically **deleted**, **recreated**, and **seeded** using `ApplicationDbContextInitialiser`. This is a practical strategy for early development, avoiding the overhead of maintaining migrations while keeping the schema and sample data in sync with the domain model.

This process includes:

- Deleting the existing database
- Recreating the schema from the current model
- Seeding default roles, users, and data

For production environments, consider using EF Core migrations or migration bundles during deployment.
For more information, see [Database Initialisation Strategies for EF Core](https://jasontaylor.dev/ef-core-database-initialisation-strategies).

## Deploy

This template is structured to follow the Azure Developer CLI (azd). You can learn more about `azd` in the [official documentation](https://learn.microsoft.com/en-us/azure/developer/azure-developer-cli). To get started:

```bash
# Log in to Azure
azd auth login

# Provision and deploy to Azure
azd up
```

To set up a CI/CD pipeline (GitHub Actions or Azure DevOps):

```bash
azd pipeline config
```

## API Documentation

This template includes built-in API documentation using [ASP.NET Core OpenAPI](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/openapi/overview) and [Scalar](https://scalar.com/). Once the application is running, navigate to `/scalar` to explore the API using the Scalar UI.

The OpenAPI specification is generated at build time and written to `wwwroot/openapi/v1.json`.

## Technologies

* [ASP.NET Core 10](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core)
* [Entity Framework Core 10](https://docs.microsoft.com/en-us/ef/core/)
* [MediatR](https://github.com/jbogard/MediatR)
* [AutoMapper](https://automapper.org/)
* [FluentValidation](https://fluentvalidation.net/)
* [NUnit](https://nunit.org/), [Shouldly](https://docs.shouldly.org/), [Moq](https://github.com/devlooped/moq) & [Respawn](https://github.com/jbogard/Respawn)
* [Scalar](https://scalar.com/)

## License

This project is licensed with the [MIT license](LICENSE).
