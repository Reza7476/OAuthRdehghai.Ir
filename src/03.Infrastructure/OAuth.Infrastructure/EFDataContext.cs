using Microsoft.EntityFrameworkCore;
using OAuth.Core.Entities.Users;

namespace OAuth.Infrastructure;

public class EFDataContext : DbContext
{
    

    public EFDataContext(DbContextOptions<EFDataContext> options) : base(options)
    {

    }

    public DbSet<User>  Users { get; set; }


}
