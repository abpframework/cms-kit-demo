using CmsKitDemo.Services.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CmsKitDemo.Services
{
    public interface IImageGalleryAppService : ICrudAppService<GalleryImageDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateGalleryImageDto, CreateUpdateGalleryImageDto>
    {
    }
}
