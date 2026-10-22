using System.ComponentModel.DataAnnotations;

namespace SnackisApi.Dtos;

public class CreateThreadRequestDTO
{
    [Required(AllowEmptyStrings = false), MinLength(3), MaxLength(100)]
    public string Title { get; set; } = "";
    [Required(AllowEmptyStrings = false), MinLength(3), MaxLength(10000)]
    public string Content { get; set; } = "";
    public int CategoryId { get; set; }
}

public class UpdateThreadRequestDTO
{
    public int Id { get; set; }

    [Required(AllowEmptyStrings = false), MinLength(3), MaxLength(100)]
    public string Title { get; set; } = "";
}

public class ForumThreadDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public DateTime Date { get; set; }
    public int CategoryId { get; set; }
    public string UserId { get; set; } = "";
    public string UserName { get; set; } = "";
    public string? ProfileImage { get; set; }
    public List<ForumPostDTO> Posts { get; set; } = new List<ForumPostDTO>();
}