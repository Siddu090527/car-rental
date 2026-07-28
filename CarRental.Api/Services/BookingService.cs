using CarRental.Api.Interfaces;
using CarRental.Api.Mappers;
using CarRental.Api.Models;
using CarRental.Api.Pricing;
using CarRental.Api.Repositories;

namespace CarRental.Api.Services;

public sealed class BookingService
{
    private readonly IReadOnlyCollection<ICarRentalProvider> providers;
    private readonly IDocumentValidator documentValidator;
    private readonly PricingService pricingService;
    private readonly IBookingRepository bookingRepository;

    public BookingService(
        IEnumerable<ICarRentalProvider> providers,
        IDocumentValidator documentValidator,
        PricingService pricingService,
        IBookingRepository bookingRepository)
    {
        this.providers = providers.ToList();
        this.documentValidator = documentValidator;
        this.pricingService = pricingService;
        this.bookingRepository = bookingRepository;
    }

    /// <summary>
    /// Creates a new booking.
    /// </summary>
    public async Task<BookingResponse> BookAsync(BookingRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        ValidateBookingRequest(request);

        if (!documentValidator.IsValid(request))
        {
            throw new InvalidOperationException(
                "The supplied document is not valid for the selected pickup location.");
        }

        var provider = providers.SingleOrDefault(
            p => p.ProviderType == request.Provider);

        if (provider is null)
        {
            throw new InvalidOperationException(
                "The requested provider is not available.");
        }

        var priceBreakdown = pricingService.CalculatePrice(
            request.Provider,
            request.SelectedVehicle.DailyRate,
            request.PickupDate,
            request.ReturnDate);

        var response = provider.Book(request, priceBreakdown);

        var bookingDetails = CreateBookingDetails(request, response);

        var bookingEntity = BookingMapper.ToEntity(bookingDetails);

        await bookingRepository.CreateAsync(bookingEntity);

        return response;
    }

    /// <summary>
    /// Returns booking details by booking reference.
    /// </summary>
    public async Task<BookingDetails?> GetBookingDetailsAsync(string? reference)
    {
        if (string.IsNullOrWhiteSpace(reference))
        {
            return null;
        }

        var booking = await bookingRepository.GetByReferenceAsync(reference);

        if (booking is null)
        {
            return null;
        }

        return BookingMapper.ToModel(booking);
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

        if (request.SelectedVehicle is null)
        {
            throw new InvalidOperationException(
                "A selected vehicle is required.");
        }

        if (string.IsNullOrWhiteSpace(request.SelectedVehicle.Id))
        {
            throw new InvalidOperationException(
                "Vehicle Id is required.");
        }

        if (request.ReturnDate <= request.PickupDate)
        {
            throw new InvalidOperationException(
                "Return date must be after pickup date.");
        }
    }

    private static BookingDetails CreateBookingDetails(
        BookingRequest request,
        BookingResponse response)
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