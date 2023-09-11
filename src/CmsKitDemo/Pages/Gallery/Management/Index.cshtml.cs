using CmsKitDemo.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CmsKitDemo.Pages.Gallery.Management
{
    [Authorize(CmsKitDemoPermissions.GalleryImage.Management)]
    public class ManagementModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
