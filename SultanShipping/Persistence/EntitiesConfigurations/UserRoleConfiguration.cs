using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SultanShipping.Abstractions.Consts;

namespace SultanShipping.Persistence.EntitiesConfigurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        //Default Data
        builder.HasData(new IdentityUserRole<string>
        {
            UserId = DefaultUsers.Admin.Id,
            RoleId = DefaultRoles.Admin.Id
        });
    }
}