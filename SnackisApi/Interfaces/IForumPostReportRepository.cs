using SnackisApi.Models;

namespace SnackisApi.Interfaces;

public interface IForumPostReportRepository
{
    Task<IEnumerable<ForumPostReport>> GetAllReportsAsync();
    Task<ForumPostReport?> GetReportByIdAsync(int id);
    Task<ForumPostReport> AddAsync(ForumPostReport forumPostReport);
    Task UpdateAsync(ForumPostReport forumPostReport);
    Task DeleteAsync(int id);
}
