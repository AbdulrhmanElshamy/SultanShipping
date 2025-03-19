using SultanShipping.Abstractions;

namespace SultanShipping.Errors;

public record CustomerShipmentsErrors
{
    

    public static readonly Error CustomerShipmentNotFound =
    new("CustomerShipment.CustomerShipmentNotFound", "Customer Shipment Not Found is not found", StatusCodes.Status404NotFound);
}