using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SultanShipping.Abstractions;
using SultanShipping.Contracts.Shipments;
using SultanShipping.Services.ShipmentServices;

namespace SultanShipping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentsController(IShipmentService service) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllShipments()
        {
            var shipments = await service.GetAllShipmentsAsync();
            return Ok(shipments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetShipmentById(int id)
        {
            var result = await service.GetShipmentByIdAsync(id);

            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpGet("track/{trackingNumber}")]
        public async Task<IActionResult> TrackShipment(string trackingNumber)
        {
            var result = await service.TrackShipmentAsync(trackingNumber);

            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpPost]
        public async Task<IActionResult> CreateShipment([FromBody] ShipmentCreateDto shipmentDto, [FromQuery] int mainShipmentId)
        {
            var result = await service.CreateShipmentAsync(shipmentDto, mainShipmentId);

            if (result.IsSuccess)
                return CreatedAtAction(nameof(GetShipmentById), new { id = result.Value.Id }, result.Value);

            return result.ToProblem();
        }

        [HttpPost("{id}/status")]
        public async Task<IActionResult> AddStatusUpdate(int id, [FromBody] StatusUpdateCreateDto statusUpdateDto)
        {
            var result = await service.AddStatusUpdateAsync(id, statusUpdateDto);

            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();

        }

        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> CancelShipment(int id, string cancellationReason)
        {
            var result = await service.CancelShipmentAsync(id, cancellationReason);

            return result.IsSuccess ? Ok() : result.ToProblem();

        }
    }
}
