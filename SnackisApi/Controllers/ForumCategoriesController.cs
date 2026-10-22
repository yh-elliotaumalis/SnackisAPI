using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SnackisApi.Models;
using SnackisApi.Data;
using SnackisApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using SnackisApi.Dtos;

[Route("api/categories")]
[ApiController]
public class ForumCategoriesController : ControllerBase
{
    private readonly IForumThreadService _threadService;
    private readonly IForumCategoryService _categoryService;
    public ForumCategoriesController(IForumThreadService threadService, IForumCategoryService categoryService)
    {
        _threadService = threadService;
        _categoryService = categoryService;
    }

    // GET: api/categories
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ForumCategoryDTO>>> GetForumCategories()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        var dtos = categories.Select(c => new ForumCategoryDTO
        {
            Id = c.Id,
            Name = c.Name,
            ParentCategoryId = c.ParentCategoryId,
            Topic = c.Topic,
            Priority = c.Priority
        }).ToList();
        return Ok(dtos);
    }

    // PUT: api/categories/1
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> PutForumCategory(int? id, UpdateForumCategoryDTO updateRequest)
    {
        if (id != updateRequest.Id)
        {
            return BadRequest();
        }

        var category = await _categoryService.GetCategoryByIdAsync(id.Value);
        if (category is null)
        {
            return NotFound();
        }


        if (updateRequest.Name is not null)
        {
            category.Name = updateRequest.Name;
        }

        if (updateRequest.Priority is not null)
        {
            category.Priority = updateRequest.Priority.Value;
        }

        if (updateRequest.Topic is not null)
        {
            category.Topic = updateRequest.Topic;
        }

        if (updateRequest.ParentCategoryId is not null)
        {
            var parentCategory = await _categoryService.GetCategoryByIdAsync(updateRequest.ParentCategoryId.Value);
            if (parentCategory is null)
            {
                return BadRequest("Parent category does not exist.");
            }
            category.ParentCategoryId = updateRequest.ParentCategoryId;
        }

        await _categoryService.UpdateAsync(category);

        return NoContent();
    }

    // POST: api/categories
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost, Authorize(Roles = "Admin")]
    public async Task<ActionResult<ForumCategoryDTO>> PostForumCategory(CreateForumCategoryDTO createRequest)
    {
        if (createRequest.ParentCategoryId is not null)
        {
            var parentCategory = await _categoryService.GetCategoryByIdAsync(createRequest.ParentCategoryId.Value);
            if (parentCategory is null)
            {
                return BadRequest("Parent category does not exist.");
            }
        }

        var category = new ForumCategory
        {
            Name = createRequest.Name,
            Topic = createRequest.Topic,
            Priority = createRequest.Priority ?? 0,
            ParentCategoryId = createRequest.ParentCategoryId
        };

        await _categoryService.AddAsync(category);

        var dto = new ForumCategoryDTO
        {
            Id = category.Id,
            Name = category.Name,
            ParentCategoryId = category.ParentCategoryId,
            Topic = category.Topic,
            Priority = category.Priority
        };
        return Ok(dto);
    }


    // DELETE: api/categories/1
    [HttpDelete("{id}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteForumCategory(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);

        if (category == null)
        {
            return NotFound();
        }

        await _categoryService.DeleteAsync(category.Id);

        return NoContent();
    }
}
