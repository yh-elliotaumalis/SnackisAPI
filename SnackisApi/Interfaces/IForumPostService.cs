using SnackisApi.Data;
using SnackisApi.Models;

namespace SnackisApi.Interfaces;

public interface IForumPostService
{
    Task<IEnumerable<ForumPost>> GetAllPostsAsync();
    Task<ForumPost?> GetPostByIdAsync(int id);
    Task<ForumPost> CreatePost(int threadId, string content, string userId);
    Task<ForumPost> AddAsync(ForumPost forumPost);
    Task UpdateAsync(ForumPost forumPost);
    Task DeleteAsync(int id);
}
