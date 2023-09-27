using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CmsKitDemo.Data;

public class CmsKitDemoDbContextFactory : IDesignTimeDbContextFactory<CmsKitDemoDbContext>
{
    public CmsKitDemoDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var dbFolder = configuration["App:SqliteDbFolder"]?.EnsureEndsWith(Path.DirectorySeparatorChar);

        var builder = new DbContextOptionsBuilder<CmsKitDemoDbContext>()
            .UseSqlite($"Data Source={dbFolder}{configuration["App:DefaultDbName"]}.db");

        return new CmsKitDemoDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
