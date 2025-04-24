using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp;
using JetBrains.Annotations;

namespace CmsKitDemo.Entities
{
    public class GalleryImage : CreationAuditedAggregateRoot<Guid>
    {
        public string Description { get; set; }

        public Guid CoverImageMediaId { get; set; }
        
        public string CommentsSummary { get; set; }

        protected GalleryImage()
        {
        }

        public GalleryImage(Guid id, Guid coverImageMediaId, [NotNull] string description) : base(id)
        {
            CoverImageMediaId = coverImageMediaId;
            Description = Check.NotNullOrWhiteSpace(description, nameof(description), maxLength: CmsKitDemoConsts.MaxDescriptionLength);
        }
    }
}
