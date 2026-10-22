using Microsoft.EntityFrameworkCore;
using SnackisApi.Data;
using SnackisApi.Interfaces;
using SnackisApi.Models;

namespace SnackisApi.Repositories;

public class ForumCategoryRepository(ApplicationDbContext context) : IForumCategoryRepository
{
    public async Task<IEnumerable<ForumCategory>> GetAllCategoriesAsync()
    {
        return await context.Categories
            .OrderByDescending(category => category.Priority)
            .ToListAsync();
    }

    public async Task<ForumCategory?> GetCategoryByIdAsync(int id)
    {
        return await context.Categories.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<ForumCategory> AddAsync(ForumCategory forumCategory)
    {
        var category = context.Categories.Add(forumCategory);
        await context.SaveChangesAsync();
        return category.Entity;
    }

    public async Task DeleteAsync(int id)
    {
        var category = await GetCategoryByIdAsync(id);
        if (category is null) return;

        context.Categories.Remove(category);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ForumCategory forumCategory)
    {
        context.Categories.Update(forumCategory);
        await context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await context.Categories.AnyAsync(c => c.Id == id);
    }
}
