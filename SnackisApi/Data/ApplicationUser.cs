using Microsoft.AspNetCore.Identity;

namespace SnackisApi.Data;

public class ApplicationUser : IdentityUser
{
    public string? Name { get; set; }
    public string? ProfileImage { get; set; }
}
