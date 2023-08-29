using Microsoft.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;

namespace CmsKitDemo.Data;

public class CmsKitDemoEFCoreDbSchemaMigrator : ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public CmsKitDemoEFCoreDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the CmsKitDemoDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<CmsKitDemoDbContext>()
            .Database
            .MigrateAsync();
    }
}
