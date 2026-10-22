using SnackisApi.Models;

namespace SnackisApi.Interfaces;

public interface IForumMessageService
{
    Task<IEnumerable<ForumMessage>> GetMessagesByUserIdAsync(string userId);
    Task<ForumMessage?> GetMessageByIdAsync(int id);
    Task<ForumMessage> CreateMessage(string senderId, string recipientId, string content);
    Task<ForumMessage> AddAsync(ForumMessage forumMessage);
    Task DeleteAsync(int id);
}
