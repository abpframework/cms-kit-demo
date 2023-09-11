using CmsKitDemo.Entities;
using CmsKitDemo.Permissions;
using CmsKitDemo.Services.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace CmsKitDemo.Services
{
    public class ImageGalleryAppService :
        CrudAppService<GalleryImage, GalleryImageDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateGalleryImageDto, CreateUpdateGalleryImageDto>,
        IImageGalleryAppService
    {
        public ImageGalleryAppService(IRepository<GalleryImage, Guid> repository) : base(repository)
        {
            CreatePolicyName = CmsKitDemoPermissions.GalleryImage.Create;
            UpdatePolicyName = CmsKitDemoPermissions.GalleryImage.Update;
            DeletePolicyName = CmsKitDemoPermissions.GalleryImage.Delete;
        }
    }
}
