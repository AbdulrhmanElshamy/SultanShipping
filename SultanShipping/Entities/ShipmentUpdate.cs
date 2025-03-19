using SultanShipping.Entities.consts.enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SultanShipping.Entities;

public class ShipmentUpdate
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime UpdateDate { get; set; } = DateTime.UtcNow;

    [Required]
    [MaxLength(100)]
    public string Location { get; set; }

    [MaxLength(500)]
    public string Notes { get; set; }

    [Required]
    public ShipmentStatus Status { get; set; }


    [MaxLength(50)]
    public string UpdatedBy { get; set; }

    public int MasterShipmentId { get; set; }

    [ForeignKey("MasterShipmentId")]
    public virtual MasterShipment MasterShipment { get; set; }
}

