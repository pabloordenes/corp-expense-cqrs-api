# CorpExpense CQRS API

> Note: This repository is currently under active development.

Enterprise expense management REST API built with .NET 10. This project serves as a technical demonstration of modern architectural patterns, focusing on strict separation of concerns, domain encapsulation, and cloud-ready infrastructure.

## Architectural Overview

The solution strictly adheres to **Clean Architecture** principles, divided into four primary layers: Domain, Application, Infrastructure, and API. The dependency rule points exclusively inward, ensuring the core business logic remains framework-agnostic.

### Key Patterns & Principles

*   **Domain-Driven Design (DDD):** 
    The core features a rich domain model. Entities (e.g., `Expense`) act as Aggregate Roots, encapsulating their invariants. Properties have private setters to prevent anemic models, forcing state changes through explicit behaviors (e.g., `Submit()`, `Approve()`).
*   **State Machine Logic:** 
    Approval workflows are embedded within the domain entity. Transitions between `Draft`, `Submitted`, `UnderReview`, `Approved`, and `Rejected` are protected by domain rules, preventing illegal state shifts at the core level rather than relying on application-layer validation.
*   **CQRS (Command Query Responsibility Segregation):** 
    Implemented via `MediatR`. Write operations (Commands) are strictly separated from read operations (Queries). This segregation allows for independent scaling, distinct data models for reading/writing, and simplified testing.
*   **Fail-Fast Validation:** 
    Input validation is handled in the Application layer pipeline using FluentValidation, intercepting invalid requests before they reach the handler.

## Cloud-Ready Infrastructure Strategy

The infrastructure layer is designed to be fully compatible with Microsoft Azure, while allowing zero-cost local development through the **Strategy/Provider Pattern**:

*   **Storage Abstraction:** File operations (receipt uploads) depend on an `IFileStorage` interface. The local environment utilizes a file-system provider, while the production configuration is ready to inject the `Azure Blob Storage` implementation.
*   **Secret Management:** Development environments utilize .NET User Secrets to prevent credential leakage. The production pipeline is designed to seamlessly integrate with `Azure Key Vault` via Managed Identities (`DefaultAzureCredential`), requiring zero code changes for cloud deployment.
*   **Persistence:** Entity Framework Core with SQL Server.

## Technology Stack

*   **Framework:** .NET 10 / C# 14
*   **Architecture:** Clean Architecture, CQRS, DDD
*   **Libraries:** MediatR, FluentValidation, Entity Framework Core
*   **Cloud Target:** Azure (App Service / Functions, Blob Storage, Key Vault)

## Getting Started

### Prerequisites
*   .NET 10 SDK
*   SQL Server (LocalDB or Docker Container)

### Local Setup

1. Clone the repository.
2. Navigate to the API project directory.
3. Configure your local database connection string using User Secrets to maintain repository hygiene:
   ```bash
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=.;Database=CorpExpenseDb;Trusted_Connection=True;TrustServerCertificate=True;"
