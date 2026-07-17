# Reflection

## Project Summary

I built a .NET 8 Minimal API solution for the Car Rental Availability assignment with search, booking, and booking lookup capabilities. The project uses a provider-based architecture with PremiumDrive and BudgetWheels implementations, a shared provider interface, a pricing service, a document validator, and a small xUnit test suite.

## Architecture Decisions

I chose a simple layered approach that keeps the entry point focused on HTTP mapping while moving business logic into services. I used Dependency Injection to register providers and services, and I structured the solution around a provider abstraction so future providers can be added without changing the core workflow.

## Trade-offs

I kept the implementation intentionally lightweight and in-memory to stay aligned with the assignment scope. That made the project faster to build and easier to reason about, but it also means booking persistence and more advanced validation are not yet production-ready. I prioritized clarity and correctness over over-engineering.

## AI Usage

I used GitHub Copilot throughout the development process to help scaffold the initial project structure, generate C# files, implement the service and provider layers, and produce supporting documentation. The AI assistance helped accelerate repetitive work and reduce setup overhead, especially in the early phases of the project.

## Challenges Faced

The main challenge was balancing the assignment requirements with the need to keep the solution simple and maintainable. I also had to be careful not to over-implement features that were not required, while still organizing the code in a way that would support future extension.

## What Went Well

The project structure is now clear, the core API endpoints are in place, and the main business rules are covered by tests. The provider abstraction and service responsibilities are fairly well separated, which makes the solution easier to understand and extend.

## What I Would Improve With More Time

With more time, I would add stronger input validation, richer error models, more comprehensive test coverage, and persistent storage for bookings. I would also add OpenAPI documentation and further refine the separation between domain models and API-facing DTOs.

## Lessons Learned

I learned that good architecture is not only about correctness but also about keeping responsibilities clearly separated. A small project benefits a lot from a clean service boundary, clear DI setup, and a simple provider abstraction. I also learned that documentation and test coverage are just as important as implementation when delivering a complete solution.
