using SnackisApi.Models;

namespace SnackisApi.Interfaces;

public interface IForumPostReportService
{
    Task<IEnumerable<ForumPostReport>> GetAllReportsAsync();
    Task<ForumPostReport?> GetReportByIdAsync(int id);
    Task<ForumPostReport> CreateReport(int forumPostId, string userId, string reason);
    Task<ForumPostReport> AddAsync(ForumPostReport forumPostReport);
    Task UpdateAsync(ForumPostReport forumPostReport);
    Task DeleteAsync(int id);
}
