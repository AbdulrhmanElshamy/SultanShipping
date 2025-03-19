namespace SultanShipping.Contracts.Shipments
{
    public class MainShipmentDto
    {
        public int Id { get; set; }
        public string TrackingNumber { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public string Destination { get; set; }
        public string Status { get; set; }
        public ICollection<ShipmentDto> CustomerShipments { get; set; }
        public ICollection<StatusUpdateDto> StatusUpdates { get; set; }
    }
}
