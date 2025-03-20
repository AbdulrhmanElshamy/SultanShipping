using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using SultanShipping.Abstractions;
using SultanShipping.Contracts.Shipments;
using SultanShipping.Entities;
using SultanShipping.Errors;
using SultanShipping.Helpers;
using SultanShipping.Persistence;

namespace SultanShipping.Services.ShipmentServices
{
    public class ShipmentService(ApplicationDbContext dbContext,IEmailSender emailSender, IMapper _mapper,IHttpContextAccessor httpContextAccessor) : IShipmentService
    {

        public async Task<IEnumerable<ShipmentDto>> GetAllShipmentsAsync()
        {

            try
            {
                var shipments = await dbContext.CustomerShipments
                    .Include(s => s.Customer)
                    .Include(s => s.MasterShipment)
                    .Include(s => s.CustomerUpdates)
                    .ToListAsync();

                var shipmentDtos = _mapper.Map<IEnumerable<ShipmentDto>>(shipments);
                return shipmentDtos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}"); // Log the error
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                throw;
            }

        }

        public async Task<Result<ShipmentDto>> GetShipmentByIdAsync(int id)
        {
                var shipment = await dbContext.CustomerShipments
                    .Include(s => s.Customer)
                    .Include(s => s.MasterShipment)
                    .Include(s => s.CustomerUpdates)
                    .FirstOrDefaultAsync(s => s.Id == id);

                if (shipment is null)
                    return Result.Failure<ShipmentDto>(CustomerShipmentsErrors.CustomerShipmentNotFound);
            try
            {
                var shipmentDto = _mapper.Map<ShipmentDto>(shipment);
                return Result.Success(shipmentDto);


            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<Result<ShipmentTrackingDto>> TrackShipmentAsync(string trackingNumber)
        {
                var shipment = await dbContext.CustomerShipments
                    .Include(s => s.Customer)
                    .Include(s => s.CustomerUpdates)
                    .FirstOrDefaultAsync(s => s.TrackingNumber == trackingNumber);

                if (shipment is null)
                    return Result.Failure<ShipmentTrackingDto>(CustomerShipmentsErrors.CustomerShipmentNotFound);

                var trackingDto = _mapper.Map<ShipmentTrackingDto>(shipment);
                return Result.Success(trackingDto);
        }

        public async Task<Result<ShipmentDto>> CreateShipmentAsync(ShipmentCreateDto shipmentDto, int mainShipmentId)
        {

                var mainShipment = await dbContext.MasterShipments.FindAsync(mainShipmentId);
                if (mainShipment is null)
                    return Result.Failure<ShipmentDto>(CustomerShipmentsErrors.CustomerShipmentNotFound);


                var shipment = _mapper.Map<CustomerShipment>(shipmentDto);
                shipment.MasterShipmentId = mainShipmentId;
                shipment.Status = mainShipment.Status;
                shipment.TrackingNumber = $"SUB-{Guid.NewGuid().ToString().Substring(0, 8)}";

                dbContext.CustomerShipments.Add(shipment);

                await dbContext.SaveChangesAsync();

            try
            {

                return Result.Success(_mapper.Map<ShipmentDto>(shipment));
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<Result<StatusUpdateDto>> AddStatusUpdateAsync(int id, StatusUpdateCreateDto statusUpdateDto)
        {
                var shipment = await dbContext.CustomerShipments
                    .Include(s => s.Customer)
                    .FirstOrDefaultAsync(s => s.Id == id);

                if (shipment is null)
                    return Result.Failure<StatusUpdateDto>(CustomerShipmentsErrors.CustomerShipmentNotFound);

                var statusUpdate = _mapper.Map<CustomerShipmentUpdate>(statusUpdateDto);
                statusUpdate.CustomerShipmentId = id;
                statusUpdate.UpdateDate = DateTime.Now;
            

                dbContext.CustomerShipmentUpdates.Add(statusUpdate);

                shipment.Status = statusUpdate.Status;

                dbContext.Entry(shipment).State = EntityState.Modified;

                await dbContext.SaveChangesAsync();


            var placeholders = new Dictionary<string, string>
                {
                    { "{{name}}", shipment.Customer.FirstName },
                    { "{{pollTill}}", shipment.Customer.Email },
                    { "{{endDate}}", shipment.Customer.Email },
                    { "{{url}}", shipment.Customer.Email }
                };

            var body = EmailBodyBuilder.GenerateEmailBody("PollNotification", placeholders);

            await emailSender.SendEmailAsync(shipment.Customer.Email!, $"📣 Survey Basket: New Poll - {shipment.Customer.Email}", body);
            return Result.Success(_mapper.Map<StatusUpdateDto>(statusUpdate));
        }

        public async Task<Result> CancelShipmentAsync(int id, string cancellationReason)
        {
                var shipment = await dbContext.CustomerShipments
                    .Include(s => s.Customer)
                    .FirstOrDefaultAsync(s => s.Id == id);

                if (shipment is null)
                    return Result.Failure<StatusUpdateDto>(CustomerShipmentsErrors.CustomerShipmentNotFound);


                shipment.IsCancelled = true;
                shipment.Status = Entities.consts.enums.ShipmentStatus.Cancelled;

                dbContext.Entry(shipment).State = EntityState.Modified;

                var statusUpdate = new CustomerShipmentUpdate
                {
                    CustomerShipmentId = shipment.Id,
                    Location = "إلغاء",
                    Notes = cancellationReason,
                    UpdateDate = DateTime.Now
                };

                dbContext.CustomerShipmentUpdates.Add(statusUpdate);

                await dbContext.SaveChangesAsync();



            var placeholders = new Dictionary<string, string>
                {
                    { "{{name}}", shipment.Customer.FirstName },
                    { "{{pollTill}}", shipment.Customer.Email },
                    { "{{endDate}}", shipment.Customer.Email },
                    { "{{url}}", shipment.Customer.Email }
                };

            var body = EmailBodyBuilder.GenerateEmailBody("PollNotification", placeholders);

            await emailSender.SendEmailAsync(shipment.Customer.Email!, $"📣 Survey Basket: New Poll - {shipment.Customer.Email}", body);


            return Result.Success();
        }
    }
}

