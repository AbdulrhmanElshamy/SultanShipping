using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SultanShipping.Entities;

public class NotificationLog
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string CustomerId { get; set; }

    [ForeignKey("CustomerId")]
    public virtual ApplicationUser Customer { get; set; }

    [Required]
    public DateTime SentDate { get; set; } = DateTime.UtcNow;

    [Required]
    [MaxLength(100)]
    public string Subject { get; set; }

    [Required]
    public string Content { get; set; }

    public bool IsSuccess { get; set; }

    [MaxLength(500)]
    public string ErrorMessage { get; set; }
}

