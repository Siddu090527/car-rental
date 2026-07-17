using CarRental.Api.Enums;

namespace CarRental.Api.Models;

public class ProviderVehicle
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public VehicleCategory Category { get; set; }
    public string Provider { get; set; } = string.Empty;
    public decimal DailyRate { get; set; }
    public bool IsAvailable { get; set; }
    public InsuranceType InsuranceType { get; set; }
}
