# Project Reflection

## Overview

This project involved building a Car Rental API using .NET 8 Minimal API while following clean architecture principles and the functional requirements provided in the case study.

The primary objective was to build a maintainable, extensible, and production-oriented solution rather than simply making the APIs work.

---

# What Went Well

Several architectural practices significantly improved the quality of the implementation.

## Clean Separation of Concerns

The project clearly separates responsibilities across:

- Minimal API
- Services
- Providers
- Repository
- Entity Framework Core
- SQL Server

This makes the application easier to maintain and extend.

---

## Provider Pattern

Using the Provider Pattern allowed PremiumDrive and BudgetWheels to implement independent business rules without affecting each other.

Adding a third provider would require only implementing the provider interface and registering it with dependency injection.

---

## Repository Pattern

The repository abstracts database access from the business layer.

Business logic remains inside the services while persistence is isolated in the repository.

---

## Entity Framework Core

Entity Framework Core simplified persistence and reduced boilerplate code while supporting asynchronous database operations.

---

## Dependency Injection

Dependency Injection improved testability and reduced coupling between components.

---

# Challenges Faced

Several implementation challenges were encountered during development.

## SQL Server Integration

Initial integration required configuring DbContext, dependency injection, and repository classes correctly before persistence worked as expected.

---

## Unit Test Compatibility

Introducing asynchronous repository methods required updating the services and fake repository while ensuring all existing unit tests continued to pass.

---

## Document Validation

The initial implementation validated only "Domestic" and "International".

The solution was later updated to use actual cities:

Domestic

- Hyderabad
- Bengaluru

International

- London
- Dubai
- Singapore

This better aligns with the case study requirements.

---

## Exception Handling

Implementing a global exception middleware simplified API responses and centralised error handling.

---

# Design Decisions

The following architectural decisions were made intentionally.

## Repository Pattern

Used to isolate persistence from business logic.

---

## Provider Pattern

Allows each rental provider to implement independent pricing and booking behaviour.

---

## Mapper Pattern

Separates database entities from API models, reducing coupling between persistence and business logic.

---

## Async Repository

Database operations use asynchronous methods to improve scalability.

---

# Lessons Learned

This project reinforced several software engineering principles.

- Keep business logic separate from persistence.
- Use dependency injection to improve maintainability.
- Prefer composition over conditional logic.
- Validate inputs early.
- Write unit tests before large refactoring.
- Verify every change with build and automated tests.

---

# Future Improvements

Potential enhancements include:

- JWT Authentication
- Role-based Authorization
- Serilog logging
- Docker support
- Azure deployment
- CI/CD pipeline
- Distributed caching
- Health checks
- API versioning
- Pagination and filtering
- OpenTelemetry monitoring

---

# Conclusion

The final solution satisfies the functional requirements of the case study while following clean architecture principles and modern .NET development practices.

The project remains extensible, testable, and suitable for future enhancements with minimal impact on the existing codebase.