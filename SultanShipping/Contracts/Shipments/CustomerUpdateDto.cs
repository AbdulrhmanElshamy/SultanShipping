namespace SultanShipping.Contracts.Shipments
{
    public class CustomerUpdateDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ShippingAddress { get; set; }
        public string Location { get; set; }
        public string Notes { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
