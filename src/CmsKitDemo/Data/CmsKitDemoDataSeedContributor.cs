using Microsoft.AspNetCore.Identity;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;

namespace CmsKitDemo.Data;

public class CmsKitDemoDataSeedContributor :IDataSeedContributor, ITransientDependency
{
    private readonly IdentityUserManager _userManager;
    private readonly IConfiguration _configuration;

    public CmsKitDemoDataSeedContributor(IdentityUserManager userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public virtual async Task SeedAsync(DataSeedContext context)
    {
        var adminUser = await _userManager.FindByNameAsync(IdentityDataSeedContributor.AdminUserNameDefaultValue);
        if (adminUser != null)
        {
            (await _userManager.RemovePasswordAsync(adminUser)).CheckErrors();
            (await _userManager.AddPasswordAsync(adminUser, _configuration["App:DefaultAdminPassword"]!)).CheckErrors();
        }
    }
}