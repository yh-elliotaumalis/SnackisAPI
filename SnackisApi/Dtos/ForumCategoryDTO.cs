using System.ComponentModel.DataAnnotations;

namespace SnackisApi.Dtos;

public class CreateForumCategoryDTO
{
    [Required(AllowEmptyStrings = false), MinLength(3), MaxLength(100)]
    public string Name { get; set; } = null!;
    [Required(AllowEmptyStrings = false), MinLength(3), MaxLength(1000)]
    public string Topic { get; set; } = null!;
    public int? Priority { get; set; } = 0;
    public int? ParentCategoryId { get; set; } = null!;
}

public class UpdateForumCategoryDTO
{
    public int Id { get; set; }
    [Required(AllowEmptyStrings = false), MinLength(3), MaxLength(100)]
    public string Name { get; set; } = null!;
    [Required(AllowEmptyStrings = false), MinLength(3), MaxLength(1000)]
    public string Topic { get; set; } = null!;
    public int? Priority { get; set; } = null!;
    public int? ParentCategoryId { get; set; } = null!;
}

public class ForumCategoryDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Topic { get; set; } = null!;
    public int? Priority { get; set; } = null!;
    public int? ParentCategoryId { get; set; } = null!;
}