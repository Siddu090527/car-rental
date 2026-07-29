# Sequence Diagram

# Car Rental Availability System

## Overview

This document describes the sequence of interactions between the user, Angular frontend, .NET backend, Provider Pattern, and SQL Server.

---

# 1. Vehicle Search

```
+--------+       +-------------+       +--------------+       +---------------------+
|  User  |       | Angular UI  |       | SearchService|       | Rental Providers    |
+--------+       +-------------+       +--------------+       +---------------------+
     |                   |                     |                         |
     | Search Vehicles   |                     |                         |
     |------------------>|                     |                         |
     |                   | GET /cars/search    |                         |
     |                   |-------------------->|                         |
     |                   |                     | Search PremiumDrive     |
     |                   |                     |------------------------>|
     |                   |                     |                         |
     |                   |                     | Search BudgetWheels     |
     |                   |                     |------------------------>|
     |                   |                     |                         |
     |                   |                     | Merge Results           |
     |                   |<--------------------|                         |
     | Display Vehicles  |                     |                         |
     |<------------------|                     |                         |
```

---

# 2. Vehicle Booking

```
+--------+      +-------------+      +---------------+      +-------------+
| User   |      | Angular UI  |      | BookingService|      | SQL Server  |
+--------+      +-------------+      +---------------+      +-------------+
     |                 |                     |                     |
     | Select Vehicle  |                     |                     |
     |---------------->|                     |                     |
     |                 | POST /cars/book     |                     |
     |                 |-------------------->|                     |
     |                 |                     | Validate Booking     |
     |                 |                     | Calculate Price      |
     |                 |                     | Generate Reference   |
     |                 |                     | Save Booking         |
     |                 |                     |--------------------->|
     |                 |                     |                     |
     |                 |<--------------------|                     |
     | Booking Success |                     |                     |
     |<----------------|                     |                     |
```

---

# 3. Booking Lookup

```
+--------+      +-------------+      +---------------+      +-------------+
| User   |      | Angular UI  |      | BookingService|      | SQL Server  |
+--------+      +-------------+      +---------------+      +-------------+
     |                 |                     |                     |
     | Enter Reference |                     |                     |
     |---------------->|                     |                     |
     |                 | GET /cars/booking   |                     |
     |                 |-------------------->|                     |
     |                 |                     | Retrieve Booking    |
     |                 |                     |-------------------->|
     |                 |                     |                     |
     |                 |<--------------------|                     |
     | Booking Details |                     |                     |
     |<----------------|                     |                     |
```

---

# End-to-End Flow

```
User
  │
  ▼
Angular UI
  │
  ▼
REST API (.NET 8)
  │
  ▼
Business Services
  │
  ▼
Provider Pattern
  │
  ▼
Entity Framework Core
  │
  ▼
SQL Server
```

---

# Components Involved

- Angular Frontend
- .NET 8 Minimal API
- SearchService
- BookingService
- PremiumDriveProvider
- BudgetWheelsProvider
- Entity Framework Core
- SQL Server

---

# Summary

The application follows a clean request flow where the frontend communicates with the backend through REST APIs. Business logic is encapsulated in services, provider-specific behavior is isolated using the Provider Pattern, and bookings are persisted using Entity Framework Core and SQL Server.