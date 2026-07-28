# Car Rental API

## Overview

Car Rental API is a .NET 8 Minimal API application that allows customers to search available rental vehicles, calculate rental pricing, validate booking documents, create bookings, and retrieve booking details.

The application follows Clean Architecture principles and uses Repository Pattern, Provider Pattern, Dependency Injection, Entity Framework Core, and SQL Server for persistence.

---

## Features

- Search available vehicles
- Book rental vehicles
- Retrieve booking details
- SQL Server persistence
- Swagger/OpenAPI support
- Entity Framework Core
- Repository Pattern
- Provider Pattern
- Async programming
- Global Exception Middleware
- Unit Tests

---

## Technology Stack

| Technology | Version |
|------------|---------|
| .NET | 8 |
| ASP.NET Core Minimal API | 8 |
| Entity Framework Core | 8 |
| SQL Server | 2022 |
| Swagger / OpenAPI | Latest |
| xUnit | Latest |

---

## Project Structure

```
CarRental.Api
│
├── Data
│   └── AppDbContext.cs
│
├── Entities
│   └── Booking.cs
│
├── Extensions
│   └── ServiceCollectionExtensions.cs
│
├── Interfaces
│
├── Mappers
│   └── BookingMapper.cs
│
├── Middleware
│   └── ExceptionMiddleware.cs
│
├── Models
│
├── Pricing
│   └── PricingService.cs
│
├── Providers
│   ├── PremiumDriveProvider.cs
│   └── BudgetWheelsProvider.cs
│
├── Repositories
│   ├── IBookingRepository.cs
│   └── BookingRepository.cs
│
├── Services
│   ├── BookingService.cs
│   └── SearchService.cs
│
├── Validators
│   └── DocumentValidator.cs
│
└── Program.cs
```

---

## Architecture

The application uses the following design patterns:

- Repository Pattern
- Provider Pattern
- Dependency Injection
- Mapper Pattern
- Minimal API
- Async/Await

---

## Business Rules

### PremiumDrive

- Flat daily pricing
- Comprehensive insurance
- Free cancellation up to 48 hours before pickup
- Vehicles are always available

### BudgetWheels

- Flat daily pricing
- 20% surcharge for rentals on Friday, Saturday, and Sunday
- Basic insurance
- Vehicles may be unavailable
- Non-refundable bookings

---

## Pricing Rules

### PremiumDrive

```
Total Price = Daily Rate × Rental Nights
```

### BudgetWheels

```
Total Price =
(Daily Rate × Rental Nights)
+ Weekend Surcharge
```

Weekend surcharge:

```
20% of Daily Rate
for each Friday, Saturday, and Sunday.
```

---

## Document Validation

| Pickup Location | Required Document |
|----------------|-------------------|
| Domestic | National ID |
| International | Passport |

---

## Database

Database:

```
CarRentalDb
```

Main table:

```
Bookings
```

Example:

| Column |
|---------|
| BookingReferenceNumber |
| DriverName |
| PickupLocation |
| PickupDate |
| ReturnDate |
| Provider |
| VehicleId |
| TotalPrice |
| DocumentType |
| DocumentNumber |
| CancellationPolicy |
| CreatedOn |

---

## API Endpoints

### Health

```
GET /
```

---

### Search Vehicles

```
GET /cars/search
```

Example:

```
pickup=Domestic
from=2026-08-01T10:00:00
to=2026-08-03T10:00:00
```

---

### Book Vehicle

```
POST /cars/book
```

Sample Request

```json
{
  "driverName": "Siddaiah",
  "documentType": 0,
  "documentNumber": "N123456789",
  "pickupLocation": "Domestic",
  "provider": 0,
  "selectedVehicle": {
    "id": "PD-001",
    "name": "PremiumDrive Compact",
    "category": 1,
    "provider": "PremiumDrive",
    "dailyRate": 110,
    "isAvailable": true,
    "insuranceType": 1
  },
  "pickupDate": "2026-08-01T10:00:00",
  "returnDate": "2026-08-03T10:00:00"
}
```

---

### Retrieve Booking

```
GET /cars/booking/{reference}
```

Example:

```
GET /cars/booking/PD-5D01C92B46B64E3DAED66BB65B54F16A
```

---

## Running the Application

### Restore

```bash
dotnet restore
```

### Build

```bash
dotnet build
```

### Run

```bash
dotnet run --project CarRental.Api
```

Swagger:

```
https://localhost:<port>/swagger
```

---

## Running Tests

```bash
dotnet test
```

---

## Error Handling

The application uses a global exception middleware that returns consistent HTTP responses.

Examples:

- 400 Bad Request
- 404 Not Found
- 500 Internal Server Error

---

## Design Decisions

- Repository Pattern separates persistence logic.
- Provider Pattern enables support for multiple rental providers.
- Entity Framework Core manages SQL Server persistence.
- BookingMapper separates database entities from API models.
- PricingService encapsulates provider-specific pricing rules.
- DocumentValidator centralises booking validation rules.

---

## Future Enhancements

- Authentication & Authorization
- Logging (Serilog)
- Docker support
- Azure deployment
- Pagination & filtering
- Rate limiting
- CI/CD pipeline
- Caching
- Monitoring & Health Checks

---

## Author

**Siddaiah Shaik**

.NET Full Stack Developer
