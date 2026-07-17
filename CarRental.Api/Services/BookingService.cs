using CarRental.Api.Interfaces;
using CarRental.Api.Models;
using CarRental.Api.Pricing;

namespace CarRental.Api.Services;

public sealed class BookingService
{
    private readonly IReadOnlyCollection<ICarRentalProvider> providers;
    private readonly IDocumentValidator documentValidator;
    private readonly PricingService pricingService;
    private readonly Dictionary<string, BookingDetails> bookings = new(StringComparer.OrdinalIgnoreCase);

    public BookingService(IEnumerable<ICarRentalProvider> providers, IDocumentValidator documentValidator, PricingService pricingService)
    {
        this.providers = providers.ToList();
        this.documentValidator = documentValidator;
        this.pricingService = pricingService;
    }

    public BookingResponse Book(BookingRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        ValidateBookingRequest(request);

        if (!documentValidator.IsValid(request))
        {
            throw new InvalidOperationException("The supplied document is not valid for the selected pickup location.");
        }

        var provider = providers.SingleOrDefault(currentProvider => currentProvider.ProviderType == request.Provider)
            ?? throw new InvalidOperationException("The requested provider is not available.");

        var priceBreakdown = pricingService.CalculatePrice(request.Provider, request.SelectedVehicle.DailyRate, request.PickupDate, request.ReturnDate);
        var response = provider.Book(request, priceBreakdown);

        bookings[response.BookingReferenceNumber] = CreateBookingDetails(request, response);

        return response;
    }

    public BookingDetails? GetBookingDetails(string? reference)
    {
        if (string.IsNullOrWhiteSpace(reference))
        {
            return null;
        }

        bookings.TryGetValue(reference, out var details);
        return details;
    }

    private static void ValidateBookingRequest(BookingRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.DriverName))
        {
            throw new InvalidOperationException("Driver name is required.");
        }

        if (string.IsNullOrWhiteSpace(request.PickupLocation))
        {
            throw new InvalidOperationException("Pickup location is required.");
        }

        if (request.SelectedVehicle is null || string.IsNullOrWhiteSpace(request.SelectedVehicle.Id))
        {
            throw new InvalidOperationException("A selected vehicle is required.");
        }
    }

    private static BookingDetails CreateBookingDetails(BookingRequest request, BookingResponse response)
    {
        return new BookingDetails
        {
            BookingReferenceNumber = response.BookingReferenceNumber,
            DriverName = request.DriverName,
            DocumentType = request.DocumentType,
            DocumentNumber = request.DocumentNumber,
            PickupLocation = request.PickupLocation,
            Provider = request.Provider,
            SelectedVehicle = request.SelectedVehicle,
            TotalPrice = response.TotalPrice,
            CancellationPolicy = response.CancellationPolicy,
            PickupDate = request.PickupDate,
            ReturnDate = request.ReturnDate
        };
    }
}
