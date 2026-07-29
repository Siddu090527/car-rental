# Project Structure

# Car Rental Availability System

## Overview

The solution is divided into two independent projects.

- Backend (.NET 8 Minimal API)
- Frontend (Angular)

---

# Solution Structure

```
car-rental/
│
├── CarRental.Api/
│
├── car-rental-ui/
│
├── README.md
├── Architecture.md
├── DatabaseDesign.md
├── API.md
├── DeploymentGuide.md
├── ProjectStructure.md
├── FutureEnhancements.md
├── SequenceDiagram.md
└── PostmanCollection.md
```

---

# Backend Structure

```
CarRental.Api
│
├── Data
│     └── AppDbContext.cs
│
├── Entities
│     └── BookingEntity.cs
│
├── Models
│     ├── BookingRequest.cs
│     ├── BookingResponse.cs
│     ├── BookingDetails.cs
│     ├── CarSearchRequest.cs
│     ├── CarSearchResponse.cs
│     ├── ProviderVehicle.cs
│     └── PriceBreakdown.cs
│
├── Interfaces
│     └── ICarRentalProvider.cs
│
├── Providers
│     ├── PremiumDriveProvider.cs
│     └── BudgetWheelsProvider.cs
│
├── Services
│     ├── SearchService.cs
│     └── BookingService.cs
│
├── Extensions
│     └── ServiceCollectionExtensions.cs
│
├── Middleware
│     └── ExceptionMiddleware.cs
│
├── Program.cs
│
└── appsettings.json
```

---

# Frontend Structure

```
car-rental-ui
│
├── src
│
├── app
│
│   ├── core
│   │
│   │   └── services
│   │       └── car-rental.service.ts
│   │
│   ├── models
│   │
│   ├── pages
│   │
│   │   ├── search
│   │   │
│   │   │   ├── search.ts
│   │   │   ├── search.html
│   │   │   └── search.scss
│   │   │
│   │   └── booking
│   │       ├── booking.ts
│   │       ├── booking.html
│   │       └── booking.scss
│   │
│   ├── app.routes.ts
│   ├── app.config.ts
│   ├── app.ts
│   └── app.html
│
└── package.json
```

---

# Backend Responsibilities

## Data

Contains Entity Framework Core database context.

---

## Models

Contains request and response models used by APIs.

---

## Interfaces

Defines contracts implemented by providers.

---

## Providers

Contains business logic specific to each rental provider.

- PremiumDrive
- BudgetWheels

---

## Services

Contains application business logic.

SearchService

- Vehicle Search
- Provider Aggregation

BookingService

- Price Calculation
- Booking Creation
- Booking Lookup

---

## Middleware

Handles global exception handling.

---

## Extensions

Registers Dependency Injection services.

---

# Frontend Responsibilities

## Search Page

Allows users to

- Search vehicles
- View search results
- Book vehicles

---

## Booking Page

Allows users to retrieve booking details.

---

## Service Layer

Communicates with REST APIs.

---

## Models

Stores TypeScript interfaces used across the application.

---

# Design Principles

The project follows

- SOLID Principles
- Dependency Injection
- Provider Pattern
- Service Layer Pattern
- Clean Separation of Concerns
- Minimal API

---

# Benefits

- Easy to maintain
- Easy to extend
- Supports additional providers
- Clear separation of business logic
- Reusable services
- Testable architecture