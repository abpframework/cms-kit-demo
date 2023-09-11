using CmsKitDemo.Services;
using CmsKitDemo.Services.Dtos;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CmsKitDemo.Pages.Gallery
{
    public class ImageGalleryModel : PageModel
    {
        public List<GalleryImageWithDetailsDto> Images { get; set; }

        private readonly IImageGalleryAppService _imageGalleryAppService;

        public ImageGalleryModel(IImageGalleryAppService imageGalleryAppService)
        {
            _imageGalleryAppService = imageGalleryAppService;
        }
        
        public async Task OnGetAsync()
        {
            Images = await _imageGalleryAppService.GetDetailedListAsync();
        }
    }
}
