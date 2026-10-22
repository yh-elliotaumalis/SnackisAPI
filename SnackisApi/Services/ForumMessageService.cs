using SnackisApi.Interfaces;
using SnackisApi.Models;

namespace SnackisApi.Services;

public class ForumMessageService(IForumMessageRepository repository) : IForumMessageService
{
    public async Task<IEnumerable<ForumMessage>> GetMessagesByUserIdAsync(string userId) =>
        await repository.GetMessagesByUserIdAsync(userId);

    public async Task<ForumMessage?> GetMessageByIdAsync(int id) =>
        await repository.GetMessageByIdAsync(id);

    public async Task<ForumMessage> AddAsync(ForumMessage forumMessage) =>
        await repository.AddAsync(forumMessage);

    public async Task DeleteAsync(int id) =>
        await repository.DeleteAsync(id);

    public async Task<ForumMessage> CreateMessage(string senderId, string recipientId, string content)
    {
        var message = new ForumMessage
        {
            SenderId = senderId,
            RecipientId = recipientId,
            Content = content,
        };

        return await AddAsync(message);
    }
}
