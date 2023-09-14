using CmsKitDemo.Permissions;
using CmsKitDemo.Services;
using CmsKitDemo.Services.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CmsKitDemo.Pages.Gallery.Management
{
    [Authorize(CmsKitDemoPermissions.GalleryImage.Create)]
    public class CreateModalModel : PageModel
    {
        [BindProperty]
        public CreateUpdateGalleryImageDto Image { get; set; }

        private readonly IImageGalleryAppService _imageGalleryAppService;

        public CreateModalModel(IImageGalleryAppService imageGalleryAppService)
        {
            _imageGalleryAppService = imageGalleryAppService;
        }

        public void OnGet()
        {
        }

        public async Task OnPostAsync()
        {
            await _imageGalleryAppService.CreateAsync(Image);
        }
    }
}
