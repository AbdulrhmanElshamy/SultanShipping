namespace SultanShipping.Contracts.Shipments
{
    public class ShipmentCreateDto
    {
        public Guid CustomerId { get; set; }
        public string DeliveryAddress { get; set; }
    }
}
