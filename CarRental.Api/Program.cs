using CarRental.Api.Data;
using CarRental.Api.Enums;
using CarRental.Api.Extensions;
using CarRental.Api.Middleware;
using CarRental.Api.Models;
using CarRental.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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

var app = builder.Build();

// Global exception middleware
app.UseMiddleware<ExceptionMiddleware>();

// Enable CORS
app.UseCors("Angular");

// Swagger
app.UseSwagger();
app.UseSwaggerUI();

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
    if (request is null)
        return Results.BadRequest("Booking payload is required.");

    if (string.IsNullOrWhiteSpace(request.DriverName))
        return Results.BadRequest("Driver name is required.");

    if (string.IsNullOrWhiteSpace(request.PickupLocation))
        return Results.BadRequest("Pickup location is required.");

    if (request.ReturnDate <= request.PickupDate)
        return Results.BadRequest("Return date must be after pickup date.");

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