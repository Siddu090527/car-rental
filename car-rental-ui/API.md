# API Documentation

# Car Rental Availability System

## Overview

The Car Rental Availability System exposes REST APIs for:

- Vehicle Search
- Vehicle Booking
- Booking Lookup

Base URL

```
http://localhost:5254
```

---

# API List

| Method | Endpoint | Description |
|---------|----------|-------------|
| GET | /cars/search | Search available vehicles |
| POST | /cars/book | Create booking |
| GET | /cars/booking/{reference} | Retrieve booking details |

---

# 1. Search Vehicles

## Endpoint

```
GET /cars/search
```

## Description

Searches available vehicles from all configured providers.

---

## Query Parameters

| Parameter | Required | Type | Description |
|------------|----------|------|-------------|
| pickup | Yes | string | Pickup Location |
| from | Yes | Date | Pickup Date |
| to | Yes | Date | Return Date |
| category | No | int | Vehicle Category |

---

## Sample Request

```
GET /cars/search?pickup=Hyderabad&from=2026-07-30&to=2026-08-02
```

---

## Sample Response

```json
{
  "vehicles": [
    {
      "id": "PD-001",
      "name": "PremiumDrive Compact",
      "category": 1,
      "provider": "PremiumDrive",
      "dailyRate": 110,
      "isAvailable": true,
      "insuranceType": 1
    },
    {
      "id": "BW-001",
      "name": "BudgetWheels Economy",
      "category": 0,
      "provider": "BudgetWheels",
      "dailyRate": 70,
      "isAvailable": true,
      "insuranceType": 0
    }
  ]
}
```

---

## Validation

- Pickup Location is required.
- Pickup Date is required.
- Return Date is required.
- Return Date must be greater than Pickup Date.

---

## Status Codes

| Code | Description |
|------|-------------|
| 200 | Success |
| 400 | Invalid Request |
| 500 | Internal Server Error |

---

# 2. Book Vehicle

## Endpoint

```
POST /cars/book
```

---

## Description

Creates a booking for the selected vehicle.

---

## Sample Request

```json
{
  "driverName": "John Smith",
  "documentType": 0,
  "documentNumber": "ABC123456",
  "pickupLocation": "Hyderabad",
  "pickupDate": "2026-07-30T10:00:00",
  "returnDate": "2026-08-02T10:00:00",
  "provider": 0,
  "selectedVehicle": {
    "id": "PD-001",
    "name": "PremiumDrive Compact",
    "category": 1,
    "provider": "PremiumDrive",
    "dailyRate": 110,
    "isAvailable": true,
    "insuranceType": 1
  }
}
```

---

## Sample Response

```json
{
  "bookingReferenceNumber": "PD-123456789",
  "provider": "PremiumDrive",
  "totalPrice": 330,
  "cancellationPolicy": "Free cancellation up to 48 hours before pickup"
}
```

---

## Validation

- Driver Name required.
- Pickup Location required.
- Pickup Date required.
- Return Date required.
- Document Number required.
- Selected Vehicle required.

---

## Status Codes

| Code | Description |
|------|-------------|
| 201 | Booking Created |
| 400 | Validation Failed |
| 500 | Internal Server Error |

---

# 3. Booking Lookup

## Endpoint

```
GET /cars/booking/{reference}
```

---

## Description

Returns booking details using Booking Reference Number.

---

## Sample Request

```
GET /cars/booking/PD-123456789
```

---

## Sample Response

```json
{
  "bookingReferenceNumber": "PD-123456789",
  "driverName": "John Smith",
  "pickupLocation": "Hyderabad",
  "pickupDate": "2026-07-30T10:00:00",
  "returnDate": "2026-08-02T10:00:00",
  "provider": "PremiumDrive",
  "totalPrice": 330,
  "cancellationPolicy": "Free cancellation up to 48 hours before pickup"
}
```

---

## Status Codes

| Code | Description |
|------|-------------|
| 200 | Success |
| 404 | Booking Not Found |
| 500 | Internal Server Error |

---

# Business Rules

## PremiumDrive

- Always available
- Comprehensive Insurance
- Flat Daily Pricing
- Free Cancellation up to 48 Hours

---

## BudgetWheels

- Basic Insurance
- Weekend Surcharge
- Non-refundable
- Unavailable vehicles excluded from search results

---

# Vehicle Categories

| Value | Category |
|--------|----------|
| 0 | Economy |
| 1 | Compact |
| 2 | SUV |
| 3 | Minivan |

---

# Insurance Types

| Value | Insurance |
|--------|------------|
| 0 | Basic |
| 1 | Comprehensive |

---

# HTTP Status Codes

| Code | Meaning |
|------|---------|
| 200 | OK |
| 201 | Created |
| 400 | Bad Request |
| 404 | Not Found |
| 500 | Internal Server Error |

---

# Testing

The APIs can be tested using:

- Swagger UI
- Postman
- Angular Frontend

Swagger URL

```
http://localhost:5254/swagger
```

---

# Author

Siddaiah Shaik

.NET Full Stack Developer