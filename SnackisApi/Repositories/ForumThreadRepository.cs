using Microsoft.EntityFrameworkCore;
using SnackisApi.Data;
using SnackisApi.Interfaces;
using SnackisApi.Models;

namespace SnackisApi.Repositories;

public class ForumThreadRepository(ApplicationDbContext context) : IForumThreadRepository
{
    public async Task<IEnumerable<ForumThread>> GetAllThreadsAsync()
    {
        return await context.Threads
            .Include(thread => thread.User)
            .ToListAsync();
    }

    public async Task<ForumThread?> GetThreadByIdAsync(int id)
    {
        return await context.Threads
            .Include(thread => thread.User)
            .Include(thread => thread.Posts)
            .ThenInclude(post => post.User)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<ForumThread> AddAsync(ForumThread forumThread)
    {
        var thread = context.Threads.Add(forumThread);
        await context.SaveChangesAsync();
        return thread.Entity;
    }

    public async Task DeleteAsync(int id)
    {
        var thread = await GetThreadByIdAsync(id);
        if (thread is null) return;

        context.Threads.Remove(thread);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ForumThread forumThread)
    {
        context.Threads.Update(forumThread);
        await context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int threadId)
    {
        return await context.Threads.AnyAsync(t => t.Id == threadId);
    }
}
