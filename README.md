# Smart (Hospital) Appointment System

## Overview

This project is a multi-layer .NET Web API designed as a smart appointment system. In this system, patients can create appointments with doctors according to time, and the API efficiently handles these requests.
The application is built with a clean separation of concerns, with distinct layers for data access, business logic, and presentation.

## Features

- **Appointment Booking:** Patients can schedule appointments with doctors.
- **Multi-layer Architecture:** Organized into separate layers for easier maintenance and scalability.
- **Entity Framework Integration:** Utilizes Entity Framework for robust database interactions.
- **Performance Optimizations:** Uses efficient LINQ queries with `AsNoTracking()` for read-only operations to enhance query speed and reduce memory usage.
- **End-to-End Deployment:** Both the application and its database are deployed, ensuring a complete solution.

## Technologies Used

- .NET 9
- Entity Framework Core
- PostgreSQL (both development and production)
- LINQ for data queries
- RESTful API design principles

## Installation

1. **Clone the Repository:**

   ```bash
   git clone https://github.com/RidvanOzturk/SmartAppointmentSystem.git
   cd smart-appointment-system
   
2. **Configure the Database**

- **"AppointmentContext": "Host=localhost;Port=5432;Database=SmartAppointmentDb;Username=postgres;Password=*******;"** in the `appsettings.json` and environment-specific files.
- **Run the EF Core migrations** to set up the database schema:

  ```bash
  dotnet ef migrations add InitialCreate
  dotnet ef database update
  ```
  
 3. **Build and Run**

  ```bash
  dotnet build
  dotnet run
  ```

 4. **Usage**
  API Endpoints:

Use the provided endpoints to create, read, update, and delete appointments and doctor records.
Example: GET /api/doctors/all returns a list of all doctors.

Testing with Postman:
Example: https://smartappointmentsystem.up.railway.app/api/doctor/all

## Deployment
Both the API and the database have been deployed in a cloud environment. 

## Performance Optimization
For read-only queries, the application uses AsNoTracking() in LINQ queries to improve performance by reducing the overhead of EF Core's change tracking.



