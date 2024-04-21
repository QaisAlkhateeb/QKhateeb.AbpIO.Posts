using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Abp.ContributorPatternExample.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class ContributorPatternExampleDbContextFactory : IDesignTimeDbContextFactory<ContributorPatternExampleDbContext>
{
    public ContributorPatternExampleDbContext CreateDbContext(string[] args)
    {
        ContributorPatternExampleEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<ContributorPatternExampleDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new ContributorPatternExampleDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Abp.ContributorPatternExample.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
