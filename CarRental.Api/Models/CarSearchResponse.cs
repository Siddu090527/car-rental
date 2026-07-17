namespace CarRental.Api.Models;

public class CarSearchResponse
{
    public List<ProviderVehicle> Vehicles { get; set; } = new();
}
