using CarRental.Api.Models;

namespace CarRental.Api.Interfaces;

public interface IDocumentValidator
{
    bool IsValid(BookingRequest request);
}
