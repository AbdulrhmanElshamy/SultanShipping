using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SultanShipping.Entities;

public sealed class ApplicationUser : IdentityUser
{

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public bool IsDisabled { get; set; }

    public List<RefreshToken> RefreshTokens { get; set; } = [];

    [MaxLength(255)]
    public string ShippingAddress { get; set; } = string.Empty;

    public  ICollection<CustomerShipment> Shipments { get; set; } = new List<CustomerShipment>();
}

