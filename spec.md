# Car Rental Availability - Technical Specification

## Table of Contents

1. Overview
2. Technology Stack
3. Providers
4. Vehicle Categories
5. API Endpoints
6. Pricing Rules
7. Business Rules
8. Document Validation
9. Validation Rules
10. Architecture
11. Dependency Injection
12. HTTP Status Codes
13. Extensibility
14. Assumptions

---

# Overview

This application provides a unified car rental search and booking experience by integrating multiple rental providers through a common interface.

The system supports:

- Vehicle Search
- Vehicle Booking
- Pricing Calculation
- Document Validation

The architecture is designed to be extensible, allowing additional rental providers to be integrated without modifying the core business flow.

---

# Technology Stack

## Backend

- .NET 8 Minimal API
- C#

## Frontend

- Angular

## Testing

- xUnit

## Design Principles

- Dependency Injection
- Provider Pattern
- SOLID Principles
- Separation of Concerns

---

# Providers

## PremiumDrive

Features:

- Flat daily pricing
- Always available
- Comprehensive insurance included
- Free cancellation up to 48 hours before pickup

---

## BudgetWheels

Features:

- Base daily pricing
- Weekend surcharge
- May return unavailable vehicles
- Basic insurance
- Non-refundable bookings

---

# Vehicle Categories

All provider-specific categories are normalized into the following common categories:

- Economy
- Compact
- SUV
- Minivan

---

# API Endpoints

## GET /cars/search

Search available rental cars.

### Query Parameters

| Parameter | Required | Description |
|----------|----------|-------------|
| pickup | Yes | Pickup location |
| from | Yes | Pickup date |
| to | Yes | Return date |
| category | No | Vehicle category |

### Response

Returns a normalized list of available vehicles from all providers.

---

## POST /cars/book

Books the selected vehicle.

### Request

- Driver Name
- Document Type
- Document Number
- Selected Vehicle
- Provider

### Response

Returns:

- Booking Reference Number
- Provider
- Total Price
- Cancellation Policy

---

## GET /cars/booking/{reference}

Returns booking details using the booking reference.

---

# Pricing Rules

## PremiumDrive

Pricing is calculated using a flat daily rate.

```
Total Price = Daily Rate × Rental Nights
```

---

## BudgetWheels

Pricing is calculated by iterating through each rental night.

Weekend surcharge applies only on:

- Friday
- Saturday
- Sunday

Weekend nights are charged at:

```
Daily Rate + 20%
```

Weekday nights use the normal daily rate.

---

# Business Rules

- PremiumDrive vehicles are always available.
- BudgetWheels may return unavailable vehicles.
- Vehicles marked as unavailable are excluded from search results.
- Vehicle categories from all providers are normalized into a common enum.
- Both providers expose daily pricing.
- The API returns both:
  - Per-day price
  - Total rental price

---

# Document Validation

Document validation is performed on both:

- Client-side
- Server-side

## Domestic Pickup

Accepted document:

- National ID

## International Pickup

Accepted document:

- Passport

When validation fails, the API returns:

```
HTTP 422 Unprocessable Entity
```

with a meaningful error message.

---

# Validation Rules

## HTTP 400 Bad Request

Returned when:

- Pickup location is missing
- Pickup date is missing
- Return date is missing
- Return date is before or equal to pickup date

---

## HTTP 422 Unprocessable Entity

Returned when:

- Invalid document type is supplied for the selected pickup location

---

# Architecture

```text
                Angular UI
                     │
                     ▼
            ASP.NET Minimal API
                     │
                     ▼
        Search / Booking Services
                     │
                     ▼
          ICarRentalProvider
              /           \
             /             \
            ▼               ▼
PremiumDriveProvider   BudgetWheelsProvider
             \             /
              \           /
               ▼         ▼
         Normalized Response
```

The business logic is isolated from provider implementations, allowing providers to be replaced or extended independently.

---

# Dependency Injection

The application uses Dependency Injection for loose coupling and extensibility.

Current provider registrations:

- PremiumDriveProvider
- BudgetWheelsProvider

Both implement:

```
ICarRentalProvider
```

The Search Service interacts only with the interface and is unaware of provider-specific implementations.

---

# HTTP Status Codes

| Status Code | Description |
|-------------|-------------|
| 200 OK | Successful search |
| 201 Created | Booking successful |
| 400 Bad Request | Invalid or missing request parameters |
| 422 Unprocessable Entity | Document validation failed |

---

# Extensibility

The system is designed to support additional providers without modifying the existing search workflow.

Example:

```
LuxuryCarsProvider : ICarRentalProvider
```

Adding a new provider requires:

1. Implementing `ICarRentalProvider`
2. Registering the provider in Dependency Injection

No modifications are required to:

- Search API
- Booking API
- Existing provider implementations

This follows the Open/Closed Principle (OCP).

---

# Assumptions

- No authentication or authorization.
- No database persistence.
- No external rental APIs.
- Provider responses are deterministic.
- The application runs completely offline.
- Booking information is stored in memory for the duration of application execution.
- Pickup locations are predefined by the application.
- All prices are returned in a single currency.