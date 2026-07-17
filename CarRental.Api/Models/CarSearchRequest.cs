using CarRental.Api.Enums;

namespace CarRental.Api.Models;

public class CarSearchRequest
{
    public string PickupLocation { get; set; } = string.Empty;
    public DateTime PickupDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public VehicleCategory? Category { get; set; }
}
