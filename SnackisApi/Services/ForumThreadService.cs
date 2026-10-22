using Microsoft.EntityFrameworkCore;
using SnackisApi.Data;
using SnackisApi.Interfaces;
using SnackisApi.Models;

namespace SnackisApi.Services;

public class ForumThreadService(IForumThreadRepository repository) : IForumThreadService
{
    public async Task<IEnumerable<ForumThread>> GetAllThreadsAsync() =>
        await repository.GetAllThreadsAsync();

    public async Task<ForumThread?> GetThreadByIdAsync(int id) =>
        await repository.GetThreadByIdAsync(id);

    public async Task<ForumThread> AddAsync(ForumThread forumThread) =>
        await repository.AddAsync(forumThread);

    public async Task DeleteAsync(int id) =>
        await repository.DeleteAsync(id);

    public async Task UpdateAsync(ForumThread forumThread) =>
        await repository.UpdateAsync(forumThread);

    public async Task<ForumThread> CreateThread(string title, string body, string userId, int categoryId)
    {
        var thread = new ForumThread
        {
            Title = title,
            UserId = userId,
            CategoryId = categoryId,
            Posts = new List<ForumPost>
            {
                new ForumPost
                {
                    Body = body,
                    Date = DateTime.Now,
                    UserId = userId
                }
            }
        };

        return await AddAsync(thread);
    }

    public async Task<bool> ExistsAsync(int threadId) =>
        await repository.ExistsAsync(threadId);
}
