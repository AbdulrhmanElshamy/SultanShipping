namespace SultanShipping.Contracts.Shipments
{
    public class MainShipmentCreateDto
    {
        public string TrackingNumber { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public string Destination { get; set; }
        public string Status { get; set; }
    }
}
