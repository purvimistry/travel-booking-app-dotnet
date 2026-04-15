using Microsoft.EntityFrameworkCore;
using TravelBookingApp.Application.Interfaces;
using TravelBookingApp.Application.Services;
using TravelBookingApp.Infrastructure.Data;
using TravelBookingApp.Infrastructure.ExternalServices;
using TravelBookingApp.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddScoped<IBookingService, BookingService>();

builder.Services.AddHttpClient<ICurrencyService, CurrencyService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowWeb",
        policy => policy
            .WithOrigins("https://localhost:7045") // your MVC app URL
            .AllowAnyHeader()
            .AllowAnyMethod());
});
var app = builder.Build();
app.UseCors("AllowWeb");



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
