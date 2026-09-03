CINEMA BOOKING SYSTEM
======================

1. PROJECT OVERVIEW
-------------------

Cinema Booking System is a backend project for managing an online cinema
booking workflow.

The project is built with ASP.NET Core .NET 8 and follows Clean Architecture.
It provides movie, theater, seat, showtime, booking, food and payment
management. Authentication and authorization are handled by Duende
IdentityServer, while SignalR is used for real-time seat status updates.

The solution also contains a separate AI service for chatbot/LLM integration.

Main features:
- User registration and login
- JWT authentication
- Role and permission-based authorization
- Movie management
- Genre management
- Theater management
- Seat management
- Showtime management
- Cinema ticket booking
- Seat hold / booked / available states
- Booking expiration background service
- Food ordering with booking
- VNPAY payment integration
- VNPAY callback and return URL handling
- Real-time seat status notification with SignalR
- Pagination
- Global exception handling
- Unit testing with xUnit and Moq
- AI chatbot service using OpenAI SDK


2. TECHNOLOGY STACK
-------------------

Backend:
- C# / .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server / LocalDB
- MediatR
- AutoMapper

Authentication / Authorization:
- Duende IdentityServer 7.4.5
- ASP.NET Core Identity
- JWT Bearer Authentication
- Claims / Permission-based Authorization
- OAuth 2.0 / OpenID Connect
- PKCE for Swagger authentication

Real-time communication:
- ASP.NET Core SignalR

Payment:
- VNPAY Sandbox



Testing:
- xUnit
- Moq
- Microsoft.NET.Test.Sdk
- Coverlet

Other:
- Bogus for seed data
- Swagger / OpenAPI


3. SOLUTION STRUCTURE
---------------------

CinemaProjectSolution/
|
+-- Cinema.Api/
|   +-- Controllers/
|   +-- Middleware/
|   +-- Attribute/
|   +-- Program.cs
|   +-- appsettings.json
|
+-- Cinema.Application/
|   +-- Features/
|   |   +-- Booking/
|   |   +-- Food/
|   |   +-- Genre/
|   |   +-- Movie/
|   |   +-- Payment/
|   |   +-- Seat/
|   |   +-- Showtime/
|   |   +-- Theater/
|   |
|   +-- Interfaces/
|   +-- BackgroundServices/
|   +-- AutoMappers/
|   +-- DI/
|
+-- Cinema.Domain/
|   +-- Enitities/
|
+-- Cinema.Infrastructure/
|   +-- Database/
|   +-- Repositories/
|   +-- Helpers/
|   +-- Migrations/
|   +-- DI/
|
+-- Cinema.SignalR/
|   +-- Hubs/
|   +-- Services/
|   +-- DI/
|
+-- Cenima.IdentityApi/
|   +-- IdentityServer configuration
|   +-- ASP.NET Identity
|   +-- Permission module
|   +-- Login / Logout / Account pages
|
|
+-- SeedWorks/
|   +-- DTOs / Models
|   +-- API response models
+--tests/
|   +-- Cinema.Application.Test/
|     +-- Unit tests
|
+-- CinemaProjectSolution.slnx


4. ARCHITECTURE
---------------

The project follows Clean Architecture principles.

Dependency direction:

    Cinema.Api
        |
        v
    Cinema.Application
        |
        v
    Cinema.Domain

Cinema.Infrastructure implements interfaces defined by the Application
layer.

Cinema.SignalR provides the SignalR implementation and communicates with
the Application layer through abstractions.

Main responsibilities:

Cinema.Domain
- Contains entities and domain enums.
- Does not depend on Infrastructure or API.

Cinema.Application
- Contains business logic.
- Uses MediatR commands and queries.
- Defines repository and service interfaces.
- Contains background processing logic.
- Does not directly depend on Infrastructure implementations.

Cinema.Infrastructure
- Contains EF Core DbContext.
- Contains repositories.
- Contains database migrations.
- Contains VNPAY gateway implementation.
- Provides implementations for Application interfaces.

Cinema.Api
- Exposes HTTP endpoints.
- Configures authentication and authorization.
- Configures Swagger.
- Maps SignalR hubs.
- Handles global exceptions.

Cenima.IdentityApi
- Provides authentication using ASP.NET Identity and Duende IdentityServer.
- Issues access tokens.
- Adds user roles and permissions to tokens.

Cinema.SignalR
- Contains the seat notification hub and notification service.

Cinema.AI
- Provides a separate service for AI/chatbot functionality.


5. DOMAIN MODEL
---------------

The main booking relationship is:

    Movie
      |
      v
    Showtime
      |
      +---- Theater
      |       |
      |       v
      |      Seat
      |
      v
    ShowtimeSeat
      |
      v
    Booking
      |
      +---- BookingSeat
      |
      +---- BookingFood
      |
      v
    Payment

Important entities:
- Movie
- Genre
- MovieGenre
- Theater
- Seat
- Showtime
- ShowtimeSeat
- Booking
- BookingSeat
- Food
- BookingFood
- Payment

Seat status:
- Available
- Hold
- Booked

Booking status:
- Pending
- Confirmed
- Cancelled
- Expired

Payment status:
- Pending
- Success
- Failed
- Refunded

Payment methods currently defined:
- VnPay
- Momo
- ZaloPay


6. BOOKING FLOW
---------------

A typical booking flow is:

1. User authenticates through IdentityServer.
2. Client receives an access token.
3. Client calls the Cinema API with the Bearer token.
4. User selects a showtime and seats.
5. API verifies that the requested ShowtimeSeat records exist.
6. API checks that selected seats are available.
7. Food can be added to the booking.
8. Booking is created with Pending status.
9. Selected seats are held for the booking.
10. User creates a payment request.
11. API creates a VNPAY payment URL.
12. User is redirected to VNPAY.
13. VNPAY calls the IPN/callback endpoint.
14. API verifies the VNPAY signature.
15. Successful payment changes:
       Payment -> Success
       Booking -> Confirmed
16. Failed or expired bookings are handled accordingly.

Booking operations use Unit of Work / transaction handling where multiple
database operations must be completed as one logical operation.


7. REAL-TIME SEAT UPDATES
-------------------------

SignalR hub:

    /hubs/seat

The application contains a SeatHub and SeatNotificationService.

When a seat status changes, the notification service sends an event to the
SignalR group associated with the showtime.

Event name:

    SeatsStatusChanged

Event data contains:
- showtimeId
- SeatId
- status
- holdUntil

Clients can subscribe to the corresponding showtime group and update the
seat map in real time without continuously polling the API.


8. BOOKING EXPIRATION
---------------------

The project contains a BackgroundService named:

    BookingExprationService

It periodically calls the booking expiration service.

The current background process checks for expired bookings approximately
every 20 seconds.

Its purpose is to prevent seats from remaining held forever when a user
does not complete payment.

Conceptually:

    Pending Booking
          |
          | timeout
          v
       Expired
          |
          v
    Release held seats


9. PAYMENT / VNPAY
------------------

The payment layer uses the IPaymentGateway abstraction so that different
payment providers can be added without changing the main booking logic.

Current gateway:
- VnpayPaymentGateway

Payment creation:
- Creates a pending Payment record.
- Builds the VNPAY request.
- Generates the payment URL.
- Stores the payment URL.
- Returns the payment information to the API client.

Payment callback:
- Reads vnp_TxnRef to identify the booking.
- Finds the corresponding payment.
- Verifies the VNPAY secure hash.
- Checks transaction status and response code.
- Updates payment and booking status.

Endpoints:

    POST /api/Payment
    GET  /api/Payment/vnp/ipn
    GET  /api/Payment/vnp/return_url

IMPORTANT:
Never commit real VNPAY credentials, hash secrets, production API keys,
OpenAI keys, or other secrets to source control.

Use User Secrets or environment variables for sensitive configuration in
real deployments.


10. AUTHENTICATION
------------------

IdentityServer runs separately from the Cinema API.

Development HTTPS URLs configured in the project:

IdentityServer:
    https://localhost:5004

Cinema API:
    https://localhost:5000

Cinema AI:
    https://localhost:7076

The Cinema API validates JWT access tokens issued by IdentityServer.

Swagger is configured with OAuth 2.0 Authorization Code Flow and PKCE.

Swagger OAuth client:
    cinema-swagger

Scopes:
    openid
    profile
    cinema.read
    cinema.write


11. AUTHORIZATION / PERMISSIONS
-------------------------------

The API uses permission-based authorization.

Examples of permissions:

    movie.create
    movie.read
    movie.update
    movie.delete

    genre.create
    genre.read
    genre.update
    genre.delete

    theater.create
    theater.read
    theater.update
    theater.delete

    seat.create
    seat.read
    seat.update
    seat.delete

    showtime.create
    showtime.read
    showtime.update
    showtime.delete

    food.create
    food.read
    food.update
    food.delete

    booking.create
    booking.cancel

Permissions are added as claims and are checked by authorization policies
and the project's PermissionAttribute.


12. API MODULES
--------------

Cinema API controllers:

    /api/Booking
    /api/Food
    /api/Genre
    /api/Movie
    /api/Payment
    /api/Seat
    /api/Showtime
    /api/Theater

Most management modules provide:
- GET / pagination
- POST / create
- PUT / update
- DELETE / delete

For exact request/response models and available operations, use Swagger.


13. DATABASE
------------

The project uses SQL Server LocalDB during development.

Default development database:

    Database: CenimaDb

Current connection string is configured for:

    Server=(localdb)\MSSQLLocalDB

Before running the project, make sure SQL Server LocalDB is installed and
running.

EF Core migrations are included in:

    Cinema.Infrastructure/Migrations/


14. DATABASE MIGRATION
----------------------

From the solution directory:

    dotnet restore
    dotnet build

To apply the Cinema database migrations, run:

    dotnet ef database update \
        --project Cinema.Infrastructure \
        --startup-project Cinema.Api

If the EF CLI is not installed:

    dotnet tool install --global dotnet-ef


15. SEED DATA
-------------

The project contains database seed logic for:
- Genres
- Movies
- Theaters
- Seats
- Showtimes

IdentityServer also contains seed logic for:
- Users
- Roles
- Permissions
- IdentityServer configuration

On startup, the Identity API applies its IdentityServer-related migrations
and performs its seed operations.

If database initialization fails, check:
- SQL Server LocalDB is running.
- The connection string is correct.
- Required migrations have been applied.
- Foreign-key dependencies are seeded in the correct order.


16. HOW TO RUN
--------------

Prerequisites:

- .NET 8 SDK
- SQL Server LocalDB or SQL Server
- Visual Studio 2022 or another .NET IDE
- VNPAY Sandbox account/credentials if payment testing is required


Step 1 - Clone the project

    git clone <your-repository-url>

    cd CinemaProjectSolution


Step 2 - Configure the database

Update the DefaultConnection in the appropriate appsettings file.

Example:

    Server=(localdb)\MSSQLLocalDB;
    Database=CenimaDb;
    Trusted_Connection=True;
    TrustServerCertificate=True;


Step 3 - Configure VNPAY

Configure the VNPAY values in local configuration:

    VnpayOptions:
      vnp_TmnCode
      vnp_HashSecret
      vnp_Url
      ReturnUrl

Use VNPAY Sandbox credentials for local development.

Do not commit the real hash secret.


Step 4 - Apply migrations

    dotnet ef database update \
        --project Cinema.Infrastructure \
        --startup-project Cinema.Api


Step 5 - Start IdentityServer

Run:

    Cenima.IdentityApi

IdentityServer:

    https://localhost:5004


Step 6 - Start Cinema API

Run:

    Cinema.Api

Swagger:

    https://localhost:5000/swagger



17. RUNNING WITH VISUAL STUDIO
------------------------------

Open:

    CinemaProjectSolution.slnx

Recommended startup projects:

    Cenima.IdentityApi
    Cinema.Api

Cinema.AI can be started when AI functionality is required.

Make sure IdentityServer is running before testing authenticated API
requests.


18. SWAGGER
-----------

Cinema API Swagger:

    https://localhost:5000/swagger

Swagger supports OAuth 2.0 Authorization Code Flow with PKCE.

Typical flow:

    Swagger
       |
       v
    IdentityServer login
       |
       v
    Authorization code
       |
       v
    Access token
       |
       v
    Cinema API


19. TESTING
-----------

Unit tests are located in:

    Cinema.Application.Test/

The project uses:
- xUnit
- Moq

Current tests include booking handler scenarios such as:
- Showtime not found
- Showtime exists
- Seat already booked
- Seat currently held
- Seat not found
- Available seat / successful booking flow

Run all tests:

    dotnet test


20. DESIGN PATTERNS / PRACTICES
-------------------------------

The project uses several common backend development patterns:

Clean Architecture
- Separates domain, business logic, infrastructure and presentation.

CQRS-style MediatR
- Commands are used for state-changing operations.
- Queries are used for data retrieval.

Repository Pattern
- Application depends on repository interfaces.
- Infrastructure provides implementations.

Unit of Work
- Coordinates database changes and transactions.

Dependency Injection
- Services and repositories are registered through DI.

Gateway / Strategy Pattern
- Payment providers implement IPaymentGateway.
- PaymentService selects the appropriate gateway by PaymentMethod.

Background Service
- BookingExprationService handles expiration processing.

Claims-based Authorization
- Permissions are represented as claims and enforced through policies.

SignalR
- Provides real-time seat status updates.


21. DEVELOPMENT NOTES
---------------------

The project is currently intended primarily as a development / learning
project.

Before production deployment, consider:
- Moving all secrets to secure configuration.
- Using production signing credentials instead of developer signing
  credentials.
- Configuring HTTPS certificates properly.
- Configuring VNPAY production credentials and URLs.
- Improving payment idempotency for repeated callbacks.
- Adding stronger concurrency handling for seat booking.
- Adding more unit and integration tests.
- Adding structured logging and monitoring.
- Reviewing EF Core package versions so all projects use a consistent
  supported version.
- Adding Docker / CI/CD configuration if required.
- Configuring CORS according to the actual frontend domain.


22. PROJECT STATUS
------------------

Implemented:
- Clean Architecture structure
- CRUD modules for cinema data
- Booking workflow
- Seat status management
- Food attached to bookings
- JWT authentication
- IdentityServer
- Permission-based authorization
- SignalR seat notification
- Booking expiration background service
- VNPAY payment integration
- Swagger OAuth / PKCE
- Database migrations
- Seed data
- Initial unit tests


Planned / extendable:
- Additional payment gateways such as Momo and ZaloPay
- More comprehensive integration tests
- Production-grade distributed locking/concurrency control
- Redis-based caching
- Message broker integration
- Frontend application
- Production deployment and CI/CD


23. AUTHOR
----------

Cinema Booking System
Backend project built with ASP.NET Core .NET 8.


24. LICENSE
-----------

Add your preferred license here if this project will be published
publicly.
