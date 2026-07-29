# Deployment Guide

# Car Rental Availability System

## Overview

This document describes how to run the Car Rental Availability System locally.

---

# Prerequisites

Install the following software:

- .NET 8 SDK
- SQL Server
- SQL Server Management Studio (SSMS)
- Node.js (LTS)
- Angular CLI
- Visual Studio 2022 or VS Code

---

# Clone Repository

```bash
git clone <repository-url>
cd car-rental
```

---

# Backend Setup

Navigate to backend

```bash
cd CarRental.Api
```

Restore packages

```bash
dotnet restore
```

Build project

```bash
dotnet build
```

Update Connection String

Open

```
appsettings.json
```

Update

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=CarRentalDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

---

# Apply Database Migration

```bash
dotnet ef database update
```

If migrations are unavailable

```bash
dotnet ef migrations add InitialCreate

dotnet ef database update
```

---

# Run Backend

```bash
dotnet run
```

Backend URL

```
http://localhost:5254
```

Swagger

```
http://localhost:5254/swagger
```

---

# Frontend Setup

Navigate

```bash
cd car-rental-ui
```

Install packages

```bash
npm install
```

Run Angular

```bash
ng serve
```

Angular URL

```
http://localhost:4200
```

---

# Verify Search

Open Angular

Enter

- Pickup Location
- Pickup Date
- Return Date

Click

```
Search Cars
```

Expected

Vehicle list should be displayed.

---

# Verify Booking

Select a vehicle

Enter

- Driver Name
- Document Type
- Document Number

Click

```
Confirm Booking
```

Expected

Booking reference should be generated.

---

# Verify Database

Open SQL Server Management Studio.

Execute

```sql
SELECT *
FROM Bookings
ORDER BY Id DESC;
```

Expected

New booking record should exist.

---

# Verify Booking Lookup

Open

```
http://localhost:4200/booking
```

Enter Booking Reference.

Expected

Booking details should be displayed.

---

# API Verification

Swagger

```
http://localhost:5254/swagger
```

Verify

- Search API
- Booking API
- Booking Lookup API

---

# Build Angular

```bash
ng build
```

Production files

```
dist/
```

---

# Publish Backend

```bash
dotnet publish -c Release
```

Published output

```
bin/Release/net8.0/publish
```

---

# Troubleshooting

## Angular cannot connect

Verify

```
http://localhost:5254
```

is running.

---

## CORS Error

Ensure

```csharp
builder.Services.AddCors(...)
```

is configured correctly.

---

## Database Connection Failed

Verify

- SQL Server running
- Connection String
- Database exists

---

## Migration Error

Run

```bash
dotnet ef database update
```

---

# Software Versions

| Software | Version |
|----------|----------|
| .NET | 8 |
| Angular | 20 |
| SQL Server | Express / Developer |
| Node.js | LTS |
| Entity Framework Core | 8 |

---

# Deployment Checklist

- Backend builds successfully
- Frontend builds successfully
- Database connected
- Swagger accessible
- Vehicle Search working
- Booking working
- Booking Lookup working
- SQL persistence verified

---

# Author

Siddaiah Shaik

.NET Full Stack Developer