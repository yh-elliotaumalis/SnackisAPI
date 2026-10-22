using Microsoft.EntityFrameworkCore;
using SnackisApi.Data;
using SnackisApi.Interfaces;
using SnackisApi.Models;

namespace SnackisApi.Repositories;

public class ForumPostReportRepository(ApplicationDbContext context) : IForumPostReportRepository
{
    public async Task<IEnumerable<ForumPostReport>> GetAllReportsAsync()
    {
        return await context.PostReports
            .Include(r => r.ForumPost)
            .ThenInclude(p => p.User)
            .Include(r => r.ForumPost)
            .ThenInclude(p => p.Thread)
            .Include(r => r.Reporter)
            .ToListAsync();
    }

    public async Task<ForumPostReport?> GetReportByIdAsync(int id)
    {
        return await context.PostReports
            .Include(r => r.ForumPost)
            .ThenInclude(p => p.User)
            .Include(r => r.ForumPost)
            .ThenInclude(p => p.Thread)
            .Include(r => r.Reporter)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<ForumPostReport> AddAsync(ForumPostReport forumPostReport)
    {
        var report = context.PostReports.Add(forumPostReport);
        await context.SaveChangesAsync();
        return report.Entity;
    }

    public async Task UpdateAsync(ForumPostReport forumPostReport)
    {
        context.PostReports.Update(forumPostReport);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var report = await context.PostReports.FirstOrDefaultAsync(r => r.Id == id);
        if (report is null) return;

        context.PostReports.Remove(report);
        await context.SaveChangesAsync();
    }
}
