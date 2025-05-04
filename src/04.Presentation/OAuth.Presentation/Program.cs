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
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRouting();
app.UseStaticFiles();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapControllers(); // برای API‌ها
});



app.Run();
