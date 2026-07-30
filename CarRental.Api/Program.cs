using CarRental.Api.Data;
using CarRental.Api.Enums;
using CarRental.Api.Extensions;
using CarRental.Api.Middleware;
using CarRental.Api.Models;
using CarRental.Api.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog from configuration early so startup logs are captured.
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS for Angular
builder.Services.AddCors(options =>
{
    options.AddPolicy("Angular", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Application services
builder.Services.AddCarRentalServices();

// Entity Framework
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Health checks
builder.Services.AddHealthChecks();

try
{
    Log.Information("Starting Car Rental API");

    var app = builder.Build();

    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var providerName = dbContext.Database.ProviderName;

        if (!string.IsNullOrWhiteSpace(providerName) &&
            providerName.Contains("Sqlite", StringComparison.OrdinalIgnoreCase))
        {
            dbContext.Database.EnsureCreated();
        }
        else
        {
            dbContext.Database.Migrate();
        }
    }

    // Global exception middleware
    app.UseMiddleware<ExceptionMiddleware>();

    // Enable CORS
    app.UseCors("Angular");

    // Swagger
    app.UseSwagger();
    app.UseSwaggerUI();

    // Health endpoint
    app.MapHealthChecks("/health");

    // Root endpoint
    app.MapGet("/", () =>
        Results.Ok(new
        {
            message = "Car Rental API is running."
        }));

    // Search vehicles
    app.MapGet("/cars/search",
    (
        string? pickup,
        DateTime? from,
        DateTime? to,
        VehicleCategory? category,
        SearchService searchService
    ) =>
    {
        if (string.IsNullOrWhiteSpace(pickup))
            return Results.BadRequest("Pickup location is required.");

        if (from is null)
            return Results.BadRequest("Pickup date is required.");

        if (to is null)
            return Results.BadRequest("Return date is required.");

        if (to <= from)
            return Results.BadRequest("Return date must be after pickup date.");

        var request = new CarSearchRequest
        {
            PickupLocation = pickup,
            PickupDate = from.Value,
            ReturnDate = to.Value,
            Category = category
        };

        var response = searchService.Search(request);

        return Results.Ok(response);
    });

    // Create booking
    app.MapPost("/cars/book",
    async (
        BookingRequest request,
        BookingService bookingService
    ) =>
    {
        // Transport-level validation only: ensure payload exists.
        if (request is null)
            return Results.BadRequest("Booking payload is required.");

        // Delegate all business validation and orchestration to BookingService.
        // BookingService throws InvalidOperationException for validation failures
        // which is handled by the global ExceptionMiddleware.
        var response = await bookingService.BookAsync(request);

        return Results.Created(
            $"/cars/booking/{response.BookingReferenceNumber}",
            response);
    });

    // Booking details
    app.MapGet("/cars/booking/{reference}",
    async (
        string reference,
        BookingService bookingService
    ) =>
    {
        var booking = await bookingService.GetBookingDetailsAsync(reference);

        return booking is null
            ? Results.NotFound()
            : Results.Ok(booking);
    });

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}

public partial class Program { }
