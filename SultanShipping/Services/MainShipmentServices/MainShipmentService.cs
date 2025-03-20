using MapsterMapper;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using SultanShipping.Abstractions;
using SultanShipping.Contracts.Shipments;
using SultanShipping.Entities;
using SultanShipping.Errors;
using SultanShipping.Extensions;
using SultanShipping.Persistence;

namespace SultanShipping.Services.MainShipmentServices
{
    public class MainShipmentService(ApplicationDbContext _context, IMapper _mapper, IEmailSender emailService,IHttpContextAccessor httpContextAccessor):IMainShipmentService
    {

        public async Task<IEnumerable<MainShipmentDto>> GetAllMainShipmentsAsync()
        {
                var mainShipments = await _context.MasterShipments
                    .Include(ms => ms.CustomerShipments)
                    .Include(ms => ms.Updates)
                    .ToListAsync();

                return _mapper.Map<IEnumerable<MainShipmentDto>>(mainShipments);
                
           
        }

        public async Task<Result<MainShipmentDto>> GetMainShipmentByIdAsync(int id)
        {
                  var mainShipment = await _context.MasterShipments
                    .Include(ms => ms.CustomerShipments)
                        .ThenInclude(cs => cs.Customer)
                    .Include(ms => ms.Updates)
                    .FirstOrDefaultAsync(ms => ms.Id == id);

                if (mainShipment is null)
                    return Result.Failure<MainShipmentDto>(CustomerShipmentsErrors.CustomerShipmentNotFound);

                var mainShipmentDto = _mapper.Map<MainShipmentDto>(mainShipment);
                return Result.Success(mainShipmentDto);
           
        }

        public async Task<Result<MainShipmentDto>> CreateMainShipmentAsync(MainShipmentCreateDto mainShipmentDto)
        {
                var mainShipment = _mapper.Map<MasterShipment>(mainShipmentDto);

            mainShipment.CreatedBy = httpContextAccessor.HttpContext.User.GetUserId() ?? "";
                await _context.MasterShipments.AddAsync(mainShipment);
                await _context.SaveChangesAsync();

                var mainShipmentDtoResult = _mapper.Map<MainShipmentDto>(mainShipment);
                return Result.Success(mainShipmentDtoResult);
           
        }

        public async Task<Result> UpdateMainShipmentAsync(int id, MainShipmentUpdateDto mainShipmentDto)
        {
                var mainShipment = await _context.MasterShipments.FindAsync(id);
                if (mainShipment is null)
                    return Result.Failure<MainShipmentDto>(CustomerShipmentsErrors.CustomerShipmentNotFound);

                _mapper.Map(mainShipmentDto, mainShipment);

                _context.Entry(mainShipment).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Result.Success();
           
        }

        public async Task<Result<StatusUpdateDto>> AddStatusUpdateAsync(int id, StatusUpdateCreateDto statusUpdateDto)
        {

                var mainShipment = await _context.MasterShipments
                    .Include(ms => ms.CustomerShipments)
                        .ThenInclude(cs => cs.Customer)
                    .FirstOrDefaultAsync(ms => ms.Id == id);

                if (mainShipment is null)
                    return Result.Failure<StatusUpdateDto>(CustomerShipmentsErrors.CustomerShipmentNotFound);

                var statusUpdate = _mapper.Map<ShipmentUpdate>(statusUpdateDto);
                statusUpdate.MasterShipmentId = id;
                statusUpdate.UpdateDate = DateTime.Now;
            statusUpdate.UpdatedBy = httpContextAccessor.HttpContext.User.GetUserId() ?? "";
                _context.ShipmentUpdates.Add(statusUpdate);


                mainShipment.Status = statusUpdate.Status;
                _context.Entry(mainShipment).State = EntityState.Modified;

              
                foreach (var shipment in mainShipment.CustomerShipments.Where(s => !s.IsCancelled))
                {
                    var shipmentStatusUpdate = new CustomerShipmentUpdate
                    {
                        CustomerShipmentId = shipment.Id,
                        Location = statusUpdate.Location,
                        Notes = statusUpdate.Notes,
                        UpdateDate = DateTime.Now,
                    };

                    _context.CustomerShipmentUpdates.Add(shipmentStatusUpdate);

                    shipment.Status = statusUpdate.Status;
                    _context.Entry(shipment).State = EntityState.Modified;
                  

                }

                await _context.SaveChangesAsync();

                //ToDo :SendEmail

                var statusUpdateResponse = _mapper.Map<StatusUpdateDto>(statusUpdate);
                return Result.Success(statusUpdateResponse);
          
        }
    }
}
