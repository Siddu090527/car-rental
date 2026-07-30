using CarRental.Api.Enums;
using CarRental.Api.Interfaces;
using CarRental.Api.Models;
using CarRental.Api.Pricing;
using CarRental.Api.Providers;
using CarRental.Api.Services;
using CarRental.Api.Validators;
using CarRental.Tests.Fakes;

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
            new PricingService(),
            new FakeBookingRepository());
    }

    [Fact]
    public void PricingService_CalculatesPremiumDriveFlatRatePricing()
    {
        var pricingService = new PricingService();

        var breakdown = pricingService.CalculatePrice(
            ProviderType.PremiumDrive,
            110m,
            new DateTime(2026, 7, 20),
            new DateTime(2026, 7, 23));

        Assert.Equal(110m, breakdown.DailyRate);
        Assert.Equal(3, breakdown.RentalNights);
        Assert.Equal(0m, breakdown.Surcharge);
        Assert.Equal(330m, breakdown.TotalPrice);
    }

    [Fact]
    public void PricingService_CalculatesBudgetWheelsWeekdayPricingWithoutSurcharge()
    {
        var pricingService = new PricingService();

        var breakdown = pricingService.CalculatePrice(
            ProviderType.BudgetWheels,
            70m,
            new DateTime(2026, 7, 14),
            new DateTime(2026, 7, 16));

        Assert.Equal(70m, breakdown.DailyRate);
        Assert.Equal(2, breakdown.RentalNights);
        Assert.Equal(0m, breakdown.Surcharge);
        Assert.Equal(140m, breakdown.TotalPrice);
    }

    [Fact]
    public void PricingService_CalculatesBudgetWheelsWeekendSurcharge()
    {
        var pricingService = new PricingService();

        var breakdown = pricingService.CalculatePrice(
            ProviderType.BudgetWheels,
            70m,
            new DateTime(2026, 7, 17),
            new DateTime(2026, 7, 20));

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
            PickupLocation = "Hyderabad",
            PickupDate = new DateTime(2026, 7, 20),
            ReturnDate = new DateTime(2026, 7, 24)
        });

        Assert.DoesNotContain(
            response.Vehicles,
            vehicle => vehicle.Provider == "BudgetWheels" &&
                       vehicle.Id == "BW-003" &&
                       vehicle.IsAvailable == false);

        Assert.All(
            response.Vehicles,
            vehicle => Assert.True(vehicle.IsAvailable, "SearchService should filter unavailable vehicles before returning results."));
    }

    [Fact]
    public void SearchService_IncludesCancellationPolicyInResults()
    {
        var searchService = new SearchService(new ICarRentalProvider[]
        {
            new PremiumDriveProvider(),
            new BudgetWheelsProvider()
        });

        var response = searchService.Search(new CarSearchRequest
        {
            PickupLocation = "Hyderabad",
            PickupDate = new DateTime(2026, 7, 20),
            ReturnDate = new DateTime(2026, 7, 24)
        });

        Assert.Contains(
            response.Vehicles,
            vehicle => vehicle.Provider == "PremiumDrive" &&
                       vehicle.CancellationPolicy == "Free cancellation up to 48 hours before pickup");
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
            PickupLocation = "Hyderabad",
            PickupDate = new DateTime(2026, 7, 20),
            ReturnDate = new DateTime(2026, 7, 24),
            Category = VehicleCategory.SUV
        });

        Assert.All(
            response.Vehicles,
            vehicle => Assert.Equal(VehicleCategory.SUV, vehicle.Category));
    }

    [Fact]
    public void DocumentValidator_AcceptsDomesticNationalId()
    {
        var validator = new DocumentValidator();

        var request = CreateBookingRequest(
            DocumentType.NationalId,
            "Hyderabad");

        Assert.True(validator.IsValid(request));
    }

    [Fact]
    public void DocumentValidator_AcceptsInternationalPassport()
    {
        var validator = new DocumentValidator();

        var request = CreateBookingRequest(
            DocumentType.Passport,
            "London");

        Assert.True(validator.IsValid(request));
    }

    [Fact]
    public async Task BookingService_ThrowsForInvalidBookingRequest()
    {
        var bookingService = CreateBookingService();

        var request = CreateBookingRequest(
            DocumentType.NationalId,
            "Hyderabad");

        request.DriverName = string.Empty;

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => bookingService.BookAsync(request));
    }

    [Fact]
    public async Task BookingService_RetrievesBookingByReference()
    {
        var bookingService = CreateBookingService();

        var request = CreateBookingRequest(
            DocumentType.NationalId,
            "Hyderabad");

        var response = await bookingService.BookAsync(request);

        var details = await bookingService.GetBookingDetailsAsync(
            response.BookingReferenceNumber);

        Assert.NotNull(details);

        Assert.Equal(
            response.BookingReferenceNumber,
            details!.BookingReferenceNumber);

        Assert.Equal(
            request.DriverName,
            details.DriverName);
    }

    [Fact]
    public async Task BookingService_PreservesSelectedVehicleDetailsInBookingDetails()
    {
        var bookingService = CreateBookingService();

        var request = CreateBookingRequest(
            DocumentType.NationalId,
            "Hyderabad");

        var response = await bookingService.BookAsync(request);
        var details = await bookingService.GetBookingDetailsAsync(
            response.BookingReferenceNumber);

        Assert.NotNull(details);
        Assert.Equal(request.SelectedVehicle.Id, details!.SelectedVehicle.Id);
        Assert.Equal(request.SelectedVehicle.Name, details.SelectedVehicle.Name);
        Assert.Equal(request.SelectedVehicle.DailyRate, details.SelectedVehicle.DailyRate);
        Assert.Equal(request.SelectedVehicle.InsuranceType, details.SelectedVehicle.InsuranceType);
        Assert.Equal(request.SelectedVehicle.CancellationPolicy, details.SelectedVehicle.CancellationPolicy);
    }

    [Fact]
    public async Task BookingService_ThrowsForMissingDocumentNumber()
    {
        var bookingService = CreateBookingService();
        var request = CreateBookingRequest(
            DocumentType.NationalId,
            "Hyderabad");

        request.DocumentNumber = string.Empty;

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => bookingService.BookAsync(request));
    }

    [Fact]
    public async Task BookingService_ThrowsForInvalidSelectedVehicle()
    {
        var bookingService = CreateBookingService();
        var request = CreateBookingRequest(
            DocumentType.NationalId,
            "Hyderabad");

        request.SelectedVehicle = new ProviderVehicle
        {
            Id = string.Empty,
            Name = string.Empty,
            DailyRate = 0m,
            Provider = "PremiumDrive"
        };

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => bookingService.BookAsync(request));
    }

    private static BookingRequest CreateBookingRequest(
        DocumentType documentType,
        string pickupLocation)
    {
        return new BookingRequest
        {
            DriverName = "Jane Doe",
            DocumentType = documentType,
            DocumentNumber = documentType == DocumentType.NationalId
                ? "N12345"
                : "P12345",

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
                InsuranceType = InsuranceType.Comprehensive,
                CancellationPolicy = "Free cancellation up to 48 hours before pickup"
            },

            PickupDate = new DateTime(2026, 7, 20),
            ReturnDate = new DateTime(2026, 7, 23)
        };
    }
}