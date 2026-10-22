using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SnackisApi.Data;
using SnackisApi.Dtos;
using SnackisApi.Interfaces;
using SnackisApi.Models;
using System.Security.Claims;

[Route("api/messages")]
[ApiController]
public class ForumMessagesController : ControllerBase
{
    private readonly IForumMessageService _messageService;
    private readonly UserManager<ApplicationUser> _userManager;
    public ForumMessagesController(IForumMessageService messageService, UserManager<ApplicationUser> userManager)
    {
        _messageService = messageService;
        _userManager = userManager;
    }

    // GET: api/messages
    [HttpGet, Authorize]
    public async Task<ActionResult<IEnumerable<ForumMessageDTO>>> GetForumMessages()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var messages = await _messageService.GetMessagesByUserIdAsync(userId);
        var dtos = messages.Select(m => new ForumMessageDTO
        {
            Id = m.Id,
            Content = m.Content,
            Date = m.Date,
            Sender = new PublicUserDTO
            {
                Id = m.Sender.Id,
                Name = m.Sender.Name ?? m.Sender.UserName,
                ProfileImage = m.Sender.ProfileImage
            },
            Recipient = new PublicUserDTO
            {
                Id = m.Recipient.Id,
                Name = m.Recipient.Name ?? m.Recipient.UserName,
                ProfileImage = m.Recipient.ProfileImage
            }
        }).ToList();

        return Ok(dtos);
    }

    // GET: api/messages/5
    [HttpGet("{id}"), Authorize]
    public async Task<ActionResult<ForumMessageDTO>> GetForumMessage(int id)
    {
        var message = await _messageService.GetMessageByIdAsync(id);
        if (message is null)
        {
            return NotFound();
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (message.SenderId != userId && message.RecipientId != userId)
        {
            return Forbid();
        }

        var dto = new ForumMessageDTO
        {
            Id = message.Id,
            Content = message.Content,
            Date = message.Date,
            Sender = new PublicUserDTO
            {
                Id = message.Sender.Id,
                Name = message.Sender.Name ?? message.Sender.UserName,
                ProfileImage = message.Sender.ProfileImage
            },
            Recipient = new PublicUserDTO
            {
                Id = message.Recipient.Id,
                Name = message.Recipient.Name ?? message.Recipient.UserName,
                ProfileImage = message.Recipient.ProfileImage
            }
        };

        return Ok(dto);
    }

    // POST: api/messages
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost, Authorize]
    public async Task<ActionResult<ForumMessageDTO>> PostForumMessage(CreateMessageRequestDTO message)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var messageContent = message.Content.Trim();

        var recipient = await _userManager.FindByIdAsync(message.RecipientId);
        if (recipient is null)
        {
            return BadRequest("Invalid recipient.");
        }

        var forumMessage = await _messageService.CreateMessage(userId, recipient.Id, messageContent);
        var createdMessage = await _messageService.GetMessageByIdAsync(forumMessage.Id);

        var dto = new ForumMessageDTO
        {
            Id = createdMessage.Id,
            Content = createdMessage.Content,
            Date = createdMessage.Date,
            Sender = new PublicUserDTO
            {
                Id = createdMessage.Sender.Id,
                Name = createdMessage.Sender.Name ?? createdMessage.Sender.UserName,
                ProfileImage = createdMessage.Sender.ProfileImage
            },
            Recipient = new PublicUserDTO
            {
                Id = createdMessage.Recipient.Id,
                Name = createdMessage.Recipient.Name ?? createdMessage.Recipient.UserName,
                ProfileImage = createdMessage.Recipient.ProfileImage
            }
        };

        return Ok(dto);
    }

    // DELETE: api/messages/5
    [HttpDelete("{id}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteForumMessage(int id)
    {
        var message = await _messageService.GetMessageByIdAsync(id);
        if (message is null)
        {
            return NotFound();
        }

        await _messageService.DeleteAsync(message.Id);

        return NoContent();
    }
}
