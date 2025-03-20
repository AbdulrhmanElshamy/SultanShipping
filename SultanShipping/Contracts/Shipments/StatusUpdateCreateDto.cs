using SultanShipping.Entities.consts.enums;

namespace SultanShipping.Contracts.Shipments
{
    public class StatusUpdateCreateDto
    {
        public string Location { get; set; }
        public string Notes { get; set; }

        public ShipmentStatus status { get; set; }
    }
}
