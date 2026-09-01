# MagicVilla API

MagicVilla API is a .NET-based hotel and villa management backend built with ASP.NET Core, Entity Framework Core, SQL Server, JWT authentication, and API versioning. The solution includes a REST API for villas and villa numbers, plus a web front-end that consumes the API and handles authentication through cookies.

This project is designed as a learning and demo application for building a complete API + web client architecture, following common enterprise patterns for repositories, DTOs, versioned endpoints, and secure authentication.

## Overview

The solution is composed of these main projects:

- `MagicVilla_VillaAPI`: REST API backend
- `MagicVilla_WebPage`: ASP.NET Core MVC app consuming the API
- `MagicVilla_utility`: shared classes and constants
- `MagicVilla_Web`: an additional F# web project present in the repository, but not the primary active project for this solution

The API exposes endpoints for:

- Managing villas
- Managing villa numbers
- User registration and login
- JWT-based authentication
- API versioning (v1 and v2)
- Swagger documentation
- Pagination and filtering

## Features

### Backend features

- ASP.NET Core Web API
- SQL Server persistence with Entity Framework Core
- Repository pattern
- AutoMapper for DTO mapping
- JWT authentication
- Role-based authorization (`admin` role)
- API versioning using `Microsoft.AspNetCore.Mvc.Versioning`
- Swagger/OpenAPI documentation
- Response caching
- Data filtering and pagination
- Generic API response envelope

### Frontend features

- MVC web UI built with ASP.NET Core
- Login and registration pages
- Cookie-based session handling after JWT authentication
- Integration with the API via `HttpClient`
- Protected pages and access-denied flow

## Tech Stack

- ASP.NET Core 10
- C#
- Entity Framework Core 7
- SQL Server
- JWT / Bearer Authentication
- Swagger / Swashbuckle
- AutoMapper
- Newtonsoft.Json
- ASP.NET Core Identity
- ASP.NET Core MVC

## Solution Structure

```text
MagicVilla_API/
├── MagicVilla.sln
├── README.md
├── MagicVilla_utility/
│   └── SD.cs
├── MagicVilla_VillaAPI/
│   ├── Controllers/
│   │   ├── UserController.cs
│   │   ├── V1/
│   │   └── V2/
│   ├── Data/
│   ├── Models/
│   ├── Repository/
│   ├── appsettings.json
│   ├── appsettings.Development.json
│   ├── Program.cs
│   └── MagicVilla_VillaAPI.csproj
├── MagicVilla_WebPage/
│   ├── Controllers/
│   ├── Models/
│   ├── Services/
│   ├── Views/
│   ├── appsettings.json
│   ├── Program.cs
│   └── MagicVilla_WebPage.csproj
└── MagicVilla_Web/
    └── F# web project
```

## Main Architecture

### API layer

The `MagicVilla_VillaAPI` project contains:

- Controllers for villas, villa numbers, and authentication
- Models and DTOs
- Data access layer through repositories
- Database context configuration
- Authentication configuration and security settings

### Repository pattern

Repositories abstract the database logic and expose methods such as:

- `GetAllAsync(...)`
- `GetAsync(...)`
- `CreateAsync(...)`
- `RemoveAsync(...)`
- `UpdateAsync(...)`

This keeps controllers thinner and the data access layer reusable.

### Identity + JWT

The API uses:

- `ApplicationUser` with ASP.NET Core Identity
- JWT bearer authentication
- Role-based authorization (admin role is enforced on some endpoints)

The JWT secret is configured in `appsettings.json` under `ApiSettings:Secret`.

## Database

The application is configured to use SQL Server and the connection string is defined in:

- `MagicVilla_VillaAPI/appsettings.json`

Default configuration:

```json
"ConnectionStrings": {
  "DefaultSQLConnection": "Server=localhost; Database=MagicVilla;TrustServerCertificate=true;Trusted_Connection=true;MultipleActiveResultSets=true"
}
```

Before running the API, make sure:

1. SQL Server is installed and running locally.
2. A database named `MagicVilla` exists or can be created.
3. The connection string matches your local environment.

The project uses EF Core migrations and the database context seeds initial villa data.

## Authentication

The API supports user registration and login through the `UserController`:

- `POST /api/v1/UsersAuth/register`
- `POST /api/v1/UsersAuth/login`

The login endpoint returns a JWT token if the credentials are valid.

The Swagger UI is configured to allow Bearer token authentication, which is useful for testing protected endpoints.

Protected endpoints include the villa and villa-number management actions that require the admin role.

## API Versioning

The API is versioned with ASP.NET Core API versioning:

- V1: `api/v1/...`
- V2: `api/v2/...`

This is configured in `Program.cs` with:

- `AddApiVersioning()`
- `AddVersionedApiExplorer()`

Example routes:

- `api/v1/VillaAPI`
- `api/v1/VillaNumberAPI`
- `api/v2/VillaNumberAPI`

## Swagger

Swagger is enabled in development mode:

- `UseSwagger()`
- `UseSwaggerUI()`

You can access the API documentation at:

```text
https://localhost:7001/swagger
```

or, depending on the launched profile:

```text
http://localhost:5000/swagger
```

## Available Endpoints

### Authentication

```http
POST /api/v1/UsersAuth/register
POST /api/v1/UsersAuth/login
```

### Villa API (v1)

```http
GET    /api/v1/VillaAPI
GET    /api/v1/VillaAPI/{id}
POST   /api/v1/VillaAPI
PUT    /api/v1/VillaAPI/{id}
DELETE /api/v1/VillaAPI/{id}
PATCH  /api/v1/VillaAPI/{id}
```

### Villa Number API (v1)

```http
GET    /api/v1/VillaNumberAPI
GET    /api/v1/VillaNumberAPI/{id}
POST   /api/v1/VillaNumberAPI
PUT    /api/v1/VillaNumberAPI/{id}
DELETE /api/v1/VillaNumberAPI/{id}
```

### Villa Number API (v2)

```http
GET /api/v2/VillaNumberAPI/GetString
```

## Project Dependencies

The backend project references packages such as:

- `Microsoft.AspNetCore.Authentication.JwtBearer`
- `Microsoft.AspNetCore.Identity.EntityFrameworkCore`
- `Microsoft.EntityFrameworkCore.SqlServer`
- `Microsoft.AspNetCore.Mvc.Versioning`
- `Microsoft.AspNetCore.JsonPatch`
- `Swashbuckle.AspNetCore`
- `AutoMapper`
- `Serilog.AspNetCore`

The web project references:

- `AutoMapper`
- `Newtonsoft.Json`
- `MagicVilla_utility`
- `MagicVilla_VillaAPI`

## Running the Application

### Prerequisites

- .NET SDK 10 or compatible SDK
- SQL Server instance
- Visual Studio 2022 or VS Code with C# extension
- Optional: SQL Server Management Studio (SSMS) for database inspection

### 1. Restore dependencies

```bash
dotnet restore
```

### 2. Build the solution

```bash
dotnet build MagicVilla.sln
```

### 3. Update connection string

Edit the API appsettings file and set a valid SQL Server connection string.

### 4. Run database migrations

If the database is not already created, run:

```bash
dotnet ef database update --project MagicVilla_VillaAPI/MagicVilla_VillaAPI.csproj
```

If `dotnet ef` is not available, install the EF Core tools or use the Visual Studio NuGet Package Manager Console.

### 5. Run the API

From the solution root:

```bash
dotnet run --project MagicVilla_VillaAPI/MagicVilla_VillaAPI.csproj
```

### 6. Run the web application

```bash
dotnet run --project MagicVilla_WebPage/MagicVilla_WebPage.csproj
```

The web app expects the API URL in `MagicVilla_WebPage/appsettings.json`:

```json
"ServicesUrls": {
  "VillaApi": "https://localhost:7001/"
}
```

Make sure the address matches the actual API launch URL.

## Login Flow in Web App

The web front-end performs the following:

1. User enters username and password
2. `AuthService` calls the login endpoint on the API
3. The API returns a JWT token
4. The MVC app reads the JWT claims
5. A cookie-based authentication session is created
6. The token is stored in the session for subsequent API calls

This pattern allows the web UI to protect pages while using the API as the backend.

## Security Notes

- JWT secret should be changed in production
- Do not commit real secrets to source control
- Use HTTPS in production environments
- Restrict admin endpoints to trusted users only
- Consider stronger validation and authorization policies for enterprise scenarios

## Potential Improvements

This project is a strong base for a real application, but there are several enhancements that could be added:

- Unit and integration tests
- Better centralized error handling
- Logging best practices and ELK/Serilog dashboards
- Refresh token support
- Advanced role and permission management
- Docker support
- CI/CD pipeline
- API response caching improvements
- Frontend form validation and UX refinements

## Learning Outcomes

This solution demonstrates common real-world ASP.NET Core development patterns, including:

- REST API design
- Entity Framework Core persistence
- Identity and JWT security
- DTO mapping and validation
- API versioning
- Swagger documentation
- MVC integration with an external API
- Repository abstraction

## License

This project is provided as an educational/demo project. If needed, you can adapt the licensing terms for your organization or deployment environment.

## Author

Developed as a practical ASP.NET Core API and MVC sample focused on villa booking / accommodation management.

## Conclusion

MagicVilla API is a complete example of a small but realistic backend architecture, combining modern ASP.NET Core practices with authentication, database access, API versioning, and a web client. It is an excellent foundation for learning backend design and building similar systems for booking, property management, or service-oriented web applications.
