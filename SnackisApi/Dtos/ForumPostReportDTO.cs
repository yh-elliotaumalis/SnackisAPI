using SnackisApi.Data;
using SnackisApi.Models;
using System.ComponentModel.DataAnnotations;

namespace SnackisApi.Dtos;

public class ForumPostReportDTO
{
    public int Id { get; set; }
    public ForumPostDTO ForumPost { get; set; } = null!;
    public PublicUserDTO Reporter { get; set; } = null!;
    public string Reason { get; set; } = null!;
    public DateTime Date { get; set; } = DateTime.Now;
    public string? ThreadTitle { get; internal set; }
}

public class CreateForumPostReportDTO
{
    public int ForumPostId { get; set; }
    [Required(AllowEmptyStrings = false), MinLength(3), MaxLength(1000)]
    public string Reason { get; set; } = null!;
}
