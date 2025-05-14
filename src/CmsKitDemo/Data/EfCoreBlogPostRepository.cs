using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.CmsKit.Blogs;
using Volo.CmsKit.EntityFrameworkCore;
using Volo.CmsKit.MarkedItems;
using Volo.CmsKit.Tags;
using Volo.CmsKit.Users;

namespace CmsKitDemo.Data;

//Remove this when https://github.com/abpframework/abp/pull/22113 is merged and released
[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IBlogPostRepository))]
public class CmsKitDemoEfCoreBlogPostRepository : EfCoreBlogPostRepository
{
    private readonly EntityTagManager _entityTagManager;

    public CmsKitDemoEfCoreBlogPostRepository(
        IDbContextProvider<ICmsKitDbContext> dbContextProvider,
        MarkedItemManager markedItemManager,
        EntityTagManager entityTagManager)
        : base(dbContextProvider, markedItemManager, entityTagManager)
    {
        _entityTagManager = entityTagManager;
    }

    public override async Task<List<BlogPost>> GetListAsync(
        string? filter = null,
        Guid? blogId = null,
        Guid? authorId = null,
        Guid? tagId = null,
        Guid? favoriteUserId = null,
        BlogPostStatus? statusFilter = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        string? sorting = null,
        CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();
        var blogPostsDbSet = dbContext.Set<BlogPost>();
        var usersDbSet = dbContext.Set<CmsUser>();

        List<Guid> entityIdFilters = null;
        if (tagId.HasValue)
        {
            entityIdFilters = (await _entityTagManager.GetEntityIdsFilteredByTagAsync(tagId.Value, CurrentTenant.Id, cancellationToken)).Select(Guid.Parse).ToList();
        }

        var queryable = (await GetDbSetAsync())
            .WhereIf(entityIdFilters != null, x => entityIdFilters.Contains(x.Id))
            .WhereIf(blogId.HasValue, x => x.BlogId == blogId)
            .WhereIf(!string.IsNullOrWhiteSpace(filter), x => x.Title.Contains(filter) || x.Slug.Contains(filter))
            .WhereIf(authorId.HasValue, x => x.AuthorId == authorId)
            .WhereIf(statusFilter.HasValue, x => x.Status == statusFilter);

        queryable = queryable.OrderBy(sorting.IsNullOrEmpty() ? $"{nameof(BlogPost.CreationTime)} desc" : sorting);

        var combinedResult = await queryable
            .Join(
                usersDbSet,
                o => o.AuthorId,
                i => i.Id,
                (blogPost, user) => new { blogPost, user })
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync(GetCancellationToken(cancellationToken));

        return combinedResult.Select(s =>
        {
            s.blogPost.Author = s.user;
            return s.blogPost;
        }).ToList();
    }
}
