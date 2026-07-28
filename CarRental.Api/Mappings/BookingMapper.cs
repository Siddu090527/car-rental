using CarRental.Api.Entities;
using CarRental.Api.Enums;
using CarRental.Api.Models;

namespace CarRental.Api.Mappers;

/// <summary>
/// Maps booking models to database entities and vice versa.
/// </summary>
public static class BookingMapper
{
    /// <summary>
    /// Converts BookingDetails into Booking entity.
    /// </summary>
    public static Booking ToEntity(BookingDetails details)
    {
        ArgumentNullException.ThrowIfNull(details);

        if (details.SelectedVehicle is null)
        {
            throw new ArgumentException(
                "Selected vehicle is required.",
                nameof(details));
        }

        return new Booking
        {
            BookingReferenceNumber = details.BookingReferenceNumber,
            DriverName = details.DriverName,
            PickupLocation = details.PickupLocation,
            PickupDate = details.PickupDate,
            ReturnDate = details.ReturnDate,
            Provider = details.Provider.ToString(),
            VehicleId = details.SelectedVehicle.Id,
            TotalPrice = details.TotalPrice,
            CancellationPolicy = details.CancellationPolicy,
            DocumentNumber = details.DocumentNumber,
            DocumentType = details.DocumentType.ToString(),
            CreatedOn = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Converts Booking entity into BookingDetails model.
    /// </summary>
    public static BookingDetails ToModel(Booking booking)
    {
        ArgumentNullException.ThrowIfNull(booking);

        Enum.TryParse(
            booking.Provider,
            out ProviderType provider);

        Enum.TryParse(
            booking.DocumentType,
            out DocumentType documentType);

        return new BookingDetails
        {
            BookingReferenceNumber = booking.BookingReferenceNumber,
            DriverName = booking.DriverName,
            PickupLocation = booking.PickupLocation,
            PickupDate = booking.PickupDate,
            ReturnDate = booking.ReturnDate,
            Provider = provider,
            TotalPrice = booking.TotalPrice,
            CancellationPolicy = booking.CancellationPolicy,
            DocumentNumber = booking.DocumentNumber,
            DocumentType = documentType,
            SelectedVehicle = new ProviderVehicle
            {
                Id = booking.VehicleId
            }
        };
    }
}