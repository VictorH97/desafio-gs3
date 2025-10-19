# Aviadores API

This is an ASP.NET Core Web API following the MVC pattern. It includes User and Aircraft domains, with controllers and repositories using Entity Framework Core.

## Features
- UserController with login endpoint
- AircraftController with CRUD endpoints
- Clean architecture and separation of concerns

## Getting Started
1. Ensure you have the .NET SDK installed.
2. Restore dependencies:
   ```sh
   dotnet restore
   ```
3. Build the project:
   ```sh
   dotnet build
   ```
4. Run the API:
   ```sh
   dotnet run
   ```

4. Publish the API:
   ```sh
   dotnet publish -c Release -o ./publish  
   ```

## Project Structure
- `Controllers/` - API controllers
- `Models/` - Domain models
- `Repositories/` - Data access logic
- `Data/` - EF Core DbContext

## Next Steps
- Implement User and Aircraft domains
- Add authentication logic for login
- Implement CRUD for Aircraft

---
Generated with GitHub Copilot
