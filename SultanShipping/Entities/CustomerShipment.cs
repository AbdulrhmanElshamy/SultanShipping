using SultanShipping.Entities.consts.enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SultanShipping.Entities;

public class CustomerShipment
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(30)]
    public string TrackingNumber { get; set; }

    [Required]
    [MaxLength(255)]
    public string DeliveryAddress { get; set; }

    [Required]
    public ShipmentStatus Status { get; set; } = ShipmentStatus.InProgress;

    public bool IsCancelled { get; set; } = false;

    [MaxLength(500)]
    public string? CancellationReason { get; set; }

    public DateTime? CancellationDate { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int MasterShipmentId { get; set; }

    [ForeignKey("MasterShipmentId")]
    public virtual MasterShipment MasterShipment { get; set; }

    public string CustomerId { get; set; }

    [ForeignKey("CustomerId")]
    public virtual ApplicationUser Customer { get; set; }

    public virtual ICollection<CustomerShipmentUpdate> CustomerUpdates { get; set; }
}

