# HavayarQuiz

## How To Add New Migration?

### Example: Adding Migration

Using EF Core command line tool;

````bash
dotnet ef migrations add migrationName -p src/HavayarQuiz.Persistence.MsSql.Migrations/ -s src/HavayarQuiz.Persistence.MsSql.Migrations/
````

Using Visual Studio Package Manager Console;

````bash
Add-Migration migrationName -ProjectName src\HavayarQuiz.Persistence.MsSql.Migrations -StartUpProjectName src\HavayarQuiz.Persistence.MsSql.Migrations
````


## Updating the Databases

Using EF Core command line tool;
````bash
dotnet ef database update -p src/HavayarQuiz.Persistence.MsSql.Migrations/ -s src/HavayarQuiz.Persistence.MsSql.Migrations/
````


Using Visual Studio Package Manager Console;
````bash
Update-Database -ProjectName src\HavayarQuiz.Persistence.MsSql.Migrations -StartUpProjectName src\HavayarQuiz.Persistence.MsSql.Migrations
````
