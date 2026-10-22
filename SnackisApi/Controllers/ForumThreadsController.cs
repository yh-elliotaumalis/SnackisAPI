using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SnackisApi.Models;
using SnackisApi.Data;
using SnackisApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using SnackisApi.Dtos;
using System.Security.Claims;

[Route("api/threads")]
[ApiController]
public class ForumThreadsController : ControllerBase
{
    private readonly IForumThreadService _threadService;
    private readonly IForumCategoryService _categoryService;
    public ForumThreadsController(IForumThreadService threadService, IForumCategoryService categoryService)
    {
        _threadService = threadService;
        _categoryService = categoryService;
    }

    // GET: api/threads
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ForumThreadDTO>>> GetForumThreads()
    {
        var threads = await _threadService.GetAllThreadsAsync();
        var dtos = threads.Select(t => new ForumThreadDTO
        {
            Id = t.Id,
            Title = t.Title,
            Date = t.Date,
            CategoryId = t.CategoryId,
            UserId = t.UserId,
            UserName = t.User?.Name ?? t.User?.UserName,
            ProfileImage = t.User?.ProfileImage
        }).ToList();


        return Ok(dtos);
    }

    // GET: api/threads/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ForumThreadDTO>> GetForumThread(int id)
    {
        var forumThread = await _threadService.GetThreadByIdAsync(id);

        if (forumThread == null)
        {
            return NotFound();
        }

        var postDtos = forumThread.Posts?.Select(p => new ForumPostDTO
        {
            Id = p.Id,
            Body = p.Body,
            Date = p.Date,
            UserId = p.UserId,
            UserName = p.User?.Name ?? p.User?.UserName,
            ProfileImage = p.User?.ProfileImage
        }).ToList();

        var threadDto = new ForumThreadDTO
        {
            Id = forumThread.Id,
            Title = forumThread.Title,
            Date = forumThread.Date,
            CategoryId = forumThread.CategoryId,
            UserId = forumThread.UserId,
            UserName = forumThread.User?.Name ?? forumThread.User?.UserName,
            ProfileImage = forumThread.User?.ProfileImage,
            Posts = postDtos,
        };

        return Ok(threadDto);
    }

    // PUT: api/threads/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}"), Authorize]
    public async Task<IActionResult> PutForumThread(int? id, UpdateThreadRequestDTO updateRequest)
    {
        if (id != updateRequest.Id)
        {
            return BadRequest();
        }

        var thread = await _threadService.GetThreadByIdAsync(id.Value);
        if (thread is null)
        {
            return NotFound();
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (thread.UserId != userId && !User.IsInRole("Admin"))
        {
            return Forbid();
        }

        thread.Title = updateRequest.Title.Trim();

        await _threadService.UpdateAsync(thread);

        return NoContent();
    }

    // POST: api/threads
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost, Authorize]
    public async Task<ActionResult<ForumThreadDTO>> PostForumThread(CreateThreadRequestDTO thread)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var threadTitle = thread.Title.Trim();
        var threadContent = thread.Content.Trim();
        var threadCategoryId = thread.CategoryId;
        
        if (!await _categoryService.ExistsAsync(threadCategoryId))
        {
            return BadRequest("Category does not exist.");
        }

        var forumThread = await _threadService.CreateThread(threadTitle, threadContent, userId, threadCategoryId);

        var threadDto = new ForumThreadDTO
        {
            Id = forumThread.Id,
            Title = forumThread.Title,
            Date = forumThread.Date,
            CategoryId = forumThread.CategoryId,
            UserId = forumThread.UserId,
        };

        return Ok(threadDto);
    }

    // DELETE: api/threads/5
    [HttpDelete("{id}"), Authorize]
    public async Task<IActionResult> DeleteForumThread(int id)
    {
        var forumThread = await _threadService.GetThreadByIdAsync(id);

        if (forumThread == null)
        {
            return NotFound();
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (forumThread.UserId != userId && !User.IsInRole("Admin"))
        {
            return Forbid();
        }

        await _threadService.DeleteAsync(forumThread.Id);

        return NoContent();
    }
}
