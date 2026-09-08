# Smart Appointment System

A multi-layer **ASP.NET Core Web API built on .NET 10** for managing doctors, patients, working hours, and appointments, with **OpenAI-powered natural-language appointment search**.

Patients can search for available appointment times in Turkish, for example:

> Yarın 14'ten sonra kardiyoloji randevularını getir.

The AI extracts the medical branch, date, and time filter. The application then queries PostgreSQL and returns available times with doctor information. Appointment search is read-only; booking is a separate API operation.

## Live API

- [API base URL](https://smartappointmentsystem.onrender.com)
- [Doctor listing](https://smartappointmentsystem.onrender.com/api/doctor/all)
- [Health check](https://smartappointmentsystem.onrender.com/health)

The API is hosted on **Render**, with **Supabase PostgreSQL** as the hosted database. This repository contains the backend API, not a web frontend; the root URL is not a landing page.

## Features

- Patient and doctor registration and login, BCrypt password hashing, and JWT authentication for protected endpoints.
- Appointment creation, retrieval, updates, and deletion.
- Medical branch and doctor search, weekly doctor schedules, and appointment intervals.
- Doctor ratings and reviews.
- AI chat and Turkish natural-language appointment search using the OpenAI Responses API.
- Structured extraction of search filters using a strict JSON schema.
- Asynchronous database and AI operations with `CancellationToken` support.
- `AsNoTracking()` for read-only queries, request logging, centralized exception handling, and rate limiting on AI endpoints.
- Docker deployment, EF Core migrations, and a health-check endpoint.

## Technology Stack

- .NET 10 / ASP.NET Core Web API
- Entity Framework Core 10 with the Npgsql PostgreSQL provider
- PostgreSQL / Supabase
- OpenAI .NET SDK, Responses API, and Structured Outputs
- JWT Bearer authentication and BCrypt.Net-Next
- FluentValidation
- Dependency Injection and the Options Pattern
- Docker / Render
- `.slnx` solution format

## Architecture

| Project | Responsibility |
| --- | --- |
| `SmartAppointmentSystem.Api` | HTTP endpoints, request models, authentication, middleware, and dependency registration. |
| `SmartAppointmentSystem.Business` | Service contracts, business logic, and request/response DTOs. |
| `SmartAppointmentSystem.Data` | Entities, EF Core configurations, `AppointmentContext`, and migrations. |
| `SmartAppointmentSystem.Infrastructure` | OpenAI integration through `OpenAiChatService` and `OpenAiOptions`. |

The Business layer declares `IAiChatService`; Infrastructure implements it. The API registers the implementation through dependency injection, keeping OpenAI SDK details outside the business contract. Business services currently access EF Core through the Data project.

### How AI Appointment Search Works

1. A client sends a prompt to `POST /api/ai/appointment-search`.
2. `AiController` calls `IAiChatService.ParseAppointmentSearchAsync`.
3. `OpenAiChatService` sends the prompt, date context, and JSON schema to OpenAI through the .NET SDK.
4. The structured response is deserialized into `AppointmentSearchIntent`: `branch`, `date`, and `afterTime`.
5. `IAppointmentService` queries doctor schedules and existing appointments in PostgreSQL and calculates matching available times.
6. The API returns the branch, date, and matching doctor/time entries.

OpenAI interprets the request; it does not query the database, generate the doctor list, or create an appointment. The integration uses [Structured Outputs](https://developers.openai.com/api/docs/guides/structured-outputs) to constrain the extracted filter format. It does not currently use function calling, RAG, a vector database, or agents.

## Local Setup

### 1. Prerequisites and Clone

Install the .NET 10 SDK and have a PostgreSQL database available locally or through Supabase. AI endpoints also require an OpenAI API key with access to the configured model.

```bash
git clone https://github.com/RidvanOzturk/SmartAppointmentSystem.git
cd SmartAppointmentSystem
dotnet restore SmartAppointmentSystem.slnx
```

### 2. Configure Secrets

Run these commands from the repository root, replacing all placeholders with your own values. Use a strong, randomly generated JWT signing secret containing at least 32 bytes.

```bash
dotnet user-secrets set "ConnectionStrings:AppointmentContext" "Host=localhost;Port=5432;Database=SmartAppointmentDb;Username=postgres;Password=<DATABASE_PASSWORD>;" --project SmartAppointmentSystem.Api
dotnet user-secrets set "AppSettings:Secret" "<RANDOM_JWT_SIGNING_SECRET_AT_LEAST_32_BYTES>" --project SmartAppointmentSystem.Api
dotnet user-secrets set "OpenAI:ApiKey" "<OPENAI_API_KEY>" --project SmartAppointmentSystem.Api
```

The API project already has a `UserSecretsId`; no additional initialization is needed. User Secrets are loaded in the Development environment and are stored outside the repository. Do not commit database passwords, API keys, or JWT signing secrets to `appsettings.json`.

`OpenAiOptions.Model` currently defaults to `gpt-5.6-luna`. To override it, configure a model available to your API project that supports the Responses API and Structured Outputs:

```bash
dotnet user-secrets set "OpenAI:Model" "<MODEL_ID>" --project SmartAppointmentSystem.Api
```

Non-secret token settings (`TokenSettings:Issuer`, `TokenSettings:Audience`, and `TokenSettings:ExpiresInMinutes`) are already provided in `appsettings.json`.

### 3. Build and Run

```bash
dotnet build SmartAppointmentSystem.slnx
dotnet run --project SmartAppointmentSystem.Api --launch-profile http
```

- Local API: `http://localhost:5105`
- Health check: `http://localhost:5105/health`
- OpenAPI document, Development only: `http://localhost:5105/openapi/v1.json`

The application applies existing EF Core migrations at startup using `Database.MigrateAsync()`. The configured database must be reachable and the database user must have the required permissions. **Do not create another `InitialCreate` migration when installing the project.**

To apply the existing migrations explicitly before startup, install a compatible EF Core 10 CLI tool and run:

```bash
dotnet ef database update --project SmartAppointmentSystem.Data --startup-project SmartAppointmentSystem.Api -- --environment Development
```

### 4. Optional Demo Data

After the schema exists, `scripts/seed-demo-data.sql` can populate a demo database with fictional branches, doctors, patients, weekly schedules, appointments, and ratings. It adds missing fixtures without updating or deleting existing records; identity or schedule conflicts abort the transaction.

For Supabase, run the file in the intended project's SQL Editor. For local PostgreSQL or Supabase through `psql`, use `scripts/Seed-DemoData.ps1` with the appropriate target. For example, in PowerShell with `psql` on `PATH`:

```powershell
$env:PGHOST = 'localhost'
$env:PGPORT = '5432'
$env:PGDATABASE = 'SmartAppointmentDb'
$env:PGUSER = 'postgres'
./scripts/Seed-DemoData.ps1 -Target Local -ValidateOnly
./scripts/Seed-DemoData.ps1 -Target Local
```

The script prompts for the database password when no password environment variable or password file is configured. `-ValidateOnly` rolls back the inserts, although identity sequences may advance. For Supabase, use its database/pooler host and connection details, set `PGSSLMODE=require` (or a stricter verified TLS mode), and pass `-Target Supabase`.

Only seed a database intended for demo/testing: these fixtures contain publicly documented demo credentials. The scripts are not run automatically on startup.

## API Usage

### Search Available Appointments with AI

Send this JSON body with `Content-Type: application/json` to `POST /api/ai/appointment-search`:

```json
{
  "prompt": "Yarın 14'ten sonra kardiyoloji randevularını getir"
}
```

Illustrative response, assuming the request is made on September 8, 2026 and matching schedules exist:

```json
{
  "branch": "Kardiyoloji",
  "date": "2026-09-09",
  "appointments": [
    {
      "doctorId": "0a76e482-034f-3518-df61-6e44133dbc3e",
      "doctorName": "Emre Demir",
      "time": "14:30:00"
    }
  ]
}
```

The actual result depends on database contents. Relative dates are resolved using Turkey's UTC+3 date context. “14'ten sonra” excludes 14:00 itself. No matches return `200 OK` with an empty `appointments` array; a missing branch or date returns `400 Bad Request`. Prompts are limited to 4,000 characters.

### Selected Endpoints

| Method | Route | Purpose | JWT required |
| --- | --- | --- | --- |
| GET | `/health` | Application health check | No |
| GET | `/api/doctor/all` | List doctors | No |
| GET | `/api/branch/all` | List medical branches | No |
| POST | `/api/patient/signup` | Register a patient | No |
| POST | `/api/patient/login` | Log in as a patient | No |
| POST | `/api/doctor/signup` | Register a doctor | No |
| POST | `/api/doctor/login` | Log in as a doctor | No |
| POST | `/api/ai/chat` | Send a prompt and receive a text response | No |
| POST | `/api/ai/appointment-search` | Search available appointment times | No |
| POST | `/api/appointment` | Create an appointment for the authenticated patient | Yes |
| GET | `/api/appointment/all` | List appointments | Yes |

Both AI endpoints accept `{ "prompt": "..." }`. The chat endpoint returns `{ "response": "..." }`. They currently have a shared fixed-window rate limit of 20 requests per 10 seconds, but do not require authentication.

For protected endpoints, log in and send the returned JWT in `Authorization: Bearer <TOKEN>`. In Postman, select **Authorization → Bearer Token**. Use the application's login token, not your OpenAI API key. Booking remains a separate authenticated request after selecting a search result.

## Deployment: Render + Supabase

The repository includes a multi-stage Dockerfile using the .NET 10 SDK for publishing and the ASP.NET Core 10 runtime for execution.

Configure the following environment variables in the Render service. Keep secret values in the hosting platform, not in Git or the Docker image.

| Environment variable | Value / purpose |
| --- | --- |
| `ASPNETCORE_ENVIRONMENT` | `Production` |
| `ConnectionStrings__AppointmentContext` | Supabase **Session Pooler** connection string, port `5432`, with TLS enabled |
| `AppSettings__Secret` | Strong random JWT signing secret, at least 32 bytes |
| `OpenAI__ApiKey` | OpenAI API key |
| `OpenAI__Model` | Optional override of the model configured in `OpenAiOptions` |

Double underscores map environment variables to nested .NET configuration keys. Development User Secrets are not deployed to Render.

For this Render deployment, select **Session Pooler** in the Supabase project's **Connect** panel. The default direct endpoint uses IPv6 and can fail with `Network is unreachable` on an IPv4-only network; the shared Session Pooler supports IPv4. See the [Supabase connection guide](https://supabase.com/docs/guides/database/connecting-to-postgres).

Example Npgsql value for `ConnectionStrings__AppointmentContext` (replace placeholders using your project's Session Pooler details):

```text
Host=<SESSION_POOLER_HOST>;Port=5432;Database=postgres;Username=postgres.<PROJECT_REF>;Password=<DATABASE_PASSWORD>;SSL Mode=Require;
```

Copy the exact pooler hostname from Supabase; do not use the direct `db.<PROJECT_REF>.supabase.co` hostname or the Transaction Pooler port `6543` for this configuration.

The Dockerfile already sets `ASPNETCORE_HTTP_PORTS=10000`, so no additional port-binding environment variable is needed. It also sets `DOTNET_HOSTBUILDER__RELOADCONFIGONCHANGE=false`, which disables automatic configuration-file watching to avoid the inotify watcher limit encountered in the deployment environment. Use `/health` as the service health-check path; it is not a database or OpenAI connectivity check.

The deployed application also runs migrations at startup. Back up important data and review pending migrations before deploying against a populated database.

## Current Boundaries

- AI search does not reserve a slot or guarantee that it remains free until booking.
- Availability currently compares stored appointment times with schedule times without an explicit UTC-to-local conversion. Verify time-zone consistency before relying on occupied-slot filtering across environments.
- Public AI endpoints should receive appropriate access controls and usage protections before serving unrestricted production traffic.
- This is an appointment-management project, not a medical diagnosis or treatment service. Use fictional data and avoid sending sensitive patient information in prompts.



