using SnackisApi.Models;

namespace SnackisApi.Interfaces;

public interface IForumPostRepository
{
    Task<IEnumerable<ForumPost>> GetAllPostsAsync();
    Task<ForumPost?> GetPostByIdAsync(int id);
    Task<ForumPost> AddAsync(ForumPost forumPost);
    Task UpdateAsync(ForumPost forumPost);
    Task DeleteAsync(int id);
}
