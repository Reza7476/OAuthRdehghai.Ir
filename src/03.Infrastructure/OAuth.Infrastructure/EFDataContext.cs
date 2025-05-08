using Microsoft.EntityFrameworkCore;
using OAuth.Core.Entities.Users;
using System.Reflection;

namespace OAuth.Infrastructure;

public class EFDataContext : DbContext
{

    public EFDataContext(DbContextOptions<EFDataContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
