using SnackisApi.Models;

namespace SnackisApi.Interfaces;

public interface IForumCategoryRepository
{
    Task<IEnumerable<ForumCategory>> GetAllCategoriesAsync();
    Task<ForumCategory?> GetCategoryByIdAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<ForumCategory> AddAsync(ForumCategory forumCategory);
    Task UpdateAsync(ForumCategory forumCategory);
    Task DeleteAsync(int id);
}
