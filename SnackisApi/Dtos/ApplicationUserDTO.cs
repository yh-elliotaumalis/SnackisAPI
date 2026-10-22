using System.ComponentModel.DataAnnotations;

namespace SnackisApi.Dtos;

public class PublicUserDTO
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? ProfileImage { get; set; }
}

public class UserDTO
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public IList<string> Roles { get; set; } = null!;
    public string? ProfileImage { get; set; }
}

public class UpdateUserRequestDTO
{
    [Required, MinLength(3), MaxLength(30)]
    public string Name { get; set; } = null!;
}