using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SnackisApi.Models;
using SnackisApi.Data;
using SnackisApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using SnackisApi.Dtos;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

[Route("api/me")]
[ApiController]
public class ForumMeController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IWebHostEnvironment _env;
    public ForumMeController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IWebHostEnvironment env)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _env = env;
    }

    // GET: api/me
    [HttpGet, Authorize]
    public async Task<ActionResult<UserDTO>> GetUser()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return NotFound();
        }

        var userDto = new UserDTO
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Roles = await _userManager.GetRolesAsync(user),
            ProfileImage = user.ProfileImage,
        };

        return Ok(userDto);
    }

    // PUT: api/me
    [HttpPut, Authorize]
    public async Task<ActionResult<UserDTO>> UpdateUser(UpdateUserRequestDTO updateRequest)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return NotFound();
        }

        user.Name = updateRequest.Name;

        await _userManager.UpdateAsync(user);

        var userDto = new UserDTO
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Roles = await _userManager.GetRolesAsync(user),
            ProfileImage = user.ProfileImage,
        };

        return Ok(userDto);
    }

    // PUT: api/me/image
    [HttpPut("image"), Authorize]
    public async Task<ActionResult<UserDTO>> UpdateProfileImage(IFormFile image)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return NotFound();
        }

        var fileName = Random.Shared.Next(0, 100000).ToString() + Path.GetExtension(image.FileName).ToLowerInvariant();
        if (!(fileName.EndsWith(".jpg") || fileName.EndsWith(".jpeg") || fileName.EndsWith(".png") || fileName.EndsWith(".webp") || fileName.EndsWith(".gif")))
        {
            return BadRequest("Only jpg, png, webp and gif images are allowed.");
        }

        if (image.Length > 1024 * 1024 * 4)
        {
            return BadRequest("Image is too large.");
        }

        string folderPath = Path.Combine(_env.WebRootPath, "userImages");

        using (var fileStream = new FileStream(Path.Combine(folderPath, fileName), FileMode.Create))
        {
            await image.CopyToAsync(fileStream);
        }

        user.ProfileImage = fileName;

        await _userManager.UpdateAsync(user);

        var userDto = new UserDTO
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Roles = await _userManager.GetRolesAsync(user),
            ProfileImage = user.ProfileImage,
        };

        return Ok(userDto);
    }
}