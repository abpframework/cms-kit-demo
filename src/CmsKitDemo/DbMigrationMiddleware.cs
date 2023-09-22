using Microsoft.Extensions.Options;
using System.Data.Common;
using System.Net;
using System.Text;
using System.Text.Unicode;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Threading;

namespace CmsKitDemo
{
    public class DbMigrationMiddleware : IMiddleware, ITransientDependency
    {
        private readonly IConnectionStringResolver _connectionStringResolver;
        private readonly IConfiguration _configuration;
        private readonly SemaphoreSlim _semaphore;

        public DbMigrationMiddleware(IConnectionStringResolver connectionStringResolver, IConfiguration configuration)
        {
            _connectionStringResolver = connectionStringResolver;
            _configuration = configuration;
            _semaphore = new SemaphoreSlim(1, 1);
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var connString = await _connectionStringResolver.ResolveAsync();
            var dbFilePath = connString.Replace("Data Source=", "").Replace(";Cache=Shared", ""); ;

            if(ShouldCreateDemoDatabase(dbFilePath))
            {
                await CreateDemoDatabaseAsync(dbFilePath);
            }

            await next(context);
        }

        private string GetDefaultDbFilePath()
        {
            var dbFolder = _configuration["App:SqliteDbFolder"]?.EnsureEndsWith(Path.DirectorySeparatorChar);
            
            return $"{dbFolder}{_configuration["App:DefaultDbName"]}.db";
        }

        private bool ShouldCreateDemoDatabase(string dbFilePath)
        {
            if (!File.Exists(dbFilePath))
            {
                return true;
            }

            return new FileInfo(dbFilePath!).Length <= 0;
        }

        private async Task CreateDemoDatabaseAsync(string newDbFilePath)
        {
            await _semaphore.WaitAsync();

            try
            {
                var defaultDbFilePath = GetDefaultDbFilePath();

                //copy the existing db file to the target 
                File.Copy(defaultDbFilePath, newDbFilePath, overwrite: true);
            }
            catch(Exception ex)
            {
                //ignore
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }

    [Dependency(ReplaceServices = true)]
    public class DemoConnectionStringResolver : DefaultConnectionStringResolver
    {
        private readonly IConfiguration _configuration;
        private readonly IMyResolver _resolver;

        public DemoConnectionStringResolver(IOptionsMonitor<AbpDbConnectionOptions> options, IConfiguration configuration, IMyResolver resolver) : base(options)
        {
            _configuration = configuration;
            _resolver = resolver;
        }

        [Obsolete("Use ResolveAsync method.")]
        public override string Resolve(string connectionStringName = null)
        {
            return AsyncHelper.RunSync(() => ResolveInternalAsync(connectionStringName));
        }

        public async override Task<string> ResolveAsync(string connectionStringName = null)
        {
            return await ResolveInternalAsync(connectionStringName);
        }

        private async Task<string> ResolveInternalAsync(string connectionStringName = null)
        {
            var dbFolder = _configuration["App:SqliteDbFolder"]?.EnsureEndsWith(Path.DirectorySeparatorChar);
            if (dbFolder.IsNullOrWhiteSpace())
            {
                return await base.ResolveAsync(connectionStringName);
            }

            string demoUserId = _configuration["App:DefaultDbName"]!;
            //string demoUserId = _resolver.GetDemoUserIdOrNull() ?? _configuration["App:DefaultDbName"]!;

            var dbFilePath = $"{dbFolder}{demoUserId}.db";
            var connString = $"Data Source={dbFilePath};Cache=Shared";

            return connString;
        }
    }

    public interface IMyResolver : ITransientDependency
    {
        public string GetDemoUserIdOrNull();
    }

    public class MyResolver : IMyResolver, ITransientDependency
    {
        public const string DemoUserCookieName = "CmsKitDemoUser";

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IGuidGenerator _guidGenerator;


        public MyResolver(IHttpContextAccessor httpContextAccessor, IGuidGenerator guidGenerator)
        {
            _httpContextAccessor = httpContextAccessor;
            _guidGenerator = guidGenerator;
        }

        public string GetDemoUserIdOrNull()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                return null;
            }

            var demoUserId = httpContext.Items[DemoUserCookieName] as string;
            if (demoUserId != null)
            {
                return demoUserId;
            }

            demoUserId = httpContext.Request?.Cookies[DemoUserCookieName];
            if (demoUserId == null)
            {
                demoUserId = _guidGenerator.Create().ToString();
            }

            SetDemoUserCookie(demoUserId);
            httpContext.Items[DemoUserCookieName] = demoUserId;
            return demoUserId;
        }

        private void SetDemoUserCookie(string value)
        {
            var option = new CookieOptions
            {
                Expires = DateTime.Now.AddMonths(12)
            };

            _httpContextAccessor.HttpContext?.Response.Cookies.Append(DemoUserCookieName, value, option);
        }
    }
}
