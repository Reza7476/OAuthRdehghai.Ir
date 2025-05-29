using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using OAuth.Presentation.Configurations;
using Microsoft.IdentityModel.Tokens;
using OAuth.Infrastructure;
using System.Text;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

builder.Services
    .AddDbContext<EFDataContext>(option =>
        option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Host.AddAutofac();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerConfigGen();

builder.Services.AddSingleton<AdminInitializer>();
builder.Services.AddJwtAuthenticationConfig(builder.Configuration);

var app = builder.Build();
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseSwagger();

app.UseRezaExceptionHandler();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
    options.RoutePrefix = "swagger";
});

app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger/index.html");
    return Task.CompletedTask;
});

var adminInitializer = app.Services.GetRequiredService<AdminInitializer>();
adminInitializer.Initialize();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.Run();