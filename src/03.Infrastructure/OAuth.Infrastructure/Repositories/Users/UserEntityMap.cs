using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OAuth.Core.Entities.Users;

namespace OAuth.Infrastructure.Repositories.Users;

public class UserEntityMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> _)
    {
        _.ToTable("Users").HasKey(_ => _.Id);
        
        _.Property(_ => _.Id).IsRequired();
        
        _.Property(_ => _.Name).IsRequired();
        
        _.Property(_ => _.LastName).IsRequired();
        
        _.Property(_ => _.Mobile).IsRequired();
        
        _.Property(_ => _.UserName).IsRequired();
        
        _.Property(_ => _.HashPassword).IsRequired();
        
        _.Property(_=>_.CreationDate).IsRequired(false);

    }
}
