using CarRental.Api.Enums;
using CarRental.Api.Interfaces;
using CarRental.Api.Models;

namespace CarRental.Api.Providers;

public class PremiumDriveProvider : ICarRentalProvider
{
    public ProviderType ProviderType => ProviderType.PremiumDrive;

    public IEnumerable<ProviderVehicle> SearchVehicles(CarSearchRequest request)
    {
        var vehicles = new List<ProviderVehicle>
        {
            new() { Id = "PD-001", Name = "PremiumDrive Compact", Category = VehicleCategory.Compact, Provider = ProviderType.PremiumDrive.ToString(), DailyRate = 110m, IsAvailable = true, InsuranceType = InsuranceType.Comprehensive, CancellationPolicy = "Free cancellation up to 48 hours before pickup" },
            new() { Id = "PD-002", Name = "PremiumDrive Economy", Category = VehicleCategory.Economy, Provider = ProviderType.PremiumDrive.ToString(), DailyRate = 95m, IsAvailable = true, InsuranceType = InsuranceType.Comprehensive, CancellationPolicy = "Free cancellation up to 48 hours before pickup" },
            new() { Id = "PD-003", Name = "PremiumDrive SUV", Category = VehicleCategory.SUV, Provider = ProviderType.PremiumDrive.ToString(), DailyRate = 140m, IsAvailable = true, InsuranceType = InsuranceType.Comprehensive, CancellationPolicy = "Free cancellation up to 48 hours before pickup" },
            new() { Id = "PD-004", Name = "PremiumDrive Minivan", Category = VehicleCategory.Minivan, Provider = ProviderType.PremiumDrive.ToString(), DailyRate = 160m, IsAvailable = true, InsuranceType = InsuranceType.Comprehensive, CancellationPolicy = "Free cancellation up to 48 hours before pickup" }
        };

        return request.Category.HasValue
            ? vehicles.Where(vehicle => vehicle.Category == request.Category.Value)
            : vehicles;
    }

    public BookingResponse Book(BookingRequest request, PriceBreakdown priceBreakdown)
    {
        return new BookingResponse
        {
            BookingReferenceNumber = $"PD-{Guid.NewGuid():N}".ToUpperInvariant(),
            Provider = ProviderType.PremiumDrive.ToString(),
            TotalPrice = priceBreakdown.TotalPrice,
            CancellationPolicy = "Free cancellation up to 48 hours before pickup"
        };
    }
}
