# API Excel Automation

A .NET 10 console application that automates Excel report generation from PostgreSQL database records and REST APIs.

The project demonstrates how business data can be retrieved from different sources, processed using C#, and exported into professionally formatted Excel reports.

## Features

- PostgreSQL database integration using Entity Framework Core
- REST API integration using HttpClient
- Excel report generation using ClosedXML
- Employee data export from PostgreSQL to Excel
- REST API user data export to Excel
- Department-based filtering
- Joining date range filtering
- Excel tables with filters
- Formatted currency and dates
- Summary sheet with employee statistics
- Logging using Microsoft.Extensions.Logging
- Global error handling
- EF Core database migrations
- Configuration using `appsettings.json`
- Secure PostgreSQL connection using environment variables

## Technologies

- .NET 10
- C#
- Entity Framework Core
- PostgreSQL
- Npgsql
- ClosedXML
- REST APIs
- JSON
- Microsoft.Extensions.Configuration
- Microsoft.Extensions.Logging

## Project Structure

```text
ApiExcelAutomation
│
├── Data
│   ├── AppDbContext.cs
│   └── AppDbContextFactory.cs
│
├── Migrations
│   └── EF Core migrations
│
├── Models
│   ├── Employee.cs
│   └── ApiUser.cs
│
├── Services
│   ├── IExcelServices.cs
│   ├── ExcelServices.cs
│   ├── IApiServices.cs
│   └── ApiServices.cs
│
├── Utilities
│   └── ApplicationRunner.cs
│
├── Program.cs
├── appsettings.json
└── README.md
