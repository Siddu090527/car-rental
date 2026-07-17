using CarRental.Api.Enums;
using CarRental.Api.Interfaces;
using CarRental.Api.Models;
using CarRental.Api.Providers;
using CarRental.Api.Pricing;
using CarRental.Api.Services;
using CarRental.Api.Validators;

namespace CarRental.Tests;

public class UnitTest1
{
    [Fact]
    public void SearchService_ExcludesUnavailableBudgetWheelsVehicles()
    {
        var searchService = new SearchService(new ICarRentalProvider[]
        {
            new PremiumDriveProvider(),
            new BudgetWheelsProvider()
        });

        var response = searchService.Search(new CarSearchRequest
        {
            PickupLocation = "Domestic",
            PickupDate = new DateTime(2026, 7, 20),
            ReturnDate = new DateTime(2026, 7, 24)
        });

        Assert.DoesNotContain(response.Vehicles, vehicle => vehicle.Provider == "BudgetWheels" && vehicle.Id == "BW-003");
    }

    [Fact]
    public void BookingService_RejectsInvalidDocumentForDomesticPickup()
    {
        var bookingService = new BookingService(
            new ICarRentalProvider[]
            {
                new PremiumDriveProvider(),
                new BudgetWheelsProvider()
            },
            new DocumentValidator(),
            new PricingService());

        var request = new BookingRequest
        {
            DriverName = "Jane Doe",
            PickupLocation = "Domestic",
            DocumentType = DocumentType.Passport,
            DocumentNumber = "P12345",
            Provider = ProviderType.PremiumDrive,
            SelectedVehicle = new ProviderVehicle
            {
                Id = "PD-001",
                Name = "PremiumDrive Compact",
                Category = VehicleCategory.Compact,
                Provider = "PremiumDrive",
                DailyRate = 110m,
                IsAvailable = true,
                InsuranceType = InsuranceType.Comprehensive
            },
            PickupDate = new DateTime(2026, 7, 20),
            ReturnDate = new DateTime(2026, 7, 24)
        };

        Assert.Throws<InvalidOperationException>(() => bookingService.Book(request));
    }
}
