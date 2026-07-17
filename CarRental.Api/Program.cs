using CarRental.Api.Extensions;
using CarRental.Api.Models;
using CarRental.Api.Services;
using CarRental.Api.Enums;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarRentalServices();

var app = builder.Build();

app.MapGet("/", () => Results.Ok(new { message = "Car Rental API is running." }));

app.MapGet("/cars/search", (string? pickup, DateTime? from, DateTime? to, VehicleCategory? category, SearchService searchService) =>
{
    if (string.IsNullOrWhiteSpace(pickup) || from is null || to is null)
    {
        return Results.BadRequest("Pickup location, pickup date, and return date are required.");
    }

    if (to <= from)
    {
        return Results.BadRequest("Return date must be after pickup date.");
    }

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

app.MapPost("/cars/book", (BookingRequest request, BookingService bookingService) =>
{
    if (request is null)
    {
        return Results.BadRequest("Booking payload is required.");
    }

    if (string.IsNullOrWhiteSpace(request.DriverName))
    {
        return Results.BadRequest("Driver name is required.");
    }

    if (string.IsNullOrWhiteSpace(request.PickupLocation))
    {
        return Results.BadRequest("Pickup location is required.");
    }

    if (request.PickupDate == default || request.ReturnDate == default)
    {
        return Results.BadRequest("Pickup and return dates are required.");
    }

    if (request.ReturnDate <= request.PickupDate)
    {
        return Results.BadRequest("Return date must be after pickup date.");
    }

    try
    {
        var response = bookingService.Book(request);
        return Results.Created($"/cars/booking/{response.BookingReferenceNumber}", response);
    }
    catch (InvalidOperationException ex) when (ex.Message.Contains("document", StringComparison.OrdinalIgnoreCase))
    {
        return Results.UnprocessableEntity(new { message = ex.Message });
    }
    catch (InvalidOperationException ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.MapGet("/cars/booking/{reference}", (string reference, BookingService bookingService) =>
{
    var booking = bookingService.GetBookingDetails(reference);
    return booking is null ? Results.NotFound() : Results.Ok(booking);
});

app.Run();
