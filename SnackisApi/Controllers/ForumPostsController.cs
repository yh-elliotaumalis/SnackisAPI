using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SnackisApi.Models;
using SnackisApi.Data;
using SnackisApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using SnackisApi.Dtos;
using System.Security.Claims;

[Route("api/posts")]
[ApiController]
public class ForumPostsController : ControllerBase
{
    private readonly IForumThreadService _threadService;
    private readonly IForumPostService _postService;
    public ForumPostsController(IForumThreadService threadService, IForumPostService postService)
    {
        _threadService = threadService;
        _postService = postService;
    }


    // GET: api/posts/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ForumPostDTO>> GetForumPost(int id)
    {
        var forumPost = await _postService.GetPostByIdAsync(id);

        if (forumPost == null)
        {
            return NotFound();
        }

        var postDto = new ForumPostDTO
        {
            Id = forumPost.Id,
            Date = forumPost.Date,
            UserId = forumPost.UserId,
            UserName = forumPost.User?.UserName,
            ProfileImage = forumPost.User?.ProfileImage,
            Body = forumPost.Body
        };

        return Ok(postDto);
    }

    // PUT: api/posts/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}"), Authorize]
    public async Task<IActionResult> PutForumPost(int? id, UpdatePostRequestDTO updateRequest)
    {
        if (id != updateRequest.Id)
        {
            return BadRequest();
        }

        var post = await _postService.GetPostByIdAsync(id.Value);
        if (post is null)
        {
            return NotFound();
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (post.UserId != userId && !User.IsInRole("Admin"))
        {
            return Forbid();
        }

        post.Body = updateRequest.Body.Trim();

        await _postService.UpdateAsync(post);

        return NoContent();
    }

    // POST: api/posts
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost, Authorize]
    public async Task<ActionResult<ForumPostDTO>> PostForumPost(CreatePostRequestDTO post)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var postBody = post.Body.Trim();
        var threadId = post.ThreadId;
        
        if (!await _threadService.ExistsAsync(threadId))
        {
            return BadRequest("Thread does not exist.");
        }

        var forumPost = await _postService.CreatePost(threadId, postBody, userId);

        var postDto = new ForumPostDTO
        {
            Id = forumPost.Id,
            Date = forumPost.Date,
            UserId = forumPost.UserId,
            UserName = forumPost.User?.UserName,
            ProfileImage = forumPost.User?.ProfileImage,
            Body = forumPost.Body
        };

        return Ok(postDto);
    }

    // DELETE: api/posts/5
    [HttpDelete("{id}"), Authorize]
    public async Task<IActionResult> DeleteForumPost(int id)
    {
        var forumPost = await _postService.GetPostByIdAsync(id);

        if (forumPost == null)
        {
            return NotFound();
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (forumPost.UserId != userId && !User.IsInRole("Admin"))
        {
            return Forbid();
        }

        await _postService.DeleteAsync(forumPost.Id);

        return NoContent();
    }
}
