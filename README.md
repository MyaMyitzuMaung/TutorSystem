# 🎓 TutorSystem

Small educational management system (ASP.NET Core Web API + EF Core, Database-First).

## Project overview
- Backend: ASP.NET Core Web API (C#)
- Database: SQL Server (Database-First approach)
- EF Core models generated with `dotnet ef dbcontext scaffold`

## Database
- Database file (exported from SSMS): `TutorSystem.sql` (run this in SSMS to create the database and seed data)
- Database name used: `TutorSystemDB`

### Files included
- `TutorSystem.sql` — full DB creation script (schema + data)
- `AppDbContextModels/` — (optional) EF scaffold folder (may be excluded from repo)
- `README.md` — this file

## EF Core scaffold command
Run this from your Database project folder (adjust connection string as needed):

```bash
dotnet ef dbcontext scaffold "Server=.;Database=TutorSystemDB;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o AppDbContextModels -c AppDbContext -f
```

- `-o AppDbContextModels` → output folder for generated entities and DbContext  
- `-c AppDbContext` → name of the DbContext class to generate  
- `-f` → force overwrite existing generated files

## How to run locally (developer steps)

1. Ensure SQL Server is running on your machine (or update the connection string to your server).
2. Open **SQL Server Management Studio (SSMS)**, run `TutorSystem.sql` to create the database (if not already).
3. Open the solution in Visual Studio.
4. Update `appsettings.json` or `appsettings.Development.json` connection string (do **not** commit `appsettings.Development.json` to Git).
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=.;Database=TutorSystemDB;Trusted_Connection=True;TrustServerCertificate=True;"
   }
   ```
5. (Optional) If you changed DB schema, regenerate EF models:
   ```bash
   cd TutorSystem.Database
   dotnet ef dbcontext scaffold "Server=.;Database=TutorSystemDB;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o AppDbContextModels -c AppDbContext -f
   ```
6. Build and run the API project from Visual Studio or:
   ```bash
   dotnet build
   dotnet run --project TutorSystem.WebApi
   ```

## API Endpoints (examples)
- `GET /api/Teacher`
- `POST /api/Teacher`
- `PATCH /api/Teacher/{id}`
- `DELETE /api/Teacher/{id}`
- Similar endpoints for `Course` and `StudentLevel`

## Notes & best practices
- Keep secrets (DB passwords) out of repo. Use `appsettings.json` and add it to `.gitignore`.
- If you accidentally commit secrets, rotate them immediately.
- Use `CourseId` (int) for relations instead of string `CourseType` when you are ready.

## Development tips
- Use Postman or Swagger UI to test endpoints.
- Use feature branches for new work: `git checkout -b feature/xxxx`
- Commit often and push to GitHub: `git add .`, `git commit -m "message"`, `git push`

----
