using SultanShipping.Entities.consts.enums;

namespace SultanShipping.Contracts.Shipments
{
    public class TrackShipmentResponse
    {
        public string TrackingNumber { get; set; } = null!;

        public DateTime ExpectedDeliveryDate { get; set; }

        public string Destination { get; set; }  = null!;

        public string DeliveryAddress { get; set; } = null!;

        public ShipmentStatus Status { get; set; } = ShipmentStatus.InProgress;

        public bool IsCancelled { get; set; } = false;

        public string CancellationReason { get; set; } = null!;

        public DateTime? CancellationDate { get; set; }


        public virtual string CustomerName { get; set; } = null!;

        public virtual ICollection<CustomerShipmentUpdateResponse> CustomerUpdates { get; set; } = new List<CustomerShipmentUpdateResponse>();
    }
}
