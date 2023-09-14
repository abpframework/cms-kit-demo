using Volo.Abp.Application.Dtos;

namespace CmsKitDemo.Services.Dtos
{
    public class GalleryImageDto : CreationAuditedEntityDto<Guid>
    {
        public string Description { get; set; }
        public Guid CoverImageMediaId { get; set; }
    }
}
