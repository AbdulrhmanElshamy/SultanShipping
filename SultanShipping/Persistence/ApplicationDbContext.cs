using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SultanShipping.Entities;
using System.Reflection;

namespace SultanShipping.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor) :
    IdentityDbContext<ApplicationUser, ApplicationRole, string>(options)
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;


    public DbSet<MasterShipment> MasterShipments { get; set; }
    public DbSet<CustomerShipment> CustomerShipments { get; set; }
    public DbSet<ShipmentUpdate> ShipmentUpdates { get; set; }
    public DbSet<CustomerShipmentUpdate> CustomerShipmentUpdates { get; set; }
    public DbSet<NotificationLog> NotificationLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        var cascadeFKs = builder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => fk.DeleteBehavior == DeleteBehavior.Cascade && !fk.IsOwnership);

        foreach (var fk in cascadeFKs)
            fk.DeleteBehavior = DeleteBehavior.Restrict;


        builder.Entity<MasterShipment>()
            .HasIndex(s => s.TrackingNumber)
            .IsUnique();

        builder.Entity<CustomerShipment>()
            .HasIndex(s => s.TrackingNumber)
            .IsUnique();

        builder.Entity<MasterShipment>()
            .HasMany(m => m.CustomerShipments)
            .WithOne(c => c.MasterShipment)
            .HasForeignKey(c => c.MasterShipmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<MasterShipment>()
            .HasMany(m => m.Updates)
            .WithOne(u => u.MasterShipment)
            .HasForeignKey(u => u.MasterShipmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<CustomerShipment>()
            .HasMany(c => c.CustomerUpdates)
            .WithOne(u => u.CustomerShipment)
            .HasForeignKey(u => u.CustomerShipmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<ApplicationUser>()
            .HasMany(u => u.Shipments)
            .WithOne(s => s.Customer)
            .HasForeignKey(s => s.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);
        base.OnModelCreating(builder);

    }
}