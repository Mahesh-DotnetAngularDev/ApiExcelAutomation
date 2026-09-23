using ApiExcelAutomation.Data;
using ApiExcelAutomation.Models;
using ApiExcelAutomation.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Globalization;


using var loggerFactory =
    LoggerFactory.Create(builder =>
    {
        builder
            .AddConsole()
            .SetMinimumLevel(LogLevel.Information);
    });

if (args.Length > 0 &&
    args[0].Equals("api", StringComparison.OrdinalIgnoreCase))
{
    await GenerateApiReportAsync(
    loggerFactory);
    return;
}
var connectionString =
    Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING");

if (string.IsNullOrWhiteSpace(connectionString))
{
    Console.WriteLine(
        "POSTGRES_CONNECTION_STRING environment variable is not configured.");

    return;
}


try
{
    var options = new DbContextOptionsBuilder<AppDbContext>()
        .UseNpgsql(connectionString)
        .Options;

    using var context = new AppDbContext(options);

    // Ensure the database is up to date.
    context.Database.Migrate();

    Console.WriteLine("Database migration completed.");

    // Retrieve employees from PostgreSQL.
    var department = args.Length > 0
    ? args[0]
    : "All";

    DateTime? startDate = null;
    DateTime? endDate = null;

    if (args.Length >= 2)
    {
        if (!DateTime.TryParseExact(
                args[1],
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var parsedStartDate))
        {
            Console.WriteLine(
                "Invalid start date. Use yyyy-MM-dd format.");

            return;
        }

        startDate = parsedStartDate;
    }

    if (args.Length >= 3)
    {
        if (!DateTime.TryParseExact(
                args[2],
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var parsedEndDate))
        {
            Console.WriteLine(
                "Invalid end date. Use yyyy-MM-dd format.");

            return;
        }

        endDate = parsedEndDate;
    }

    if (startDate.HasValue &&
        endDate.HasValue &&
        startDate > endDate)
    {
        Console.WriteLine(
            "Start date cannot be later than end date.");

        return;
    }

    var query = context.Set<Employee>()
        .AsNoTracking()
        .AsQueryable();

    if (!department.Equals(
            "All",
            StringComparison.OrdinalIgnoreCase))
    {
        query = query.Where(e =>
            e.Department == department);
    }

    if (startDate.HasValue)
    {
        query = query.Where(e =>
            e.JoiningDate >= startDate.Value);
    }

    if (endDate.HasValue)
    {
        query = query.Where(e =>
            e.JoiningDate <= endDate.Value);
    }

    var employees = await query
        .OrderBy(e => e.Id)
        .ToListAsync();

    Console.WriteLine(
        $"Retrieved {employees.Count} employees from PostgreSQL.");

    if (string.IsNullOrWhiteSpace(department) ||
    department.Equals("All", StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine("Report scope: All departments");
    }
    else
    {
        Console.WriteLine($"Report scope: {department}");
    }
    Console.WriteLine($"Report department: {department}");

    if (startDate.HasValue)
    {
        Console.WriteLine(
            $"Start date: {startDate.Value:dd-MMM-yyyy}");
    }

    if (endDate.HasValue)
    {
        Console.WriteLine(
            $"End date: {endDate.Value:dd-MMM-yyyy}");
    }

    Console.WriteLine(
        $"Retrieved {employees.Count} employees from PostgreSQL.");
    if (employees.Count == 0)
    {
        Console.WriteLine("No employee records found.");
        return;
    }

    var excelLogger =
    loggerFactory.CreateLogger<ExcelService>();

    IExcelService excelService =
        new ExcelService(excelLogger);

    var filePath = excelService.GenerateEmployeeReport(employees);

    Console.WriteLine();
    Console.WriteLine("Excel report generated successfully!");
    Console.WriteLine($"File: {filePath}");
}
catch (Exception ex)
{
    Console.WriteLine();
    Console.WriteLine("An error occurred while generating the report.");
    Console.WriteLine($"Error: {ex.Message}");
}

static async Task GenerateApiReportAsync(
    ILoggerFactory loggerFactory)
{
    try
    {
        var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

        var usersApiUrl =
            configuration["ApiSettings:UsersApiUrl"];

        if (string.IsNullOrWhiteSpace(usersApiUrl))
        {
            Console.WriteLine("Users API URL is not configured.");
            return;
        }
        using var httpClient = new HttpClient();

        var apiLogger =
    loggerFactory.CreateLogger<ApiService>();

        IApiService apiService =
            new ApiService(
                httpClient,
                usersApiUrl,
                apiLogger);

        var users = await apiService.GetUsersAsync();

        Console.WriteLine(
            $"Retrieved {users.Count} records from REST API.");

        if (users.Count == 0)
        {
            Console.WriteLine("No records returned by the API.");
            return;
        }

        var excelLogger =
    loggerFactory.CreateLogger<ExcelService>();

        IExcelService excelService =
            new ExcelService(excelLogger);

        var filePath =
            excelService.GenerateApiUserReport(users);

        Console.WriteLine();
        Console.WriteLine(
            "API Excel report generated successfully!");

        Console.WriteLine($"File: {filePath}");
    }
    catch (Exception ex)
    {
        Console.WriteLine();
        Console.WriteLine(
            $"API report generation failed: {ex.Message}");
    }
}

Console.WriteLine();
Console.WriteLine("Press any key to exit...");
Console.ReadKey();