using OAuth.Infrastructure;
using Microsoft.EntityFrameworkCore;
using OAuth.Presentation.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDbContext<EFDataContext>(option =>
        option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Host.AddAutofac();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<AdminInitializer>();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
    options.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseStaticFiles();

app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger/index.html");
    return Task.CompletedTask;
});

var adminInitializer = app.Services.GetRequiredService<AdminInitializer>();
adminInitializer.Initialize();

app.Run();