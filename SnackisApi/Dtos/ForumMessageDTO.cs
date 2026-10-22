using System.ComponentModel.DataAnnotations;

namespace SnackisApi.Dtos;

public class ForumMessageDTO
{
    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public DateTime Date { get; set; }
    public PublicUserDTO Sender { get; set; } = null!;
    public PublicUserDTO Recipient { get; set; } = null!;
}

public class CreateMessageRequestDTO
{
    [Required(AllowEmptyStrings = false)]
    public string RecipientId { get; set; } = null!;
    [Required(AllowEmptyStrings = false), MinLength(1), MaxLength(10000)]
    public string Content { get; set; } = null!;
}
