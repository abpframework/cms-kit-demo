using CmsKitDemo.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace CmsKitDemo.Permissions
{
    public class CmsKitDemoPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(CmsKitDemoPermissions.GalleryImage.Root, L("Permission:GalleryImage"));

            myGroup.AddPermission(CmsKitDemoPermissions.GalleryImage.Management, L("Permission:GalleryImageManagement"));
            myGroup.AddPermission(CmsKitDemoPermissions.GalleryImage.Create, L("Permission:Create"));
            myGroup.AddPermission(CmsKitDemoPermissions.GalleryImage.Update, L("Permission:Edit"));
            myGroup.AddPermission(CmsKitDemoPermissions.GalleryImage.Delete, L("Permission:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<CmsKitDemoResource>(name);
        }
    }
}
