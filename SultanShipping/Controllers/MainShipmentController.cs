using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SultanShipping.Abstractions;
using SultanShipping.Contracts.Shipments;
using SultanShipping.Services.MainShipmentServices;

namespace SultanShipping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MainShipmentController(IMainShipmentService service) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MainShipmentDto>>> GetAllMainShipments()
        {
            var mainShipments = await service.GetAllMainShipmentsAsync();
            return Ok(mainShipments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MainShipmentDto>> GetMainShipmentById(int id)
        {
            var result = await service.GetMainShipmentByIdAsync(id);

            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();

        }

        [HttpPost]
        public async Task<ActionResult<MainShipmentDto>> CreateMainShipment([FromBody] MainShipmentCreateDto mainShipmentDto)
        {
            var result = await service.CreateMainShipmentAsync(mainShipmentDto);

            if (result.IsSuccess)
                return CreatedAtAction(nameof(GetMainShipmentById), new { id = result.Value.Id }, result.Value);

            return result.ToProblem();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMainShipment(int id, [FromBody] MainShipmentUpdateDto mainShipmentDto)
        {
            var result = await service.UpdateMainShipmentAsync(id, mainShipmentDto);

            return result.IsSuccess ? Ok() : result.ToProblem();

        }

        [HttpPost("{id}/status")]
        public async Task<ActionResult<StatusUpdateDto>> AddStatusUpdate(int id, [FromBody] StatusUpdateCreateDto statusUpdateDto)
        {
            var result = await service.AddStatusUpdateAsync(id, statusUpdateDto);

            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();

        }
    }
}
