namespace SultanShipping.Contracts.Shipments
{
    public class ShipmentTrackingDto
    {
        public string TrackingNumber { get; set; }
        public string CustomerName { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public string CurrentLocation { get; set; }
        public string CurrentStatus { get; set; }
        public string DeliveryAddress { get; set; }
        public string Status { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string Notes { get; set; }
        public ICollection<StatusHistoryDto> StatusHistory { get; set; }
    }
}
