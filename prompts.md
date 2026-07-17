# GitHub Copilot Prompts

## Requirements Analysis

### Objective
Understand the car rental assignment requirements and define the solution scope.

### Prompt
"Review the car rental availability specification and identify the required API endpoints, pricing rules, business rules, and validation behavior for a .NET 8 Minimal API solution."

### Outcome
Established the core requirements for search, booking, pricing, and document validation.

## Project Structure

### Objective
Create the initial solution structure for the API project.

### Prompt
"Create the project skeleton for a CarRental.Api solution with the required folders and empty C# files for models, enums, interfaces, providers, services, pricing, and validators."

### Outcome
Created the folder structure and initial class skeletons without implementation.

## Domain Models

### Objective
Define the core domain models needed for search and booking flows.

### Prompt
"Generate the main C# model classes for car search requests, search responses, provider vehicles, booking requests, booking responses, booking details, and pricing breakdowns using file-scoped namespaces."

### Outcome
Added the foundational models required by the API and services.

## Provider Pattern

### Objective
Implement the provider abstraction for multiple rental providers.

### Prompt
"Create an interface-based provider pattern for the rental application, including provider-specific implementations for PremiumDrive and BudgetWheels."

### Outcome
Introduced ICarRentalProvider and provider implementations for both suppliers.

## Services

### Objective
Build the orchestration services for search and booking operations.

### Prompt
"Implement SearchService and BookingService to coordinate provider results, pricing, validation, and booking persistence in a minimal but extensible way."

### Outcome
Added service-layer orchestration for search and booking workflows.

## Pricing

### Objective
Centralize pricing logic for both providers.

### Prompt
"Implement a PricingService that calculates PremiumDrive flat-rate pricing and BudgetWheels weekend surcharge pricing while keeping the logic centralized."

### Outcome
Added consistent pricing calculations for both providers in one service.

## Validation

### Objective
Implement document validation behavior for domestic and international pickups.

### Prompt
"Create a document validator that enforces domestic and international document rules and returns appropriate validation outcomes."

### Outcome
Added centralized validation logic for document acceptance rules.

## Minimal API

### Objective
Expose the application through .NET 8 Minimal API endpoints.

### Prompt
"Wire up Minimal API endpoints for search, booking, and booking lookup while keeping business logic out of Program.cs."

### Outcome
Implemented the required HTTP endpoints and delegated behavior to services.

## Testing

### Objective
Validate the core business behavior with automated tests.

### Prompt
"Create xUnit tests for search filtering and booking validation so the core rules are covered by automated tests."

### Outcome
Added regression tests for unavailable BudgetWheels vehicles and invalid document validation.

## Refactoring

### Objective
Improve the solution structure while preserving behavior.

### Prompt
"Refactor the application to improve SOLID principles, keep business logic out of Program.cs, centralize DI registration, and preserve current behavior."

### Outcome
Improved architecture clarity, service separation, and maintainability without changing the application behavior.
