# Grand Hotel ERP System

A comprehensive Enterprise Resource Planning (ERP) system for hotel management, built with ASP.NET Core MVC and Entity Framework Core.

## Project Overview

This is a group project implementing an integrated hotel management system with multiple modules:

- **Billing Module** - Manage invoices, payments, and financial transactions
- **Employee Management** - Staff directory, roles, and scheduling
- **Room Management** - Room inventory, status tracking, and availability
- **Inventory Management** - Stock tracking and supplies management
- **Reservations** - Booking system and guest management

## Technology Stack

- **Backend:** ASP.NET Core 9.0 MVC
- **Database:** SQL Server (LocalDB for development)
- **ORM:** Entity Framework Core 9.0
- **Frontend:** HTML, CSS, JavaScript (planned integration)

## Project Structure

```
├── Controllers/              # MVC Controllers for each module
│   ├── AccountController.cs
│   ├── BillingController.cs
│   ├── EmployeesController.cs
│   ├── InventoriesController.cs
│   ├── ReservationController.cs
│   └── RoomController.cs
├── Models/                   # Data models and DbContext
│   ├── Billing.cs
│   ├── Employee.cs
│   ├── Inventory.cs
│   ├── Room.cs
│   └── GrandHotelContext.cs
├── Migrations/               # EF Core database migrations
├── Views/                    # MVC Views (Razor templates)
├── wwwroot/                  # Static files (CSS, JS, images)
├── Data/                     # Data access layer
├── grand_hotel_room_management_frontend/  # Room management frontend prototype
├── Program.cs                # Application startup configuration
├── appsettings.json         # Configuration and connection strings
└── GrandHotel.csproj        # Project file
```

## How it Works

1. **Frontend Request** → ASP.NET MVC Controller
2. **Controller** → Queries/Updates Models via Entity Framework
3. **EF Core** → Communicates with SQL Server Database
4. **Response** → Rendered Razor Views back to client

## Getting Started

### Prerequisites

- .NET 9.0 SDK
- SQL Server Express (or LocalDB)
- Visual Studio 2022 or VS Code

### Setup Instructions

1. **Clone the repository**
   ```bash
   git clone https://github.com/Chamika-n/ERP-Project-Hotel.git
   cd ERP-Project-Hotel
   ```

2. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

3. **Apply database migrations**
   ```bash
   dotnet ef database update
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```

   The application will be available at `https://localhost:5001` (or the configured port).

### Running Tests

```bash
dotnet test
```

## Database Configuration

By default, the application uses SQL Server LocalDB. The connection string is configured in `appsettings.json`:

```json
"ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=GrandHotelDB;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

To use a different database, update the connection string in `appsettings.json` or set the `ConnectionStrings__DefaultConnection` environment variable.

## Secrets Management

**⚠️ Important:** Do not commit sensitive information (passwords, API keys, connection strings with credentials) to version control.

For development, use [User Secrets](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets):

```bash
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "your-connection-string"
```

## Frontend Integration Status

**Note:** The room management frontend in `grand_hotel_room_management_frontend/` is currently a standalone prototype using localStorage.

### Integration Roadmap

- [ ] Move frontend assets to `wwwroot/`
- [ ] Convert HTML prototype to Razor views (`.cshtml`)
- [ ] Connect frontend to RoomController API endpoints
- [ ] Replace localStorage with database calls
- [ ] Integrate with main application build

## Contributing

When contributing to this project:

1. Write code in **English** (comments, variables, etc.)
2. Follow [C# coding conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
3. Create a feature branch from `main`
4. Commit with clear, descriptive messages
5. Submit a pull request for review

## Troubleshooting

### Database Migration Issues

```bash
# Check pending migrations
dotnet ef migrations list

# Add a new migration
dotnet ef migrations add MigrationName

# Remove the last migration
dotnet ef migrations remove
```

### Build or Runtime Errors

1. Clean and rebuild:
   ```bash
   dotnet clean
   dotnet build
   ```

2. Clear NuGet cache if package-related:
   ```bash
   dotnet nuget locals all --clear
   ```

## License

[Add your license here]

## Team

Member contributions:
- Chamika-n - Lead developer

---

For more information or questions, please open an issue in the repository.
