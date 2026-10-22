namespace SnackisApi.Models;

public class ForumCategory
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? ParentCategoryId { get; set; }
    public ForumCategory? ParentCategory { get; set; }
    public string? Topic { get; set; }
    public int Priority { get; set; } = 0;
    public List<ForumThread> Threads { get; set; } = new List<ForumThread>();
}
