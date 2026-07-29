# Car Rental Availability System

# Architecture Document

## Overview

The Car Rental Availability System is built using a layered architecture with a Provider Pattern to support multiple rental providers while keeping the application extensible.

The application consists of:

- Angular Frontend
- .NET 8 Minimal API
- Service Layer
- Provider Layer
- Entity Framework Core
- SQL Server

---

# High Level Architecture

```
                 Angular UI
                      |
                      |
             HTTP REST API
                      |
                      |
             .NET 8 Minimal API
                      |
       ----------------------------
       |                          |
 Search Service             Booking Service
       |                          |
       ----------------------------
                      |
             Provider Pattern
        --------------------------
        |                        |
 PremiumDriveProvider    BudgetWheelsProvider
                      |
                      |
              Entity Framework
                      |
                      |
                 SQL Server
```

---

# Components

## Angular UI

Responsible for

- Vehicle Search
- Booking
- Booking Lookup

Communicates with the backend through REST APIs.

---

## Minimal API

Exposes REST endpoints.

Endpoints

```
GET    /cars/search

POST   /cars/book

GET    /cars/booking/{reference}
```

---

## Search Service

Responsibilities

- Validate search request
- Call all providers
- Aggregate search results
- Return combined response

---

## Booking Service

Responsibilities

- Validate booking
- Calculate pricing
- Generate Booking Reference
- Save booking
- Return booking response

---

## Provider Pattern

Each provider implements

```
ICarRentalProvider
```

Benefits

- Open Closed Principle
- Easy to add new providers
- Business rules isolated

---

# PremiumDrive Provider

Implements

- Always available
- Comprehensive insurance
- Flat daily pricing
- Free cancellation

---

# BudgetWheels Provider

Implements

- Basic insurance
- Weekend surcharge
- Non-refundable booking
- Availability filtering

---

# Entity Framework

Responsibilities

- Save bookings
- Retrieve bookings
- Database mapping

Database Context

```
AppDbContext
```

---

# SQL Server

Stores

- Booking Reference
- Driver Details
- Pickup Details
- Return Details
- Provider
- Pricing

---

# Dependency Injection

Services are registered using

```
builder.Services.AddCarRentalServices();
```

Benefits

- Loose coupling
- Easy testing
- Maintainability

---

# Exception Handling

Global Exception Middleware handles

- Unhandled Exceptions
- Validation Errors

Returns consistent HTTP responses.

---

# CORS

Angular communicates with the backend using

```
http://localhost:4200
```

Configured using

```
builder.Services.AddCors(...)
```

---

# API Flow

Vehicle Search

```
Angular

↓

GET /cars/search

↓

SearchService

↓

PremiumDrive

↓

BudgetWheels

↓

Combined Response

↓

Angular
```

---

Booking Flow

```
Angular

↓

POST /cars/book

↓

BookingService

↓

Price Calculation

↓

Save Booking

↓

SQL Server

↓

Booking Reference

↓

Angular
```

---

Booking Lookup

```
Angular

↓

GET /cars/booking/{reference}

↓

BookingService

↓

SQL Server

↓

Booking Details

↓

Angular
```

---

# Design Patterns

Implemented

- Provider Pattern
- Dependency Injection
- Service Layer
- Middleware
- Repository (Entity Framework)
- Minimal API

---

# Scalability

Adding a new provider requires

1. Create Provider Class

2. Implement ICarRentalProvider

3. Register in DI

No existing business logic needs modification.

This follows the Open/Closed Principle.