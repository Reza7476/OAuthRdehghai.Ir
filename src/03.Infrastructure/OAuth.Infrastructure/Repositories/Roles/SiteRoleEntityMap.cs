using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OAuth.Infrastructure.Repositories.Roles;

public class SiteRoleEntityMap : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> _)
    {
        _.ToTable("Roles").HasKey(_=>_.Id);
        _.Property(_ => _.Id).IsRequired().ValueGeneratedOnAdd();
        _.Property(_ => _.RoleName).IsRequired();

    }
}
