using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SnackisApi.Data;
using SnackisApi.Dtos;

[Route("api/users")]
[ApiController]
public class ForumUsersController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    public ForumUsersController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    // GET: api/users
    [HttpGet, Authorize]
    public async Task<ActionResult<IEnumerable<PublicUserDTO>>> GetForumUsers()
    {
        var users = await _userManager.Users.ToListAsync();
        var dtos = users.Select(u => new PublicUserDTO
        {
            Id = u.Id,
            Name = u.Name ?? u.UserName,
            ProfileImage = u.ProfileImage
        }).ToList();

        return Ok(dtos);
    }

    // GET: api/users/5
    [HttpGet("{id}")]
    public async Task<ActionResult<PublicUserDTO>> GetForumUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
        {
            return NotFound();
        }

        var dto = new PublicUserDTO
        {
            Id = user.Id,
            Name = user.Name ?? user.UserName,
            ProfileImage = user.ProfileImage
        };

        return Ok(dto);
    }
}
