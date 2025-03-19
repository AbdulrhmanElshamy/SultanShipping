namespace SultanShipping.Contracts.Shipments
{
    public class MainShipmentUpdateDto
    {
        public DateTime ExpectedDeliveryDate { get; set; }
        public string Destination { get; set; }
        public string Status { get; set; }
    }
}
