using SultanShipping.Entities.consts.enums;

namespace SultanShipping.Contracts.Shipments
{
    public class CustomerShipmentUpdateResponse
    {
        public int Id { get; set; }
        public DateTime UpdateDate { get; set; } = DateTime.UtcNow;
        public string Location { get; set; } = null!;

        public string Notes { get; set; } = null!;

        public ShipmentStatus Status { get; set; }

        public string UpdatedBy { get; set; } = null !;
    }
}
