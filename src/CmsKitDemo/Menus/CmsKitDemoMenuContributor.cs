using CmsKitDemo.Localization;
using CmsKitDemo.Permissions;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace CmsKitDemo.Menus;

public class CmsKitDemoMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<CmsKitDemoResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                CmsKitDemoMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fas fa-home",
                order: 0
            )
        );

        if (CmsKitDemoModule.IsMultiTenant)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        //Image Gallery Management Page
        context.Menu.Items.Add(
            new ApplicationMenuItem(CmsKitDemoMenus.GalleryImage.ImageMenus, l["Images"], "/ImageManagement", icon: "fa fa-palette")
                .RequirePermissions(CmsKitDemoPermissions.GalleryImage.Management)
        );

        return Task.CompletedTask;
    }
}
