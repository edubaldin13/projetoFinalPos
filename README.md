# ProjetoFinalPos - REST API for Cliente Management

A C# ASP.NET Core REST API implementing MVC architecture with PostgreSQL database for managing customer (Cliente) information. Built as a Software Architecture Bootcamp final project.

## 📋 Project Overview

This project demonstrates:
- **MVC Architecture** with clear separation of concerns
- **Layered Architecture** (Controllers → Services → Repositories → Data)
- **Repository Pattern** for data access abstraction
- **Dependency Injection** for loose coupling
- **Entity Framework Core** with PostgreSQL
- **Docker** containerization for development and deployment
- **RESTful API Design** with complete CRUD operations

## 📦 Quick Start

### Option 1: Docker (Recommended)

```bash
# From project root directory
docker-compose up -d

# The API will be available at:
# HTTP: http://localhost:5000
# HTTPS: https://localhost:5001

# PostgreSQL will be available at:
# localhost:5432 (username: postgres, password: postgres)
```

### Option 2: Local Development

#### Prerequisites
- .NET 10 SDK
- PostgreSQL 15+
- Visual Studio Code or Visual Studio 2022

#### Steps

1. **Install dependencies**
   ```bash
   cd ProjetoFinalPos
   dotnet restore
   ```

2. **Configure database connection** (if needed)
   - Edit `appsettings.Development.json`
   - Ensure PostgreSQL service is running

3. **Create and apply migrations**
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```

5. **Access the API**
   - HTTP: http://localhost:5000
   - HTTPS: https://localhost:5001
   - Swagger: https://localhost:5001/openapi/v1.json

## 🔌 API Endpoints

### Base URL
```
http://localhost:5000/api/cliente
```

### Endpoints

| Method | Endpoint | Description | Request Body |
|--------|----------|-------------|--------------|
| **GET** | `/` | Get all clients | - |
| **GET** | `/count` | Get total count of clients | - |
| **GET** | `/{id}` | Get client by ID | - |
| **GET** | `/search/{name}` | Search clients by name | - |
| **POST** | `/` | Create new client | ClienteDto |
| **PUT** | `/{id}` | Update client | ClienteDto |
| **DELETE** | `/{id}` | Delete client | - |

### Request/Response Examples

#### Create Client
```bash
POST /api/cliente
Content-Type: application/json

{
  "name": "João Silva",
  "email": "joao@example.com",
  "phone": "11999999999"
}
```

**Response (201 Created)**
```json
{
  "id": 1,
  "name": "João Silva",
  "email": "joao@example.com",
  "phone": "11999999999",
  "createdAt": "2026-03-29T10:30:00Z",
  "updatedAt": null
}
```

#### Get All Clients
```bash
GET /api/cliente
```

**Response (200 OK)**
```json
[
  {
    "id": 1,
    "name": "João Silva",
    "email": "joao@example.com",
    "phone": "11999999999",
    "createdAt": "2026-03-29T10:30:00Z",
    "updatedAt": null
  }
]
```

#### Get Client Count
```bash
GET /api/cliente/count
```

**Response (200 OK)**
```json
1
```

#### Search by Name
```bash
GET /api/cliente/search/João
```

#### Update Client
```bash
PUT /api/cliente/1
Content-Type: application/json

{
  "name": "João Silva Updated",
  "email": "joao.updated@example.com",
  "phone": "11888888888"
}
```

#### Delete Client
```bash
DELETE /api/cliente/1
```

**Response (204 No Content)**

## 🏗️ Project Structure

```
ProjetoFinalPos/
├── Controllers/              # HTTP request handlers
│   └── ClienteController.cs
├── Services/                 # Business logic
│   └── ClienteService.cs
├── Repositories/             # Data access layer
│   ├── IClienteRepository.cs (interface)
│   └── ClienteRepository.cs  (implementation)
├── Models/                   # Domain entities
│   └── Cliente.cs
├── Dto/                      # Data Transfer Objects
│   ├── ClienteDto.cs        (request payload)
│   └── ClienteResponseDto.cs (response payload)
├── Data/                     # Database configuration
│   └── ApplicationDbContext.cs
├── Documentation/            # Architecture docs
│   ├── ARCHITECTURE.md
│   └── C4_MODEL.md
├── Program.cs               # Startup & DI configuration
├── appsettings.json         # Default settings
├── appsettings.Development.json
├── docker-compose.yml       # Multi-container orchestration
├── Dockerfile              # Container build instructions
├── Dependencies.md         # NuGet packages & dependencies
├── ProjetoFinalPos.csproj  # Project configuration
└── ProjetoFinalPos.http    # HTTP request examples
```

## 🗄️ Database

### Schema
```sql
CREATE TABLE clientes (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    email VARCHAR(255) NOT NULL,
    phone VARCHAR(20),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP
);
```

### Connection String
```
Host=localhost;Port=5432;Database=ProjetoFinalPos;Username=postgres;Password=postgres
```

### Access PostgreSQL

```bash
# Using psql
psql -h localhost -U postgres -d ProjetoFinalPos

# Or with Docker
docker exec -it projeto_final_postgres psql -U postgres -d ProjetoFinalPos
```

## 🏛️ Architecture

### Layered Architecture Pattern

```
Controllers
    ↓ HTTP Handlers
Services
    ↓ Business Logic
Repositories
    ↓ Data Access
DbContext (EF Core)
    ↓ ORM
PostgreSQL Database
```

### Key Design Patterns

1. **Repository Pattern**: Abstract database operations
2. **Service Layer Pattern**: Centralize business logic
3. **Dependency Injection**: Loose coupling between layers
4. **DTO Pattern**: Decouple API contracts from domain models

## 📚 Documentation

- **[ARCHITECTURE.md](Documentation/ARCHITECTURE.md)** - Detailed architecture documentation, design patterns, and data flows
- **[C4_MODEL.md](Documentation/C4_MODEL.md)** - C4 model diagrams showing system context, containers, and components
- **[Dependencies.md](Dependencies.md)** - NuGet packages, versions, and external dependencies

## 🔧 Configuration

### Development Settings (appsettings.Development.json)
```json
{
  "ConnectionStrings": {
    "PostgreSQL": "Host=localhost;Port=5432;Database=ProjetoFinalPos;Username=postgres;Password=postgres"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

### Environment Variables (Docker)
```
ASPNETCORE_ENVIRONMENT=Development
ConnectionStrings__PostgreSQL=Host=postgres;Port=5432;Database=ProjetoFinalPos;Username=postgres;Password=postgres
```

## 🐳 Docker Commands

```bash
# Build and start containers
docker-compose up -d

# View logs
docker-compose logs -f

# Stop containers
docker-compose down

# Remove volumes (reset database)
docker-compose down -v

# Rebuild images
docker-compose build --no-cache

# Access PostgreSQL
docker-compose exec postgres psql -U postgres -d ProjetoFinalPos

# Access application logs
docker-compose logs app
```

## 🧪 Testing the API

### Using cURL

```bash
# Create
curl -X POST http://localhost:5000/api/cliente \
  -H "Content-Type: application/json" \
  -d '{"name":"Test","email":"test@example.com","phone":"123"}'

# Get All
curl http://localhost:5000/api/cliente

# Get by ID
curl http://localhost:5000/api/cliente/1

# Update
curl -X PUT http://localhost:5000/api/cliente/1 \
  -H "Content-Type: application/json" \
  -d '{"name":"Updated","email":"updated@example.com","phone":"456"}'

# Delete
curl -X DELETE http://localhost:5000/api/cliente/1
```

### Using Postman

1. Import the HTTP requests from `ProjetoFinalPos.http`
2. Or create requests manually with the endpoints listed above

## 📋 NuGet Packages

- **Microsoft.AspNetCore.OpenApi** - OpenAPI support
- **Microsoft.EntityFrameworkCore** - ORM framework
- **Microsoft.EntityFrameworkCore.Design** - Design-time tools
- **Npgsql.EntityFrameworkCore.PostgreSQL** - PostgreSQL provider

See [Dependencies.md](Dependencies.md) for complete list.

## ⚠️ Error Handling

| Status | Description | Example |
|--------|-------------|---------|
| 200 | OK - Successful retrieval | GET all clients |
| 201 | Created - Resource created | POST new client |
| 204 | No Content - Successful deletion | DELETE client |
| 400 | Bad Request - Invalid input | Missing required fields |
| 404 | Not Found - Resource doesn't exist | GET non-existent ID |

## 🚀 Deployment

### Production Considerations

1. **Environment Variables**: Use secure secrets management
2. **Connection Strings**: Store in environment variables, not appsettings
3. **CORS Policy**: Restrict to specific origins
4. **Logging**: Use structured logging (Serilog)
5. **Authentication**: Implement JWT or OAuth
6. **Database**: Use RDS or managed PostgreSQL service

### Docker Production Build

```bash
# Multi-stage build
docker build -t projeto-final-pos:latest .

# Run container
docker run -p 5000:5000 \
  -e ConnectionStrings__PostgreSQL="..." \
  -e ASPNETCORE_ENVIRONMENT=Production \
  projeto-final-pos:latest
```

## 🛠️ Development Workflow

1. **Make changes** to code
2. **Test locally** with `dotnet run`
3. **Run migrations** if model changes: `dotnet ef migrations add [Name]`
4. **Test API** with cURL or Postman
5. **Commit** changes to git

## 📖 Learning Resources

- [Microsoft ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core Docs](https://docs.microsoft.com/en-us/ef/core/)
- [PostgreSQL Official Docs](https://www.postgresql.org/docs/)
- [Docker Documentation](https://docs.docker.com/)
- [RESTful API Design Best Practices](https://restfulapi.net/)

## 📝 License

This project is part of a Software Architecture Bootcamp final project.

## 👨‍💻 Author

Created as a demonstration of MVC architecture and C# best practices.

---

**Ready to start?** Run `docker-compose up -d` and access the API!
