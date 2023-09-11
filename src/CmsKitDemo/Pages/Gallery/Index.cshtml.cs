using CmsKitDemo.Services;
using CmsKitDemo.Services.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp.Application.Dtos;

namespace CmsKitDemo.Pages.Gallery
{
    public class ImageGalleryModel : PageModel
    {
        public IReadOnlyList<GalleryImageDto> Images { get; set; }

        private readonly IImageGalleryAppService _imageGalleryAppService;

        public ImageGalleryModel(IImageGalleryAppService imageGalleryAppService)
        {
            _imageGalleryAppService = imageGalleryAppService;
        }
        
        public async Task OnGetAsync()
        {
            Images = (await _imageGalleryAppService.GetListAsync(new PagedAndSortedResultRequestDto())).Items;
        }
    }
}
