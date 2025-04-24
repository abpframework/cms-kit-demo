using CmsKitDemo.Entities;
using CmsKitDemo.Utils;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus;
using Volo.CmsKit.Comments;

namespace CmsKitDemo.EventHandlers;

public class GalleryImageCommentListener : ILocalEventHandler<EntityChangedEventData<Comment>>, ITransientDependency
{
    private readonly IRepository<GalleryImage, Guid> _galleryImageRepository;
    private readonly IRepository<Comment, Guid> _commentRepository;
    private readonly AiCommentSummarizer _aiCommentSummarizer;

    public GalleryImageCommentListener(
        IRepository<GalleryImage, Guid> galleryImageRepository,
        IRepository<Comment, Guid> commentRepository,
        AiCommentSummarizer aiCommentSummarizer)
    {
        _galleryImageRepository = galleryImageRepository;
        _commentRepository = commentRepository;
        _aiCommentSummarizer = aiCommentSummarizer;
    }
    
    public async Task HandleEventAsync(EntityChangedEventData<Comment> eventData)
    {
        var comment = eventData.Entity;
        
        //Here, we only interest in comments related to image gallery items
        if (comment.EntityType != CmsKitDemoConsts.ImageGalleryEntityType)
        {
            return;
        }

        if (!Guid.TryParse(comment.EntityId, out var galleryImageId))
        {
            return;
        }
        
        // Get the related image from database
        var galleryImage = await _galleryImageRepository.FindAsync(galleryImageId);
        if (galleryImage == null)
        {
            return;
        }
        
        // Get all the comments related to the image
        var queryable = await _commentRepository.GetQueryableAsync();
        var allCommentTexts = await queryable
            .Where(c => c.EntityType == CmsKitDemoConsts.ImageGalleryEntityType && c.EntityId == comment.EntityId)
            .Select(c => c.Text)
            .ToArrayAsync();

        // Update the summary of comments related to the image
        if (allCommentTexts.Length <= 0)
        {
            galleryImage.CommentsSummary = "";
        }
        else
        {
            galleryImage.CommentsSummary = await _aiCommentSummarizer.SummarizeAsync(allCommentTexts);
        }

        // Update the image in database
        await _galleryImageRepository.UpdateAsync(galleryImage);
    }
}