namespace SultanShipping.Contracts.Shipments
{
    public class ShipmentDto
    {
        public int Id { get; set; }
        public string TrackingNumber { get; set; }
        public int CustomerId { get; set; }
        public int MainShipmentId { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public string Status { get; set; }
        public bool IsCancelled { get; set; }
        public CustomerDto Customer { get; set; }
        public ICollection<StatusUpdateDto> StatusUpdates { get; set; }
    }
}
