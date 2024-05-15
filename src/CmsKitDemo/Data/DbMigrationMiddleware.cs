using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace CmsKitDemo.Data;

public class DbMigrationMiddleware : IMiddleware, ITransientDependency
{
    private readonly CmsKitDemoDbMigrationService _dbMigrationService;

    private readonly IConnectionStringResolver _connectionStringResolver;

    public DbMigrationMiddleware(CmsKitDemoDbMigrationService dbMigrationService, IConnectionStringResolver connectionStringResolver)
    {
        _dbMigrationService = dbMigrationService;
        _connectionStringResolver = connectionStringResolver;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var connString = await _connectionStringResolver.ResolveAsync();
        var dbFilePath = connString.Replace("Data Source=", "").Replace(";Cache=Shared", "");
        
        if (ShouldCreateUserDb(context, dbFilePath) || !context.Request.Cookies.ContainsKey("CMSKitDemoDbMigrated"))
        {
            await _dbMigrationService.MigrateAsync(connString);
            context.Response.Cookies.Append("CMSKitDemoDbMigrated", "true");
        }
            
        await next(context);
    }
    
    private static bool ShouldCreateUserDb(HttpContext context, string dbFilePath)
    {
        if (!context.Request.Headers.ContainsKey("User-Agent"))
        {
            return !File.Exists(dbFilePath);
        }

        var userAgent = context.Request.Headers.FirstOrDefault(e=> e.Key == "User-Agent");
        if (CmsKitDemoConsts.IgnoredUserAgents.Any(ignoredUserAgent => userAgent.Value.ToString().Contains(ignoredUserAgent)))
        {
            return false;
        }

        return !File.Exists(dbFilePath);
    }
}