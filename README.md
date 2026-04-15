# Travel Booking App (.NET 8)

A full-stack travel booking application built using **.NET 8** following **Clean Architecture** principles.
## Features
* Hotel search and filtering
* Booking management (Create, Edit, Delete)
* Customer management (demo data)
* Currency conversion (INR → USD using external API)
* Layered architecture (Domain, Application, Infrastructure, API, Web)
## Tech Stack
* ASP.NET Core MVC
* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* jQuery / AJAX
## Architecture
* **Domain** → Core entities
* **Application** → Business logic & DTOs
* **Infrastructure** → Database (EF Core)
* **API** → REST endpoints
* **Web** → UI (MVC)
## Setup Instructions

1. Clone repository
2. Add your connection string in `appsettings.json`
3. Run migrations:

   ```
   dotnet ef database update
   ```
4. Run project:

   ```
   dotnet run
   ```

## Notes
* API keys are removed for security
* Static data used for demo purposes
## Author
Purvi Mistry
