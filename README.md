# AcerPro
Interview Project

# Blazor Web Assembly Hosted Application

This is a Blazor Web Assembly Hosted application that provides account management functionality. Each user has the ability to add, edit, and delete target apps. Users can also assign multiple notifiers to each target app and configure the time interval for checking their health status.

The application is implemented following the principles of Domain-Driven Design (DDD), Test-Driven Development (TDD), and Command Query Responsibility Segregation (CQRS). All domain logics are covered by unit tests to ensure their correctness.

## Features

- Account management functionality
- Ability to add, edit, and delete target apps
- Assign multiple notifiers to each target app
- Configuration of time interval for checking target app health status

## Technologies Used

- Blazor Web Assembly
- DDD (Domain-Driven Design)
- TDD (Test-Driven Development)
- CQRS (Command Query Responsibility Segregation)
- SQL Server (as the database)
- Quartz (job scheduler)

## Getting Started

To run the application, follow these steps:

1. Ensure you have Docker installed on your machine.
2. Clone this repository to your local machine.
3. Open a terminal and navigate to the project directory.
4. Run the following command to start the application using Docker Compose:

```bash
docker-compose up
