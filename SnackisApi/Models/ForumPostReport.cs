using SnackisApi.Data;

namespace SnackisApi.Models;

public class ForumPostReport
{
    public int Id { get; set; }
    public int ForumPostId { get; set; }
    public ForumPost ForumPost { get; set; } = null!;
    public string ReporterId { get; set; } = null!;
    public ApplicationUser Reporter { get; set; } = null!;
    public string Reason { get; set; } = null!;
    public DateTime Date { get; set; } = DateTime.Now;
}
