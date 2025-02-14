using Microsoft.AspNetCore.Identity;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.Threading;

namespace CmsKitDemo.Data;

public class CmsKitDemoDataSeedContributor :IDataSeedContributor, ITransientDependency
{
    private readonly IdentityUserManager _userManager;
    private readonly IConfiguration _configuration;
    private static readonly SemaphoreSlim Semaphore = new SemaphoreSlim(1, 1);
    private static bool _isSeeded;

    public CmsKitDemoDataSeedContributor(IdentityUserManager userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public virtual async Task SeedAsync(DataSeedContext context)
    {
        if(_isSeeded)
        {
            return;
        }
        
        using (await Semaphore.LockAsync())
        {
            _isSeeded = true;
            var adminUser = await _userManager.FindByNameAsync(IdentityDataSeedContributor.AdminUserNameDefaultValue);
            if (adminUser != null)
            {
                var password = _configuration["App:DefaultAdminPassword"]!;
                if (await _userManager.CheckPasswordAsync(adminUser, password))
                {
                    return;
                }
            
                (await _userManager.RemovePasswordAsync(adminUser)).CheckErrors();
                (await _userManager.AddPasswordAsync(adminUser, password)).CheckErrors();
            }
        }
        
    }
}