using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;
using CarRental.Api.Models;

namespace CarRental.Api.IntegrationTests;

public class IntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;

    public IntegrationTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task HealthEndpoint_ReturnsOk()
    {
        var client = _factory.CreateClient();

        var res = await client.GetAsync("/health");

        res.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task SearchEndpoint_ReturnsVehicles_AndCategoryFiltering()
    {
        var client = _factory.CreateClient();

        var res = await client.GetAsync("/cars/search?pickup=Hyderabad&from=2026-07-20&to=2026-07-24");
        res.StatusCode.Should().Be(HttpStatusCode.OK);

        var payload = await res.Content.ReadFromJsonAsync<CarSearchResponse>();
        payload.Should().NotBeNull();
        payload!.Vehicles.Should().NotBeNullOrEmpty();

        // Category filter
        var res2 = await client.GetAsync("/cars/search?pickup=Hyderabad&from=2026-07-20&to=2026-07-24&category=SUV");
        res2.StatusCode.Should().Be(HttpStatusCode.OK);
        var payload2 = await res2.Content.ReadFromJsonAsync<CarSearchResponse>();
        payload2!.Vehicles.Should().OnlyContain(v => v.Category.ToString() == "SUV");
    }

    [Fact]
    public async Task BudgetWheels_UnavailableVehicle_IsExcluded()
    {
        var client = _factory.CreateClient();

        var res = await client.GetAsync("/cars/search?pickup=Hyderabad&from=2026-07-20&to=2026-07-24");
        res.StatusCode.Should().Be(HttpStatusCode.OK);

        var payload = await res.Content.ReadFromJsonAsync<CarSearchResponse>();
        payload!.Vehicles.Should().NotContain(v => v.Provider == "BudgetWheels" && v.Id == "BW-003" && v.IsAvailable == false);
    }

    [Fact]
    public async Task Book_ReturnsCreated_AndBookingStored()
    {
        var client = _factory.CreateClient();

        var request = new BookingRequest
        {
            DriverName = "Jane Doe",
            DocumentType = CarRental.Api.Enums.DocumentType.NationalId,
            DocumentNumber = "N12345",
            PickupLocation = "Hyderabad",
            Provider = CarRental.Api.Enums.ProviderType.PremiumDrive,
            SelectedVehicle = new ProviderVehicle
            {
                Id = "PD-001",
                Name = "PremiumDrive Compact",
                Category = CarRental.Api.Enums.VehicleCategory.Compact,
                Provider = "PremiumDrive",
                DailyRate = 110m,
                IsAvailable = true,
                InsuranceType = CarRental.Api.Enums.InsuranceType.Comprehensive,
                CancellationPolicy = "Free cancellation up to 48 hours before pickup"
            },
            PickupDate = new DateTime(2026, 7, 20),
            ReturnDate = new DateTime(2026, 7, 23)
        };

        var res = await client.PostAsJsonAsync("/cars/book", request);

        res.StatusCode.Should().Be(HttpStatusCode.Created);
        var response = await res.Content.ReadFromJsonAsync<BookingResponse>();
        response.Should().NotBeNull();
        response!.Provider.Should().NotBeNullOrEmpty();
        response.TotalPrice.Should().BeGreaterThan(0);
        response.CancellationPolicy.Should().NotBeNullOrEmpty();

        // Get booking details
        var get = await client.GetAsync($"/cars/booking/{response.BookingReferenceNumber}");
        get.StatusCode.Should().Be(HttpStatusCode.OK);
        var details = await get.Content.ReadFromJsonAsync<BookingDetails>();
        details.Should().NotBeNull();
        details!.BookingReferenceNumber.Should().Be(response.BookingReferenceNumber);
    }

    [Fact]
    public async Task Book_InvalidDocument_Returns422()
    {
        var client = _factory.CreateClient();

        var request = new BookingRequest
        {
            DriverName = "John Doe",
            DocumentType = CarRental.Api.Enums.DocumentType.Passport,
            DocumentNumber = "P12345",
            PickupLocation = "Hyderabad",
            Provider = CarRental.Api.Enums.ProviderType.PremiumDrive,
            SelectedVehicle = new ProviderVehicle
            {
                Id = "PD-001",
                Name = "PremiumDrive Compact",
                Category = CarRental.Api.Enums.VehicleCategory.Compact,
                Provider = "PremiumDrive",
                DailyRate = 110m,
                IsAvailable = true,
                InsuranceType = CarRental.Api.Enums.InsuranceType.Comprehensive,
                CancellationPolicy = "Free cancellation up to 48 hours before pickup"
            },
            PickupDate = new DateTime(2026, 7, 20),
            ReturnDate = new DateTime(2026, 7, 23)
        };

        var res = await client.PostAsJsonAsync("/cars/book", request);

        res.StatusCode.Should().Be((HttpStatusCode)422);
    }

    [Fact]
    public async Task Book_MissingDriverName_ReturnsValidationError()
    {
        var client = _factory.CreateClient();

        var request = new BookingRequest
        {
            DriverName = "",
            DocumentType = CarRental.Api.Enums.DocumentType.NationalId,
            DocumentNumber = "N12345",
            PickupLocation = "Hyderabad",
            Provider = CarRental.Api.Enums.ProviderType.PremiumDrive,
            SelectedVehicle = new ProviderVehicle
            {
                Id = "PD-001",
                Name = "PremiumDrive Compact",
                Category = CarRental.Api.Enums.VehicleCategory.Compact,
                Provider = "PremiumDrive",
                DailyRate = 110m,
                IsAvailable = true,
                InsuranceType = CarRental.Api.Enums.InsuranceType.Comprehensive
            },
            PickupDate = new DateTime(2026, 7, 20),
            ReturnDate = new DateTime(2026, 7, 23)
        };

        var res = await client.PostAsJsonAsync("/cars/book", request);

        res.StatusCode.Should().Be((HttpStatusCode)422);
    }

    [Fact]
    public async Task GetBooking_UnknownReference_ReturnsNotFound()
    {
        var client = _factory.CreateClient();

        var res = await client.GetAsync("/cars/booking/UNKNOWN-REF");

        res.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
