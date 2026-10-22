using Microsoft.EntityFrameworkCore;
using SnackisApi.Data;
using SnackisApi.Interfaces;
using SnackisApi.Models;

namespace SnackisApi.Services;

public class ForumPostReportService(IForumPostReportRepository repository) : IForumPostReportService
{
    public async Task<IEnumerable<ForumPostReport>> GetAllReportsAsync() =>
        await repository.GetAllReportsAsync();

    public async Task<ForumPostReport?> GetReportByIdAsync(int id) =>
        await repository.GetReportByIdAsync(id);

    public async Task<ForumPostReport> AddAsync(ForumPostReport forumPostReport) =>
        await repository.AddAsync(forumPostReport);

    public async Task DeleteAsync(int id) =>
        await repository.DeleteAsync(id);

    public async Task UpdateAsync(ForumPostReport forumPostReport) =>
        await repository.UpdateAsync(forumPostReport);

    public async Task<ForumPostReport> CreateReport(int forumPostId, string userId, string reason)
    {
        var report = new ForumPostReport
        {
            ForumPostId = forumPostId,
            ReporterId = userId,
            Reason = reason,
        };

        return await AddAsync(report);
    }
}
