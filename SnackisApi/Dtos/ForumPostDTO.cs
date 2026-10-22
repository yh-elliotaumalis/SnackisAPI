using System.ComponentModel.DataAnnotations;

namespace SnackisApi.Dtos;

public class ForumPostDTO
{
    public int Id { get; set; }
    public string Body { get; set; }
    public DateTime Date { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string? ProfileImage { get; set; }
}

public class CreatePostRequestDTO
{
    [Required(AllowEmptyStrings = false), MinLength(3), MaxLength(10000)]
    public string Body { get; set; } = "";
    public int ThreadId { get; set; }
}

public class UpdatePostRequestDTO
{
    public int Id { get; set; }
    [Required(AllowEmptyStrings = false), MinLength(3), MaxLength(10000)]
    public string Body { get; set; } = "";
}