using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OAuth.Core.Entities.UserSites;

namespace OAuth.Infrastructure.Repositories.UserSites;

public class UserSiteEntityMap : IEntityTypeConfiguration<UserSite>
{
    public void Configure(EntityTypeBuilder<UserSite> _)
    {
        _.ToTable("UserSites").HasKey(_ => _.Id);

        _.Property(_ => _.Id).IsRequired().ValueGeneratedOnAdd();

        _.Property(_ => _.SiteId).IsRequired();
        
        _.Property(_ => _.UserId).IsRequired();

        _.HasOne(_ => _.User)
            .WithMany(_ => _.UserSites)
            .HasForeignKey(_ => _.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        _.HasOne(_ => _.Site)
            .WithMany(_ => _.UserSites)
            .HasForeignKey(_ => _.SiteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
