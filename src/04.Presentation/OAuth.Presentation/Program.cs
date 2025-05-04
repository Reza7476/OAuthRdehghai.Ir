using Microsoft.EntityFrameworkCore;
using OAuth.Infrastructure;
using OAuth.Presentation.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
   .AddDbContext<EFDataContext>(option =>
   option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Host.AddAutofac();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    options.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context =>
    {
        context.Response.Redirect("/swagger/index.html");
    });
    endpoints.MapControllers();
});

app.Run();
