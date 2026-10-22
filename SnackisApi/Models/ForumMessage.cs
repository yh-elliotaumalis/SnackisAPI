using SnackisApi.Data;

namespace SnackisApi.Models;

public class ForumMessage
{
    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public DateTime Date { get; set; } = DateTime.Now;
    public string SenderId { get; set; } = null!;
    public ApplicationUser Sender { get; set; } = null!;
    public string RecipientId { get; set; } = null!;
    public ApplicationUser Recipient { get; set; } = null!;
}
