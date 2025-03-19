using SultanShipping.Entities.consts.enums;
using System.ComponentModel.DataAnnotations;

namespace SultanShipping.Entities;

public class MasterShipment
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(30)]
    public string TrackingNumber { get; set; }

    [Required]
    public DateTime ExpectedDeliveryDate { get; set; }

    [Required]
    [MaxLength(100)]
    public string Destination { get; set; }

    [Required]
    public ShipmentStatus Status { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [MaxLength(50)]
    public string CreatedBy { get; set; }

    public bool IsDeleted { get; set; } = false;

    public virtual ICollection<CustomerShipment> CustomerShipments { get; set; }
    public virtual ICollection<ShipmentUpdate> Updates { get; set; }
}

