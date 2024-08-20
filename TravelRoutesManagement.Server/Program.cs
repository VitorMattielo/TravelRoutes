using Microsoft.EntityFrameworkCore;
using TravelRoutesManagement.Domain.Facades;
using TravelRoutesManagement.Domain.Interfaces.Facades;
using TravelRoutesManagement.Domain.Interfaces.Repositories;
using TravelRoutesManagement.Domain.Interfaces.UseCases;
using TravelRoutesManagement.Domain.UseCases;
using TravelRoutesManagement.Infrastructure.Contexts;
using TravelRoutesManagement.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<TravelRouteContext>(options =>
    {
        var connectionString = builder.Configuration.GetConnectionString("TravelRouteConnection");
        options.UseSqlServer(connectionString);
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Repositories
builder.Services.AddScoped<IAirportRepository, AirportRepository>();
builder.Services.AddScoped<IFlightConnectionRepository, FlightConnectionRepository>();

// Facades
builder.Services.AddScoped<IAirportFacade, AirportFacade>();
builder.Services.AddScoped<IFlightConnectionFacade, FlightConnectionFacade>();

// UseCases
builder.Services.AddScoped<IGetCheapestRouteUseCase, GetCheapestRouteUseCase>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapFallbackToFile("/index.html");

app.Run();
