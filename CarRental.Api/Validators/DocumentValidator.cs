using CarRental.Api.Enums;
using CarRental.Api.Interfaces;
using CarRental.Api.Models;

namespace CarRental.Api.Validators;

public sealed class DocumentValidator : IDocumentValidator
{
    public bool IsValid(BookingRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return request.PickupLocation switch
        {
            "Domestic" => request.DocumentType == DocumentType.NationalId,
            "International" => request.DocumentType == DocumentType.Passport,
            _ => false
        };
    }
}
