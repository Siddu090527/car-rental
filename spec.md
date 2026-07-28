# Car Rental API – Functional Specification

## Overview

The Car Rental API provides functionality to search available rental vehicles, create bookings, and retrieve booking details. The application is implemented using .NET 8 Minimal API with Entity Framework Core and SQL Server.

---

# Functional Requirements

## Search Vehicles

Endpoint

GET /cars/search

Supports:

- Pickup City
- Pickup Date
- Return Date
- Optional Vehicle Category

Returns

- Available vehicles
- Provider
- Category
- Daily Rate
- Insurance Type
- Cancellation Policy

---

## Book Vehicle

Endpoint

POST /cars/book

Validations

- Driver name required
- Pickup city required
- Pickup date required
- Return date required
- Return date must be after pickup
- Document validation
- Vehicle required

Returns

- Booking Reference
- Provider
- Total Price
- Cancellation Policy

---

## Booking Details

Endpoint

GET /cars/booking/{reference}

Returns

- Booking information
- Driver details
- Provider
- Vehicle
- Rental dates
- Total price

---

# Provider Rules

## PremiumDrive

- Always available
- Flat daily pricing
- Comprehensive insurance
- Free cancellation up to 48 hours

---

## BudgetWheels

- Weekend surcharge
- Basic insurance
- Non-refundable
- May return unavailable vehicles

---

# Pricing Rules

PremiumDrive

Daily Rate × Rental Nights

BudgetWheels

Daily Rate × Rental Nights

+

20% surcharge for

- Friday
- Saturday
- Sunday

---

# Document Validation

Domestic Cities

- Hyderabad
- Bengaluru

International Cities

- London
- Dubai
- Singapore

Rules

Domestic

National ID

International

Passport

---

# Architecture

Presentation

↓

Minimal API

↓

BookingService

↓

PricingService

↓

Repository

↓

Entity Framework Core

↓

SQL Server

---

# Design Patterns

- Repository Pattern
- Provider Pattern
- Dependency Injection
- Mapper Pattern

---

# Database

Table

Bookings

Columns

- BookingReferenceNumber
- DriverName
- PickupLocation
- PickupDate
- ReturnDate
- Provider
- VehicleId
- TotalPrice
- DocumentType
- DocumentNumber
- CancellationPolicy
- CreatedOn

---

# Testing

Unit tests verify

- Pricing
- Search
- Booking
- Document validation
- Repository operations

---

# Future Enhancements

- Authentication
- Authorization
- Docker
- Azure Deployment
- Caching
- Logging
- Monitoring