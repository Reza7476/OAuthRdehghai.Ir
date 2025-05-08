using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OAuth.Core.Entities.Sites;

namespace OAuth.Infrastructure.Repositories.Sites;

public class SiteEntityMap : IEntityTypeConfiguration<Site>
{
    public void Configure(EntityTypeBuilder<Site> _)
    {
        _.ToTable("Sites").HasKey(_=>_.Id);

        _.Property(_ => _.Id).IsRequired().ValueGeneratedOnAdd();
        _.Property(_ => _.SiteName).IsRequired();
        _.Property(_ => _.SiteUrl).IsRequired();
    }
}
