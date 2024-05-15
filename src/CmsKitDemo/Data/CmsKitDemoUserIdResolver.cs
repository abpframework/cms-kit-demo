using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;

namespace CmsKitDemo.Data;

public class CmsKitDemoUserIdResolver : ITransientDependency
{
    private const string DemoUserCookieName = "CmsKitDemoUser";

    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IGuidGenerator _guidGenerator;

    public CmsKitDemoUserIdResolver(IHttpContextAccessor httpContextAccessor, IGuidGenerator guidGenerator)
    {
        _httpContextAccessor = httpContextAccessor;
        _guidGenerator = guidGenerator;
    }
    
    public string? GetDemoUserIdOrNull()
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

        demoUserId = GetDemoUserFromQueryStringOrNull(httpContext) ?? GetDemoUserFromCookieOrNull(httpContext);
        if (demoUserId == null)
        {
            demoUserId = _guidGenerator.Create().ToString();
        }

        SetDemoUserCookie(demoUserId);
        httpContext.Items[DemoUserCookieName] = demoUserId;
        return demoUserId;
    }

    private static string? GetDemoUserFromCookieOrNull(HttpContext httpContext)
    {
        return httpContext.Request?.Cookies[DemoUserCookieName];
    }

    private static string? GetDemoUserFromQueryStringOrNull(HttpContext httpContext)
    {
        return httpContext.Request?.Query[DemoUserCookieName];
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