namespace CarRental.Api.Entities;

/// <summary>
/// Represents a persisted booking in SQL Server.
/// Each property maps to a column in the Bookings table.
/// </summary>
public class Booking
{
    // Primary key.
    public int Id { get; set; }

    // Unique booking reference shown to the customer.
    public string BookingReferenceNumber { get; set; } = string.Empty;

    // Customer information.
    public string DriverName { get; set; } = string.Empty;

    public string DocumentType { get; set; } = string.Empty;

    public string DocumentNumber { get; set; } = string.Empty;

    // Pickup location.
    public string PickupLocation { get; set; } = string.Empty;

    // Rental duration.
    public DateTime PickupDate { get; set; }

    public DateTime ReturnDate { get; set; }

    // Vehicle selected for the booking.
    public string VehicleId { get; set; } = string.Empty;
    public string VehicleName { get; set; } = string.Empty;
    public string VehicleCategory { get; set; } = string.Empty;
    public decimal DailyRate { get; set; }
    public string InsuranceType { get; set; } = string.Empty;
    public bool VehicleIsAvailable { get; set; }

    // Rental provider.
    public string Provider { get; set; } = string.Empty;

    // Final calculated booking amount.
    public decimal TotalPrice { get; set; }

    // Provider cancellation policy at the time of booking.
    public string CancellationPolicy { get; set; } = string.Empty;

    // Audit information.
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}