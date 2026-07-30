using CarRental.Api.Enums;
using CarRental.Api.Interfaces;
using CarRental.Api.Models;

namespace CarRental.Api.Providers;

public class BudgetWheelsProvider : ICarRentalProvider
{
    public ProviderType ProviderType => ProviderType.BudgetWheels;

    public IEnumerable<ProviderVehicle> SearchVehicles(CarSearchRequest request)
    {
        var vehicles = new List<ProviderVehicle>
        {
            new() { Id = "BW-001", Name = "BudgetWheels Economy", Category = VehicleCategory.Economy, Provider = ProviderType.BudgetWheels.ToString(), DailyRate = 70m, IsAvailable = true, InsuranceType = InsuranceType.Basic, CancellationPolicy = "Non-refundable bookings" },
            new() { Id = "BW-002", Name = "BudgetWheels Compact", Category = VehicleCategory.Compact, Provider = ProviderType.BudgetWheels.ToString(), DailyRate = 80m, IsAvailable = true, InsuranceType = InsuranceType.Basic, CancellationPolicy = "Non-refundable bookings" },
            new() { Id = "BW-003", Name = "BudgetWheels SUV", Category = VehicleCategory.SUV, Provider = ProviderType.BudgetWheels.ToString(), DailyRate = 100m, IsAvailable = false, InsuranceType = InsuranceType.Basic, CancellationPolicy = "Non-refundable bookings" },
            new() { Id = "BW-004", Name = "BudgetWheels Minivan", Category = VehicleCategory.Minivan, Provider = ProviderType.BudgetWheels.ToString(), DailyRate = 120m, IsAvailable = true, InsuranceType = InsuranceType.Basic, CancellationPolicy = "Non-refundable bookings" }
        };

        return request.Category.HasValue
            ? vehicles.Where(vehicle => vehicle.Category == request.Category.Value)
            : vehicles;
    }

    public BookingResponse Book(BookingRequest request, PriceBreakdown priceBreakdown)
    {
        return new BookingResponse
        {
            BookingReferenceNumber = $"BW-{Guid.NewGuid():N}".ToUpperInvariant(),
            Provider = ProviderType.BudgetWheels.ToString(),
            TotalPrice = priceBreakdown.TotalPrice,
            CancellationPolicy = "Non-refundable bookings"
        };
    }
}
