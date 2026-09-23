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
- Employee summary and statistics
- Department-wise employee count
- Logging using Microsoft.Extensions.Logging
- Error handling
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
```

## Data Sources

### 1. PostgreSQL → Excel

Employee records are retrieved from PostgreSQL using Entity Framework Core and exported into a formatted Excel report.

The report includes:

- Employee details
- Salary information
- Joining dates
- Department information
- Employee statistics
- Department-wise employee count

### 2. REST API → Excel

The application can consume a REST API and export the returned JSON data into an Excel report.

The demo uses the JSONPlaceholder Users API.

## Usage

### Export All Employees

```bash
dotnet run
```

Retrieves all employees from PostgreSQL and generates an Excel report.

### Filter by Department

```bash
dotnet run -- IT
```

Generates a report containing employees from the IT department.

Other departments can also be supplied, for example:

```bash
dotnet run -- HR
```

### Filter by Department and Joining Date

```bash
dotnet run -- IT 2024-01-01 2024-12-31
```

Filters employees by department and joining date range.

### Export REST API Data

```bash
dotnet run -- api
```

Calls the configured REST API and generates an Excel report.

## Excel Reports

Generated reports are stored in the application's `Reports` directory.

Example:

```text
Reports/
├── EmployeeReport_yyyyMMdd_HHmmss.xlsx
└── ApiUserReport_yyyyMMdd_HHmmss.xlsx
```

### Employee Report

The employee report contains:

- Summary sheet
- Employee details sheet
- Total employee count
- Average salary
- Highest salary
- Lowest salary
- Department-wise employee count
- Excel filtering
- Formatted salary
- Formatted joining dates
- Frozen headers
- Excel table formatting

### API User Report

The API report contains:

- User ID
- Name
- Username
- Email
- Phone
- Website
- Excel table formatting
- Filters
- Frozen headers

## Configuration

The REST API URL is configured in `appsettings.json`:

```json
{
  "ApiSettings": {
    "UsersApiUrl": "https://jsonplaceholder.typicode.com/users"
  }
}
```

The PostgreSQL connection string is **not stored in the source code**.

Set the following environment variable before running the application:

```text
POSTGRES_CONNECTION_STRING
```

Example:

```text
Host=localhost;Port=5432;Database=ApiExcelAutomation;Username=postgres;Password=YOUR_PASSWORD
```

> Do not commit database passwords, API keys, or other secrets to source control.

## Database Setup

Make sure PostgreSQL is installed and running.

Configure the `POSTGRES_CONNECTION_STRING` environment variable.

Run the EF Core database migration:

```bash
dotnet ef database update
```

The application also applies pending migrations during startup.

## Running the Application

Clone the repository:

```bash
git clone https://github.com/Mahesh-DotnetAngularDev/ApiExcelAutomation.git
```

Navigate to the project:

```bash
cd ApiExcelAutomation
```

Restore dependencies:

```bash
dotnet restore
```

Build the project:

```bash
dotnet build
```

Run the application:

```bash
dotnet run
```

## Error Handling and Logging

The application includes logging and error handling for important operations such as:

- Database operations
- REST API calls
- Excel report generation
- Application execution

Logs are written to the console to help identify errors and troubleshoot failures.

## Use Cases

This type of automation can be useful for:

- Database-to-Excel reporting
- API-to-Excel automation
- Employee reports
- Business reports
- Data extraction
- Data transformation
- Operational reporting
- Automated Excel generation
- Recurring business reports

## Future Enhancements

Potential improvements include:

- CSV export
- Multiple API integrations
- Scheduled report generation
- Email delivery of reports
- Excel charts
- Configurable report templates
- Additional database providers
- Custom Excel templates
- Automated report distribution

## Security

The project follows basic security practices:

- Database credentials are provided through environment variables.
- Secrets are not stored in source control.
- API configuration is separated from application logic.
- `.gitignore` excludes build and development files.

## Author

**Mahesh Rath**

.NET Developer | ASP.NET Core | C# | PostgreSQL
