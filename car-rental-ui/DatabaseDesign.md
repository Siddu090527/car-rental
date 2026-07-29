# Database Design

## Overview

The application uses SQL Server with Entity Framework Core to persist booking information.

The database stores booking transactions created through the Booking API.

---

# Database

SQL Server

---

# Table

Bookings

---

## Columns

| Column | Type | Description |
|---------|------|-------------|
| Id | INT | Primary Key |
| BookingReferenceNumber | NVARCHAR(100) | Unique Booking Reference |
| DriverName | NVARCHAR(200) | Driver Name |
| DocumentType | INT | National ID / Passport |
| DocumentNumber | NVARCHAR(100) | Driver Document |
| PickupLocation | NVARCHAR(200) | Pickup Location |
| PickupDate | DATETIME | Pickup Date |
| ReturnDate | DATETIME | Return Date |
| Provider | INT | PremiumDrive / BudgetWheels |
| VehicleId | NVARCHAR(50) | Selected Vehicle |
| VehicleName | NVARCHAR(200) | Vehicle Name |
| TotalPrice | DECIMAL(18,2) | Final Rental Price |
| CancellationPolicy | NVARCHAR(MAX) | Provider Cancellation Policy |

---

# Primary Key

```
Id
```

---

# Unique Key

```
BookingReferenceNumber
```

---

# Entity Relationship

```
Bookings

-----------------------------

Id (PK)

BookingReferenceNumber

DriverName

DocumentType

DocumentNumber

PickupLocation

PickupDate

ReturnDate

Provider

VehicleId

VehicleName

TotalPrice

CancellationPolicy
```

---

# Entity Framework

Database Context

```
AppDbContext
```

Entity

```
BookingEntity
```

---

# Persistence Flow

```
Angular

↓

Booking API

↓

Booking Service

↓

Entity Framework Core

↓

SQL Server
```

---

# Future Improvements

- Customer Table
- Vehicles Table
- Providers Table
- Payments Table
- Booking History
- Audit Logs