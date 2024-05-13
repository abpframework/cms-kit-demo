using Microsoft.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;

namespace CmsKitDemo.Data;

public class CmsKitDemoEFCoreDbSchemaMigrator : ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public CmsKitDemoEFCoreDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync(string? connectionString = null)
    {
        if (connectionString.IsNullOrWhiteSpace())
        {
            await _serviceProvider.GetRequiredService<CmsKitDemoDbContext>().Database.MigrateAsync();
            return;
        }

        var options = new DbContextOptionsBuilder<CmsKitDemoDbContext>()
                .UseSqlite(connectionString)
                .Options;
        
        using (var dbContext = new CmsKitDemoDbContext(options))
        {
            await dbContext.Database.MigrateAsync();
        }
    }
}