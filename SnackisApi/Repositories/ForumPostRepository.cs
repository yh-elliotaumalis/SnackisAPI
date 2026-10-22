using Microsoft.EntityFrameworkCore;
using SnackisApi.Data;
using SnackisApi.Interfaces;
using SnackisApi.Models;

namespace SnackisApi.Repositories;

public class ForumPostRepository(ApplicationDbContext context) : IForumPostRepository
{
    public async Task<IEnumerable<ForumPost>> GetAllPostsAsync()
    {
        return await context.Posts
            .ToListAsync();
    }

    public async Task<ForumPost?> GetPostByIdAsync(int id)
    {
        return await context.Posts
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<ForumPost> AddAsync(ForumPost forumPost)
    {
        var post = context.Posts.Add(forumPost);
        await context.SaveChangesAsync();
        return post.Entity;
    }

    public async Task DeleteAsync(int id)
    {
        var post = await GetPostByIdAsync(id);
        if (post is null) return;

        context.Posts.Remove(post);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ForumPost forumPost)
    {
        context.Posts.Update(forumPost);
        await context.SaveChangesAsync();
    }
}
