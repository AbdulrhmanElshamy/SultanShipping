namespace SultanShipping.Contracts.Shipments
{
    public class ShipmentCreateDto
    {
        public int CustomerId { get; set; }
        public string DeliveryAddress { get; set; }
        public string TrackingNumber { get; set; }
    }
}
