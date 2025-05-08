using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OAuth.Core.Entities.Users;

namespace OAuth.Infrastructure.Repositories.Roles;

public class UserRoleEntityMap : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> _)
    {
        _.ToTable("UserRoles").HasKey(_ => _.Id);

        _.Property(_ => _.RoleId).IsRequired();
     
        _.Property(_ => _.UserId).IsRequired();
        
        _.HasOne(_ => _.Role).WithMany(_ => _.UserRoles)
            .HasForeignKey(_ => _.RoleId)
            .IsRequired().OnDelete(DeleteBehavior.Restrict);
        
        _.HasOne(_ => _.User)
            .WithMany(_ => _.UserRoles)
            .HasForeignKey(_ => _.UserId)
            .IsRequired().OnDelete(DeleteBehavior.Restrict);
    }
}
