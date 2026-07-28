# AI Prompt Log

## Overview

AI assistance was used as a development accelerator to generate boilerplate code, review architecture, improve code quality, and validate implementation. All generated code was manually reviewed, tested, and refined before inclusion in the final solution.

---

# Prompt 1

## Goal

Design a scalable architecture for a .NET 8 Car Rental API.

### Prompt

> Design a .NET 8 Minimal API for a Car Rental system using Repository Pattern, Provider Pattern, Entity Framework Core, SQL Server, Dependency Injection, and clean separation of concerns.

### Outcome

Generated the initial project structure with:

- Minimal API
- Services
- Providers
- Repository
- Entity Framework Core

---

# Prompt 2

## Goal

Implement provider-specific pricing rules.

### Prompt

> Implement PremiumDrive and BudgetWheels pricing rules according to the business requirements.

### Outcome

Implemented:

PremiumDrive

- Flat daily pricing

BudgetWheels

- Weekend surcharge
- Friday
- Saturday
- Sunday

---

# Prompt 3

## Goal

Implement persistence.

### Prompt

> Create a repository layer using Entity Framework Core with SQL Server.

### Outcome

Implemented:

- AppDbContext
- Booking entity
- Repository Pattern
- Dependency Injection

---

# Prompt 4

## Goal

Improve maintainability.

### Prompt

> Refactor the project using SOLID principles while preserving existing behaviour.

### Outcome

Improved:

- Separation of concerns
- Dependency Injection
- Mapper Pattern
- Repository abstraction

---

# Prompt 5

## Goal

Increase code quality.

### Prompt

> Review all services and recommend production-ready improvements without changing business behaviour.

### Outcome

Added:

- Argument validation
- Exception handling
- Readability improvements
- XML documentation

---

# Prompt 6

## Goal

Improve API usability.

### Prompt

> Review Swagger endpoints and recommend improvements for validation and API consistency.

### Outcome

Improved:

- Request validation
- Response consistency
- Global exception middleware

---

# Prompt 7

## Goal

Review against the case study.

### Prompt

> Compare the implementation against the official challenge and identify missing requirements.

### Outcome

Completed:

- City-based validation
- HTTP 422 responses
- Documentation updates
- Search improvements

---

# Validation

Every AI-generated change was verified by:

- dotnet build
- dotnet test
- Swagger testing
- SQL Server verification

No code was accepted without successful verification.