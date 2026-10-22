using SnackisApi.Data;

namespace SnackisApi.Models;

public class ForumThread
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public int CategoryId { get; set; }
    public ForumCategory Category { get; set; }
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
    public List<ForumPost> Posts { get; set; } = new List<ForumPost>();
}
