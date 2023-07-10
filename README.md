# HavayarQuiz

## How To Add New Migration?

Using Visual Studio Package Manager Console;


### Example: Adding Migration
````bash
dotnet ef migrations add migrationName -p src/HavayarQuiz.Persistence.MsSql.Migrations/ -s src/HavayarQuiz.Persistence.MsSql.Migrations/
````



## Updating the Databases

````bash
dotnet ef database update -p src/HavayarQuiz.Persistence.MsSql.Migrations/ -s src/HavayarQuiz.Persistence.MsSql.Migrations/
````