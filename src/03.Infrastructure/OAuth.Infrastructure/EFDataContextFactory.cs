using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace OAuth.Infrastructure;

public class EFDataContextFactory : IDesignTimeDbContextFactory<EFDataContext>
{
    public EFDataContext CreateDbContext(string[] args)
    {
        try
        {
            var dir=Directory.GetCurrentDirectory();
            var srcIndex=dir.IndexOf("src");
            var basePath=dir.Substring(0, srcIndex+3);
            string newPath = Path.Combine(basePath, "04.Presentation", "OAuth.Presentation");

            IConfiguration configuration = new ConfigurationBuilder()
               .SetBasePath(newPath)  // اصلاح به SetBasePath
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString)) 
                throw new Exception("connection String was not found");
            
            var optionBuilder = new DbContextOptionsBuilder<EFDataContext>();
            optionBuilder.UseSqlServer(connectionString);

            return new EFDataContext(optionBuilder.Options);

        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }

    }
}
