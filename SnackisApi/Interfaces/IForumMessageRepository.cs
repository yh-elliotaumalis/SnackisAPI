using SnackisApi.Models;

namespace SnackisApi.Interfaces;

public interface IForumMessageRepository
{
    Task<IEnumerable<ForumMessage>> GetMessagesByUserIdAsync(string userId);
    Task<ForumMessage?> GetMessageByIdAsync(int id);
    Task<ForumMessage> AddAsync(ForumMessage forumMessage);
    Task DeleteAsync(int id);
}
