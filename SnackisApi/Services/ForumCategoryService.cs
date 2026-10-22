using Microsoft.EntityFrameworkCore;
using SnackisApi.Data;
using SnackisApi.Interfaces;
using SnackisApi.Models;

namespace SnackisApi.Services;

public class ForumCategoryService(IForumCategoryRepository repository) : IForumCategoryService
{
    public async Task<IEnumerable<ForumCategory>> GetAllCategoriesAsync() =>
        await repository.GetAllCategoriesAsync();

    public async Task<ForumCategory?> GetCategoryByIdAsync(int id) =>
        await repository.GetCategoryByIdAsync(id);

    public async Task<ForumCategory> AddAsync(ForumCategory forumCategory) =>
        await repository.AddAsync(forumCategory);

    public async Task DeleteAsync(int id) =>
        await repository.DeleteAsync(id);

    public async Task UpdateAsync(ForumCategory forumCategory) =>
        await repository.UpdateAsync(forumCategory);
    
    public async Task<Boolean> ExistsAsync(int id) =>
        await repository.ExistsAsync(id);
}
