# Postman Collection

# Car Rental Availability System

## Overview

This document describes the API requests that can be tested using Postman.

---

# Base URL

```
http://localhost:5254
```

---

# API 1 - Search Vehicles

## Method

GET

## Endpoint

```
/cars/search
```

## Example

```
GET http://localhost:5254/cars/search?pickup=Hyderabad&from=2026-07-30&to=2026-08-02
```

### Optional Category Filter

```
category=0  Economy

category=1  Compact

category=2  SUV

category=3  Minivan
```

---

# API 2 - Book Vehicle

## Method

POST

## Endpoint

```
/cars/book
```

### Headers

```
Content-Type: application/json
```

### Request Body

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

# API 3 - Booking Lookup

## Method

GET

## Endpoint

```
/cars/booking/{reference}
```

### Example

```
GET http://localhost:5254/cars/booking/PD-123456789
```

---

# Expected Workflow

Step 1

Search Vehicles

↓

Step 2

Choose Vehicle

↓

Step 3

Create Booking

↓

Step 4

Receive Booking Reference

↓

Step 5

Lookup Booking

---

# Success Status Codes

| Status | Description |
|---------|-------------|
| 200 | Request Successful |
| 201 | Booking Created |

---

# Error Status Codes

| Status | Description |
|---------|-------------|
| 400 | Bad Request |
| 404 | Not Found |
| 500 | Internal Server Error |

---

# Testing Checklist

- Search API returns available vehicles
- Booking API creates booking
- Booking stored in SQL Server
- Booking lookup returns correct details
- Provider rules applied correctly
- Weekend surcharge applied for BudgetWheels
- PremiumDrive cancellation policy returned