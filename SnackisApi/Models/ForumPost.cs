using SnackisApi.Data;

namespace SnackisApi.Models;

public class ForumPost
{
    public int Id { get; set; }
    public string Body { get; set; } = "";
    public DateTime Date { get; set; } = DateTime.Now;
    public int ThreadId { get; set; }
    public ForumThread Thread { get; set; }
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
}
