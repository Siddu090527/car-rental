# Car Rental Availability System

## Overview

This project is a .NET 8 Minimal API and Angular application that allows users to:

- Search available rental vehicles
- Book a vehicle
- Retrieve booking details
- Support multiple rental providers using the Provider Pattern

The application follows clean architecture principles with Dependency Injection, Entity Framework Core, SQL Server, and Angular.

---

# Technology Stack

## Backend

- .NET 8 Minimal API
- C#
- Entity Framework Core
- SQL Server
- Swagger/OpenAPI

## Frontend

- Angular
- TypeScript
- HTML
- SCSS

---

# Architecture

```
Angular UI
      │
      ▼
.NET 8 Minimal API
      │
      ▼
Booking Service
Search Service
      │
      ▼
Provider Pattern
 ├── PremiumDrive
 └── BudgetWheels
      │
      ▼
SQL Server
```

---

# Features

## Vehicle Search

Users can search vehicles using:

- Pickup Location
- Pickup Date
- Return Date
- Vehicle Category

Results are returned from multiple providers.

---

## Booking

Users can

- Select a vehicle
- Enter Driver Name
- Select Document Type
- Enter Document Number

The system

- Calculates pricing
- Applies provider rules
- Generates Booking Reference
- Saves booking into SQL Server

---

## Booking Lookup

Users can retrieve booking details using

Booking Reference Number

---

# Providers

## PremiumDrive

Business Rules

- Always available
- Comprehensive insurance
- Flat daily pricing
- Free cancellation up to 48 hours

---

## BudgetWheels

Business Rules

- Basic insurance
- Weekend surcharge
- Non-refundable
- May contain unavailable vehicles

---

# Pricing Rules

PremiumDrive

Flat daily rate

BudgetWheels

Weekend surcharge applied on

- Friday
- Saturday
- Sunday

---

# Database

SQL Server stores

- Booking Reference
- Driver Details
- Provider
- Pickup Information
- Return Information
- Total Price
- Cancellation Policy

---

# Project Structure

Backend

```
CarRental.Api

Data
Extensions
Interfaces
Middleware
Models
Providers
Services
Program.cs
```

Frontend

```
car-rental-ui

core
models
pages
search
booking
```

---

# Running Backend

```
dotnet restore

dotnet build

dotnet run
```

Swagger

```
http://localhost:5254/swagger
```

---

# Running Angular

```
npm install

ng serve
```

Angular

```
http://localhost:4200
```

---

# API Endpoints

Search Vehicles

```
GET /cars/search
```

Book Vehicle

```
POST /cars/book
```

Booking Details

```
GET /cars/booking/{reference}
```

---

# Design Patterns

- Provider Pattern
- Dependency Injection
- Service Layer
- Repository via Entity Framework
- Middleware
- Minimal API

---

# Validation

Backend validates

- Pickup Location
- Pickup Date
- Return Date
- Driver Name
- Document Number

---

# Future Improvements

- Authentication
- Payment Integration
- Email Notifications
- Unit Tests
- Docker Support
- Azure Deployment

---

# Author

Siddaiah Shaik

.NET Full Stack Developer