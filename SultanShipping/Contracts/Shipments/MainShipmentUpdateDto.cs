using SultanShipping.Entities.consts.enums;

namespace SultanShipping.Contracts.Shipments
{
    public class MainShipmentUpdateDto
    {
        public DateTime ExpectedDeliveryDate { get; set; }
        public string Destination { get; set; }
        public ShipmentStatus Status { get; set; }
    }
}
