using SnackisApi.Models;

namespace SnackisApi.Interfaces;

public interface IForumThreadService
{
    Task<IEnumerable<ForumThread>> GetAllThreadsAsync();
    Task<ForumThread?> GetThreadByIdAsync(int id);
    Task<ForumThread> CreateThread(string title, string body, string userId, int categoryId);
    Task<ForumThread> AddAsync(ForumThread forumThread);
    Task UpdateAsync(ForumThread forumThread);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int threadId);
}
