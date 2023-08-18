using HavayarQuiz.Persistence.MsSql.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Reflection;

namespace HavayarQuiz.Persistence.MsSql.Migrations;

internal class HavayarQuizContextFactory : IDesignTimeDbContextFactory<HavayarQuizContext>
{
    public HavayarQuizContext CreateDbContext(string[] args)
    {
        var connStr = "Server=(localdb)\\MSSQLLocalDB;Database=HavayarQuiz;Trusted_Connection=True;MultipleActiveResultSets=true";

        if (args is not null && args.Length > 0 && args[0].StartsWith("conn="))
            connStr = args[0][5..];

        var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;

        var optionsBuilder = new DbContextOptionsBuilder<HavayarQuizContext>()
            .UseSqlServer(connStr, opts =>
                opts
                    .MigrationsAssembly(assemblyName)
                    .MigrationsHistoryTable("__EFMigrationsHistory", HavayarQuizContext.DEFAULT_SCHEMA));

        return new HavayarQuizContext(optionsBuilder.Options);
    }
}
