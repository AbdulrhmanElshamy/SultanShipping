namespace SultanShipping.Contracts.Shipments
{
    public class StatusUpdateDto
    {
        public int Id { get; set; }
        public int? MainShipmentId { get; set; }
        public int? ShipmentId { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Location { get; set; }
        public string Notes { get; set; }
        public string EmployeeId { get; set; }
    }
}
