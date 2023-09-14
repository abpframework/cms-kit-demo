using CmsKitDemo.Entities;
using CmsKitDemo.Permissions;
using CmsKitDemo.Services.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.CmsKit.Comments;
using Volo.CmsKit.Reactions;

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

        public async Task<List<GalleryImageWithDetailsDto>> GetDetailedListAsync()
        {
            var dbContext = await Repository.GetDbContextAsync();

            var images = await (from image in dbContext.Set<GalleryImage>() 
                                select image).ToListAsync();

            return images.Select(x => new GalleryImageWithDetailsDto
            {
                Id = x.Id,
                Description = x.Description,
                CoverImageMediaId = x.CoverImageMediaId,

                CommentCount = (from comment in dbContext.Set<Comment>()
                                where comment.EntityType == CmsKitDemoConsts.ImageGalleryEntityType && comment.EntityId == x.Id.ToString()
                                select comment).Count(),

                LikeCount = (from reaction in dbContext.Set<UserReaction>()
                             where reaction.EntityType == CmsKitDemoConsts.ImageGalleryEntityType && reaction.EntityId == x.Id.ToString()
                             select reaction).Count()
            }).ToList();
        }
    }
}
