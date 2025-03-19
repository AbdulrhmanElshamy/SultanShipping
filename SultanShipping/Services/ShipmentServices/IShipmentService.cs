using SultanShipping.Abstractions;
using SultanShipping.Contracts.Shipments;

namespace SultanShipping.Services.ShipmentServices
{
    public interface IShipmentService
    {
        Task<IEnumerable<ShipmentDto>> GetAllShipmentsAsync();
        Task<Result<ShipmentDto>> GetShipmentByIdAsync(int id);
        Task<Result<ShipmentTrackingDto>> TrackShipmentAsync(string trackingNumber);
        Task<Result<ShipmentDto>> CreateShipmentAsync(ShipmentCreateDto shipmentDto, int mainShipmentId);
        Task<Result<StatusUpdateDto>> AddStatusUpdateAsync(int id, StatusUpdateCreateDto statusUpdateDto);
        Task<Result> CancelShipmentAsync(int id, string cancellationReason);
    }
}
