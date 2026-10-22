using Microsoft.EntityFrameworkCore;
using SnackisApi.Data;
using SnackisApi.Interfaces;
using SnackisApi.Models;

namespace SnackisApi.Repositories;

public class ForumMessageRepository(ApplicationDbContext context) : IForumMessageRepository
{
    public async Task<IEnumerable<ForumMessage>> GetMessagesByUserIdAsync(string userId)
    {
        return await context.Messages
            .Include(m => m.Sender)
            .Include(m => m.Recipient)
            .Where(m => m.SenderId == userId || m.RecipientId == userId)
            .OrderByDescending(m => m.Date)
            .ToListAsync();
    }

    public async Task<ForumMessage?> GetMessageByIdAsync(int id)
    {
        return await context.Messages
            .Include(m => m.Sender)
            .Include(m => m.Recipient)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<ForumMessage> AddAsync(ForumMessage forumMessage)
    {
        var message = context.Messages.Add(forumMessage);
        await context.SaveChangesAsync();
        return message.Entity;
    }

    public async Task DeleteAsync(int id)
    {
        var message = await context.Messages.FirstOrDefaultAsync(m => m.Id == id);
        if (message is null) return;

        context.Messages.Remove(message);
        await context.SaveChangesAsync();
    }
}
