using JetBrains.Annotations;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Content;

namespace CmsKitDemo.Services.Dtos
{
    public class CreateUpdateGalleryImageDto
    {
        [NotNull]
        [StringLength(CmsKitDemoConsts.MaxDescriptionLength)]
        public string Description { get; set; }

        public Guid CoverImageMediaId { get; set; }
    }
}
