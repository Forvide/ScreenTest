using Company.Delivery.Api;
using Company.Delivery.Api.AppStart;
using Company.Delivery.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDeliveryControllers();
builder.Services.AddDeliveryApi();
builder.Services.AddDependencyInjection();

builder.Services.AddDbContext<DeliveryDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseDeliveryApi();
app.MapControllers();

app.Run();