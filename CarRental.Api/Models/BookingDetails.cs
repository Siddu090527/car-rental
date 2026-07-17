using CarRental.Api.Enums;

namespace CarRental.Api.Models;

public class BookingDetails
{
    public string BookingReferenceNumber { get; set; } = string.Empty;
    public string DriverName { get; set; } = string.Empty;
    public DocumentType DocumentType { get; set; }
    public string DocumentNumber { get; set; } = string.Empty;
    public string PickupLocation { get; set; } = string.Empty;
    public ProviderType Provider { get; set; }
    public ProviderVehicle SelectedVehicle { get; set; } = new();
    public decimal TotalPrice { get; set; }
    public string CancellationPolicy { get; set; } = string.Empty;
    public DateTime PickupDate { get; set; }
    public DateTime ReturnDate { get; set; }
}
