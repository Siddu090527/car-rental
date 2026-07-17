using CarRental.Api.Enums;
using CarRental.Api.Interfaces;
using CarRental.Api.Models;
using CarRental.Api.Pricing;
using CarRental.Api.Providers;
using CarRental.Api.Services;
using CarRental.Api.Validators;

namespace CarRental.Tests;

public class CarRentalBusinessRulesTests
{
    private static BookingService CreateBookingService()
    {
        return new BookingService(
            new ICarRentalProvider[]
            {
                new PremiumDriveProvider(),
                new BudgetWheelsProvider()
            },
            new DocumentValidator(),
            new PricingService());
    }

    [Fact]
    public void PricingService_CalculatesPremiumDriveFlatRatePricing()
    {
        var pricingService = new PricingService();

        var breakdown = pricingService.CalculatePrice(ProviderType.PremiumDrive, 110m, new DateTime(2026, 7, 20), new DateTime(2026, 7, 23));

        Assert.Equal(110m, breakdown.DailyRate);
        Assert.Equal(3, breakdown.RentalNights);
        Assert.Equal(0m, breakdown.Surcharge);
        Assert.Equal(330m, breakdown.TotalPrice);
    }

    [Fact]
    public void PricingService_CalculatesBudgetWheelsWeekdayPricingWithoutSurcharge()
    {
        var pricingService = new PricingService();

        var breakdown = pricingService.CalculatePrice(ProviderType.BudgetWheels, 70m, new DateTime(2026, 7, 13), new DateTime(2026, 7, 15));

        Assert.Equal(70m, breakdown.DailyRate);
        Assert.Equal(2, breakdown.RentalNights);
        Assert.Equal(0m, breakdown.Surcharge);
        Assert.Equal(140m, breakdown.TotalPrice);
    }

    [Fact]
    public void PricingService_CalculatesBudgetWheelsWeekendSurcharge()
    {
        var pricingService = new PricingService();

        var breakdown = pricingService.CalculatePrice(ProviderType.BudgetWheels, 70m, new DateTime(2026, 7, 17), new DateTime(2026, 7, 20));

        Assert.Equal(3, breakdown.RentalNights);
        Assert.Equal(42m, breakdown.Surcharge);
        Assert.Equal(252m, breakdown.TotalPrice);
    }

    [Fact]
    public void SearchService_FiltersOutUnavailableBudgetWheelsVehicles()
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
    public void SearchService_FiltersVehiclesByCategory()
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
            ReturnDate = new DateTime(2026, 7, 24),
            Category = VehicleCategory.SUV
        });

        Assert.All(response.Vehicles, vehicle => Assert.Equal(VehicleCategory.SUV, vehicle.Category));
    }

    [Fact]
    public void DocumentValidator_AcceptsDomesticNationalId()
    {
        var validator = new DocumentValidator();
        var request = CreateBookingRequest(DocumentType.NationalId, "Domestic");

        Assert.True(validator.IsValid(request));
    }

    [Fact]
    public void DocumentValidator_AcceptsInternationalPassport()
    {
        var validator = new DocumentValidator();
        var request = CreateBookingRequest(DocumentType.Passport, "International");

        Assert.True(validator.IsValid(request));
    }

    [Fact]
    public void BookingService_ThrowsForInvalidBookingRequest()
    {
        var bookingService = CreateBookingService();
        var request = CreateBookingRequest(DocumentType.NationalId, "Domestic");
        request.DriverName = string.Empty;

        var exception = Assert.Throws<InvalidOperationException>(() => bookingService.Book(request));
        Assert.Equal("Driver name is required.", exception.Message);
    }

    [Fact]
    public void BookingService_RetrievesBookingByReference()
    {
        var bookingService = CreateBookingService();
        var request = CreateBookingRequest(DocumentType.NationalId, "Domestic");

        var response = bookingService.Book(request);
        var details = bookingService.GetBookingDetails(response.BookingReferenceNumber);

        Assert.NotNull(details);
        Assert.Equal(response.BookingReferenceNumber, details!.BookingReferenceNumber);
        Assert.Equal(request.DriverName, details.DriverName);
    }

    private static BookingRequest CreateBookingRequest(DocumentType documentType, string pickupLocation)
    {
        return new BookingRequest
        {
            DriverName = "Jane Doe",
            DocumentType = documentType,
            DocumentNumber = documentType == DocumentType.NationalId ? "N12345" : "P12345",
            PickupLocation = pickupLocation,
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
            ReturnDate = new DateTime(2026, 7, 23)
        };
    }
}
