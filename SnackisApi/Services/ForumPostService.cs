using Microsoft.EntityFrameworkCore;
using SnackisApi.Data;
using SnackisApi.Interfaces;
using SnackisApi.Models;

namespace SnackisApi.Services;

public class ForumPostService(IForumPostRepository repository) : IForumPostService
{
    public async Task<IEnumerable<ForumPost>> GetAllPostsAsync() =>
        await repository.GetAllPostsAsync();

    public async Task<ForumPost?> GetPostByIdAsync(int id) =>
        await repository.GetPostByIdAsync(id);

    public async Task<ForumPost> AddAsync(ForumPost forumPost) =>
        await repository.AddAsync(forumPost);

    public async Task DeleteAsync(int id) =>
        await repository.DeleteAsync(id);

    public async Task UpdateAsync(ForumPost forumPost) =>
        await repository.UpdateAsync(forumPost);

    public async Task<ForumPost> CreatePost(int threadId, string body, string userId)
    {
        var post = new ForumPost
        {
            ThreadId = threadId,
            Body = body,
            UserId = userId,
        };

        return await AddAsync(post);
    }
}
