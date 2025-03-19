using SultanShipping.Abstractions;
using SultanShipping.Contracts.Shipments;

namespace SultanShipping.Services.MainShipmentServices
{
    public interface IMainShipmentService
    {
        Task<IEnumerable<MainShipmentDto>> GetAllMainShipmentsAsync();
        Task<Result<MainShipmentDto>> GetMainShipmentByIdAsync(int id);
        Task<Result<MainShipmentDto>> CreateMainShipmentAsync(MainShipmentCreateDto mainShipmentDto);
        Task<Result> UpdateMainShipmentAsync(int id, MainShipmentUpdateDto mainShipmentDto);
        Task<Result<StatusUpdateDto>> AddStatusUpdateAsync(int id, StatusUpdateCreateDto statusUpdateDto);
    }
}
