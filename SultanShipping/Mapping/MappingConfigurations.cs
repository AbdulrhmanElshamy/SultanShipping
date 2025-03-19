using Mapster;
using Microsoft.AspNetCore.Identity.Data;
using SultanShipping.Contracts.Shipments;
using SultanShipping.Contracts.Users;
using SultanShipping.Entities;

namespace SultanShipping.Mapping;

public class MappingConfigurations : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, ApplicationUser>()
        .Map(dest => dest.UserName, src => src.Email);

        config.NewConfig<(ApplicationUser user, IList<string> roles), UserResponse>()
            .Map(dest => dest, src => src.user)
            .Map(dest => dest.Roles, src => src.roles);

        config.NewConfig<CreateUserRequest, ApplicationUser>()
            .Map(dest => dest.UserName, src => src.Email)
            .Map(dest => dest.EmailConfirmed, src => true);

        config.NewConfig<UpdateUserRequest, ApplicationUser>()
            .Map(dest => dest.UserName, src => src.Email)
            .Map(dest => dest.NormalizedUserName, src => src.Email.ToUpper());


        config.NewConfig<ShipmentUpdate, CustomerShipmentUpdateResponse>()
               .Map(dest => dest.Id, src => src.Id)
               .Map(dest => dest.UpdateDate, src => src.UpdateDate)
               .Map(dest => dest.Location, src => src.Location)
               .Map(dest => dest.Notes, src => src.Notes)
               .Map(dest => dest.Status, src => src.Status)
               .Map(dest => dest.UpdatedBy, src => src.UpdatedBy);

        config.NewConfig<MasterShipment, TrackShipmentResponse>()
            .Map(dest => dest.TrackingNumber, src => src.TrackingNumber)
            .Map(dest => dest.ExpectedDeliveryDate, src => src.ExpectedDeliveryDate)
            .Map(dest => dest.Destination, src => src.Destination)
            .Map(dest => dest.Status, src => src.Status)
            .Map(dest => dest.CustomerUpdates, src => src.Updates);

        config.NewConfig<CustomerShipment, TrackShipmentResponse>()
            .Map(dest => dest.CustomerName, src => src.Customer.UserName)
            .Map(dest => dest.DeliveryAddress, src => src.DeliveryAddress)
            .Map(dest => dest.IsCancelled, src => src.IsCancelled)
            .Map(dest => dest.CancellationReason, src => src.CancellationReason)
            .Map(dest => dest.CancellationDate, src => src.CancellationDate)
            .Map(dest => dest.TrackingNumber, src => src.MasterShipment.TrackingNumber)
            .Map(dest => dest.ExpectedDeliveryDate, src => src.MasterShipment.ExpectedDeliveryDate)
            .Map(dest => dest.Destination, src => src.MasterShipment.Destination)
            .Map(dest => dest.Status, src => src.MasterShipment.Status)
            .Map(dest => dest.CustomerUpdates, src => src.MasterShipment.Updates);


        // Customer mappings
        TypeAdapterConfig<ApplicationUser, CustomerDto>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Username, src => src.UserName)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.PhoneNumber, src => src.PhoneNumber)
            .Map(dest => dest.ShippingAddress, src => src.ShippingAddress);

        TypeAdapterConfig<CustomerCreateDto, ApplicationUser>.NewConfig()
            .Map(dest => dest.UserName, src => src.Username)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.PhoneNumber, src => src.PhoneNumber)
            .Map(dest => dest.ShippingAddress, src => src.ShippingAddress);

        TypeAdapterConfig<CustomerUpdateDto, ApplicationUser>.NewConfig()
            .Map(dest => dest.UserName, src => src.Username)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.PhoneNumber, src => src.PhoneNumber)
            .Map(dest => dest.ShippingAddress, src => src.ShippingAddress)
            .IgnoreNullValues(true);

        // MasterShipment mappings
        TypeAdapterConfig<MasterShipment, MainShipmentDto>.NewConfig()
            .Map(dest => dest.CustomerShipments, src => src.CustomerShipments);

        TypeAdapterConfig<MainShipmentCreateDto, MasterShipment>.NewConfig()
            .Map(dest => dest.Status, src => src.Status)
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.CreatedAt);

        TypeAdapterConfig<MainShipmentUpdateDto, MasterShipment>.NewConfig()
            .IgnoreNullValues(true)
            .Ignore(dest => dest.CreatedAt)
            .Ignore(dest => dest.CreatedBy);

        // CustomerShipment mappings
        TypeAdapterConfig<CustomerShipment, ShipmentDto>.NewConfig()
            .Map(dest => dest.TrackingNumber, src => src.TrackingNumber)
            .Map(dest => dest.Status, src => src.Status);

        TypeAdapterConfig<ShipmentCreateDto, CustomerShipment>.NewConfig()
            .Map(dest => dest.TrackingNumber, src => src.TrackingNumber)
            .Map(dest => dest.DeliveryAddress, src => src.DeliveryAddress)
            .Map(dest => dest.CustomerId, src => src.CustomerId)
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.CreatedAt);

        // ShipmentUpdate mappings
        TypeAdapterConfig<ShipmentUpdate, StatusUpdateDto>.NewConfig()
            .Map(dest => dest.UpdateDate, src => src.UpdateDate)
            .Map(dest => dest.Location, src => src.Location)
            .Map(dest => dest.Notes, src => src.Notes);

        TypeAdapterConfig<StatusUpdateCreateDto, ShipmentUpdate>.NewConfig()
            .Map(dest => dest.Location, src => src.Location)
            .Map(dest => dest.Notes, src => src.Notes)
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.UpdateDate);

        // Customer Shipment Update mappings (missing in your original code)
        TypeAdapterConfig<CustomerShipmentUpdate, CustomerUpdateDto>.NewConfig()
            .Map(dest => dest.UpdateDate, src => src.UpdateDate)
            .Map(dest => dest.Location, src => src.Location)
            .Map(dest => dest.Notes, src => src.Notes);

        // Create custom mapping for ShipmentTrackingDto
        TypeAdapterConfig<CustomerShipment, ShipmentTrackingDto>.NewConfig()
            .Map(dest => dest.CustomerName, src => src.Customer.UserName)
            .Map(dest => dest.TrackingNumber, src => src.TrackingNumber)
            .Map(dest => dest.DeliveryAddress, src => src.DeliveryAddress)
            .Map(dest => dest.Status, src => src.Status.ToString())
            .Map(dest => dest.ExpectedDeliveryDate, src => src.MasterShipment.ExpectedDeliveryDate)
            .Map(dest => dest.CurrentLocation, src =>
                src.CustomerUpdates != null && src.CustomerUpdates.Any() ?
                src.CustomerUpdates.OrderByDescending(su => su.UpdateDate).First().Location : "غير معروف")
            .Map(dest => dest.LastUpdated, src =>
                src.CustomerUpdates != null && src.CustomerUpdates.Any() ?
                src.CustomerUpdates.OrderByDescending(su => su.UpdateDate).First().UpdateDate.ToString("yyyy-MM-dd HH:mm:ss") :
                null)
            .Map(dest => dest.Notes, src =>
                src.CustomerUpdates != null && src.CustomerUpdates.Any() ?
                src.CustomerUpdates.OrderByDescending(su => su.UpdateDate).First().Notes : null)
            .Map(dest => dest.StatusHistory, src =>
                src.CustomerUpdates != null ?
                src.CustomerUpdates.OrderByDescending(su => su.UpdateDate)
                    .Select(su => new StatusHistoryDto
                    {
                        Date = su.UpdateDate,
                        Location = su.Location,
                        Notes = su.Notes,
                    }).ToList() :
                new List<StatusHistoryDto>());
    }
}