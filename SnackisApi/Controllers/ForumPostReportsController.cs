using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SnackisApi.Data;
using SnackisApi.Dtos;
using SnackisApi.Interfaces;
using SnackisApi.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

[Route("api/reports")]
[ApiController]
public class ForumPostReportsController : ControllerBase
{
    private readonly IForumPostReportService _reportService;
    private readonly IForumPostService _postService;
    public ForumPostReportsController(IForumPostReportService reportService, IForumPostService postService)
    {
        _reportService = reportService;
        _postService = postService;
    }

    // GET: api/reports
    [HttpGet, Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<ForumPostReportDTO>>> GetForumPostReports()
    {
        var reports = await _reportService.GetAllReportsAsync();
        var dtos = reports.Select(r => new ForumPostReportDTO
        {
            Id = r.Id,
            ForumPost = new ForumPostDTO
            {
                Id = r.ForumPost.Id,
                Body = r.ForumPost.Body,
                Date = r.ForumPost.Date,
                UserId = r.ForumPost.UserId,
                UserName = r.ForumPost.User?.Name ?? r.ForumPost.User?.UserName,
                ProfileImage = r.ForumPost.User?.ProfileImage
            },
            Reporter = new PublicUserDTO
            {
                Id = r.Reporter.Id,
                Name = r.Reporter.Name ?? r.Reporter.UserName
            },
            Reason = r.Reason,
            ThreadTitle = r.ForumPost.Thread?.Title,
            Date = r.Date
        }).ToList();


        return Ok(dtos);
    }

    // GET: api/reports/5
    [HttpGet("{id}"), Authorize(Roles = "Admin")]
    public async Task<ActionResult<ForumPostReportDTO>> GetForumPostReport(int id)
    {
        var report = await _reportService.GetReportByIdAsync(id);

        if (report == null)
        {
            return NotFound();
        }

        var dto = new ForumPostReportDTO
        {
            Id = report.Id,
            ForumPost = new ForumPostDTO
            {
                Id = report.ForumPost.Id,
                Body = report.ForumPost.Body,
                Date = report.ForumPost.Date,
                UserId = report.ForumPost.UserId,
                UserName = report.ForumPost.User?.Name ?? report.ForumPost.User?.UserName,
                ProfileImage = report.ForumPost.User?.ProfileImage
            },
            Reporter = new PublicUserDTO
            {
                Id = report.Reporter.Id,
                Name = report.Reporter.Name ?? report.Reporter.UserName
            },
            ThreadTitle = report.ForumPost.Thread?.Title,
            Reason = report.Reason,
            Date = report.Date
        };
        return Ok(dto);
    }

    // POST: api/reports
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost, Authorize]
    public async Task<ActionResult<ForumPostReportDTO>> PostForumPostReport(CreateForumPostReportDTO report)
    {

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var post = await _postService.GetPostByIdAsync(report.ForumPostId);
        if (post is null)
        {
            return BadRequest("Forum post not found.");
        }

        var reportReason = report.Reason.Trim();

        var newReport = await _reportService.CreateReport(post.Id, userId, reportReason);

        var reportDto = new ForumPostReportDTO
        {
            Id = newReport.Id,
            ForumPost = new ForumPostDTO
            {
                Id = newReport.ForumPost.Id,
                Body = newReport.ForumPost.Body,
                Date = newReport.ForumPost.Date,
                UserId = newReport.ForumPost.UserId,
                UserName = newReport.ForumPost.User?.Name ?? newReport.ForumPost.User?.UserName,
                ProfileImage = newReport.ForumPost.User?.ProfileImage
            },
            Reason = newReport.Reason,
            Date = newReport.Date
        };

        return Ok(reportDto);

    }

    // DELETE: api/reports/5
    [HttpDelete("{id}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteForumPostReport(int id)
    {
        var report = await _reportService.GetReportByIdAsync(id);
        if (report is null)
        {
            return NotFound();
        }

        await _reportService.DeleteAsync(report.Id);

        return NoContent();
    }
}
