using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Volo.Abp.Account.Settings;
using Volo.Abp.Account.Web;
using Volo.Abp.Account.Web.Pages.Account;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.Settings;

namespace CmsKitDemo.Pages.Account;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(LoginModel))]
public class CmsKitLoginModel : LoginModel
{
    private readonly IHostEnvironment _environment;
    
    public CmsKitLoginModel(
        IAuthenticationSchemeProvider schemeProvider, 
        IOptions<AbpAccountOptions> accountOptions,
        IOptions<IdentityOptions> identityOptions,
        IdentityDynamicClaimsPrincipalContributorCache identityDynamicClaimsPrincipalContributorCache,
        IHostEnvironment environment)
        : base(
        schemeProvider,
        accountOptions,
        identityOptions,
        identityDynamicClaimsPrincipalContributorCache)
    {
        _environment = environment;
    }

    public override async Task<IActionResult> OnPostAsync(string action)
    {
        await CheckLocalLoginAsync();

        ValidateModel();

        ExternalProviders = await GetExternalProviders();

        EnableLocalLogin = await SettingProvider.IsTrueAsync(AccountSettingNames.EnableLocalLogin);

        await IdentityOptions.SetAsync();

        if (_environment.IsProduction())
        {
            if(LoginInput.UserNameOrEmailAddress is IdentityDataSeedContributor.AdminEmailDefaultValue or IdentityDataSeedContributor.AdminUserNameDefaultValue)
            {
                Alerts.Danger("In this demo, access to the admin panel is disabled. If you want to see the admin panel, download the project from GitHub and run it yourself: https://github.com/abpframework/cms-kit-demo");
                return Page();
            }
        }
        
        return await base.OnPostAsync(action);
    }
}