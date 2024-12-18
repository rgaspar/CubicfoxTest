# Cubicfox Test Web API Project

## About the Project

Build a RESTful API that provides basic CRUD functionality for tracking your time. One should be able
to start and stop timer. Each start+stop action should generate a time log object. Start time and stop
time should be saved as well as the today's date. Time log can be updated even if it is running or is
already stopped. When starting the timer, default description should be random quote generated
from https://zenquotes.io/ api. Time logs can also be deleted. The estimated implementation time for
the solution is 3-8 hours.

## Start
The main docker compose file is in root directory of a repository.
First one has to create local network:

```
docker network create cubicfoxnetwork
```
To start Backend using `docker-compose` (see more: https://docs.docker.com/compose/) command:

```
docker-compose up --build
``` 
Read more: https://docs.microsoft.com/pl-pl/dotnet/architecture/microservices/multi-container-microservice-net-applications/multi-container-applications-docker-compose

To stop container, use
```
docker-compose down
```

In this way you are starting 3 containers:
- cubicfox-api
- cubicfox-sqlserver

`cubicfox-api` is Web API container, that serves Web API;
It depends on `cubicfox-sqlserver` - we've used MS SQL Server 2022 Image (mcr.microsoft.com/mssql/server:2022-latest).

All work for creating databases done in `docker-entrypoint.sh`, which starts MS SQL server and executes script from `cubicfox-db-init.sql` using
`/db-init.sh & /opt/mssql/bin/sqlservr`.

In such a way `docker-db-init` starts MS SQL Server, creates the datababase.

Job's done. You have you database, you can create more databses in `cubicfox-db-init.sql`.

## Main Features

- [x] One should use .NET 9 Web API project (do not use minimal API, use Controllers)
- [x] For data layer use EF Core with »Code first« approach with the necessary migrations for
  database (only 1 table: TimeLogs). Database can be local file
- [x] Use dependency injection
- [x] Follow the REST principles
- [x] API should expose endpoints: 
  - Get all time logs with ability to filter by date range
  - Start/stop timer. Keep in mind that description (random quote) should be default
    description when starting timer
  - Update a single time log (eg. by Id/Guid) – ability to update description – in this case,
    description should be passed in request body (not from zenquotes API). BONUS:
    ability to update start/end time and date as well.
  - Delete a single time log – ability to delete the time log (eg. by Id/Guid).
- [x] Include basic validation (e.g., prevent multiple running timers). BONUS: global error handler
  to catch validation errors
- [x] Provide API documentation using Swagger
- [x] Use Github - Sensible code committing with clear commit messages is required. Also include
  README which should include steps on how to build application locally.
- [x] BONUS: add some unit tests
- [x] BONUS: add pagination to GET endpoints

# Getting Started

### Prerequisites

- .NET 9.0 SDK

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/rgaspar/CubicfoxTest.git
   ```

