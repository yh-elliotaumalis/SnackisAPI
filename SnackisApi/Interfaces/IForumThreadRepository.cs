using SnackisApi.Models;

namespace SnackisApi.Interfaces;

public interface IForumThreadRepository
{
    Task<IEnumerable<ForumThread>> GetAllThreadsAsync();
    Task<ForumThread?> GetThreadByIdAsync(int id);
    Task<ForumThread> AddAsync(ForumThread forumThread);
    Task UpdateAsync(ForumThread forumThread);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int threadId);

}
