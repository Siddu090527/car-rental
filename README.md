<<<<<<< HEAD
# Car Rental Availability

## Project Overview

Car Rental Availability is a .NET 8 Minimal API application that provides a unified car rental search and booking experience by integrating multiple rental providers through a common abstraction. The solution demonstrates provider-based design, dependency injection, and a simple in-memory booking flow.

## Features

- Search available rental vehicles through a unified API
- Book a vehicle using a provider-backed workflow
- Retrieve booking details by reference
- Support provider-specific pricing rules
- Filter out unavailable BudgetWheels vehicles from search results
- Validate documents based on pickup location
- Return appropriate HTTP status codes for validation and request errors

## Architecture

The application follows a simple layered structure:

- Minimal API endpoints in the application entry point
- Search and booking services for orchestration
- Provider implementations for PremiumDrive and BudgetWheels
- A shared provider interface for extensibility
- A pricing service for pricing calculations
- A document validator for document rules

## Project Structure

```text
CarRental.Api/
├── Common/
├── Enums/
├── Extensions/
├── Interfaces/
├── Mappings/
├── Models/
├── Pricing/
├── Providers/
├── Services/
└── Validators/

CarRental.Tests/
└── UnitTest1.cs
```

## Technology Stack

- .NET 8
- C#
- ASP.NET Core Minimal API
- xUnit
- Dependency Injection

## Prerequisites

Before running the application, make sure you have:

- .NET 8 SDK installed
- A terminal or command prompt
- Access to the solution folder

## Setup Instructions

1. Clone or open the repository.
2. Navigate to the solution root.
3. Restore dependencies:

```bash
dotnet restore
```

4. Build the solution:

```bash
dotnet build
```

## Running the Application

Run the API from the solution root:

```bash
dotnet run --project CarRental.Api/CarRental.Api.csproj
```

The application will start the Minimal API and expose the endpoints described below.

## Running Unit Tests

Run the test suite with:

```bash
dotnet test
```

## API Endpoints

### Search vehicles

Endpoint:

```http
GET /cars/search?pickup=Domestic&from=2026-07-20&to=2026-07-24
```

Query parameters:

- pickup: pickup location
- from: pickup date
- to: return date
- category: optional vehicle category filter

Example response:

```json
{
  "vehicles": []
}
```

### Book a vehicle

Endpoint:

```http
POST /cars/book
```

Example request body:

```json
{
  "driverName": "Jane Doe",
  "documentType": 0,
  "documentNumber": "N12345",
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
  "pickupDate": "2026-07-20T00:00:00",
  "returnDate": "2026-07-24T00:00:00"
}
```

### Get booking details

Endpoint:

```http
GET /cars/booking/{reference}
```

Example:

```http
GET /cars/booking/PD-12345
```

## Business Rules

- PremiumDrive vehicles are always available.
- BudgetWheels may return unavailable vehicles, and unavailable vehicles are excluded from search results.
- Vehicle categories are normalized into common categories: Economy, Compact, SUV, and Minivan.
- PremiumDrive uses flat daily pricing.
- BudgetWheels applies weekend surcharge on Friday, Saturday, and Sunday.
- Domestic pickup accepts National ID.
- International pickup accepts Passport.

## Assumptions

- The application runs offline and uses in-memory booking storage.
- No database persistence is implemented.
- No authentication or authorization is included.
- Provider responses are deterministic for this implementation.

## Future Improvements

Potential next steps for the project include:

- Persist bookings in a database
- Add richer request validation and error handling
- Introduce additional providers through the existing abstraction
- Expand test coverage for pricing and endpoint behavior
- Add API documentation with Swagger/OpenAPI
=======
# car-rental
>>>>>>> fe7a33765c7ba5ed330d59456b405dc4e553124b
