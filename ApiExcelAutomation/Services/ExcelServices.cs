using ApiExcelAutomation.Models;
using ClosedXML.Excel;
using Microsoft.Extensions.Logging;

namespace ApiExcelAutomation.Services;

public class ExcelService : IExcelService
{
    private readonly ILogger<ExcelService> _logger;

    public ExcelService(ILogger<ExcelService> logger)
    {
        _logger = logger;
    }
    public string GenerateEmployeeReport(List<Employee> employees)
    {
        try
        {
            var reportsFolder = Path.Combine(
                AppContext.BaseDirectory,
                "Reports");

            Directory.CreateDirectory(reportsFolder);

            var fileName =
                $"EmployeeReport_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

            var filePath = Path.Combine(reportsFolder, fileName);

            using var workbook = new XLWorkbook();

            CreateSummarySheet(workbook, employees);
            CreateEmployeeSheet(workbook, employees);
            _logger.LogInformation(
        "Generating employee Excel report for {Count} employees.",
        employees.Count);


            workbook.SaveAs(filePath);

            _logger.LogInformation(
        "Employee Excel report generated: {FilePath}",
        filePath);

            return filePath;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to generate employee Excel report.");

            throw;
        }
    }

    private static void CreateSummarySheet(
        XLWorkbook workbook,
        List<Employee> employees)
    {
        var worksheet = workbook.Worksheets.Add("Summary");

        worksheet.Cell("A1").Value = "Employee Report";
        worksheet.Range("A1:D1").Merge();

        worksheet.Cell("A2").Value =
            $"Generated On: {DateTime.Now:dd-MMM-yyyy HH:mm:ss}";
        worksheet.Range("A2:D2").Merge();

        worksheet.Cell("A4").Value = "Report Summary";
        worksheet.Range("A4:B4").Merge();

        worksheet.Cell("A5").Value = "Total Employees";
        worksheet.Cell("B5").Value = employees.Count;

        worksheet.Cell("A6").Value = "Average Salary";
        worksheet.Cell("B6").Value =
            employees.Count > 0
                ? employees.Average(e => e.Salary)
                : 0;

        worksheet.Cell("A7").Value = "Highest Salary";
        worksheet.Cell("B7").Value =
            employees.Count > 0
                ? employees.Max(e => e.Salary)
                : 0;

        worksheet.Cell("A8").Value = "Lowest Salary";
        worksheet.Cell("B8").Value =
            employees.Count > 0
                ? employees.Min(e => e.Salary)
                : 0;

        worksheet.Cell("A10").Value = "Department";
        worksheet.Cell("B10").Value = "Employee Count";

        var departmentCounts = employees
            .GroupBy(e => e.Department)
            .OrderBy(g => g.Key);

        var row = 11;

        foreach (var department in departmentCounts)
        {
            worksheet.Cell(row, 1).Value = department.Key;
            worksheet.Cell(row, 2).Value = department.Count();

            row++;
        }

        // Title formatting
        worksheet.Range("A1:D1").Style.Font.Bold = true;
        worksheet.Range("A1:D1").Style.Font.FontSize = 18;

        worksheet.Range("A4:B4").Style.Font.Bold = true;
        worksheet.Range("A10:B10").Style.Font.Bold = true;

        // Summary formatting
        worksheet.Range("A5:A8").Style.Font.Bold = true;

        worksheet.Range("B6:B8")
            .Style.NumberFormat.Format = "₹#,##0.00";

        worksheet.Range($"A4:B{row - 1}")
            .Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

        worksheet.Range($"A4:B{row - 1}")
            .Style.Border.InsideBorder = XLBorderStyleValues.Thin;

        worksheet.Columns().AdjustToContents();
    }

    private static void CreateEmployeeSheet(
        XLWorkbook workbook,
        List<Employee> employees)
    {
        var worksheet = workbook.Worksheets.Add("Employees");

        worksheet.Cell("A1").Value = "Employee Report";
        worksheet.Range("A1:F1").Merge();

        worksheet.Cell("A2").Value =
            $"Generated On: {DateTime.Now:dd-MMM-yyyy HH:mm:ss}";
        worksheet.Range("A2:F2").Merge();

        worksheet.Cell("A4").Value = "ID";
        worksheet.Cell("B4").Value = "Name";
        worksheet.Cell("C4").Value = "Email";
        worksheet.Cell("D4").Value = "Department";
        worksheet.Cell("E4").Value = "Salary";
        worksheet.Cell("F4").Value = "Joining Date";

        var row = 5;

        foreach (var employee in employees)
        {
            worksheet.Cell(row, 1).Value = employee.Id;
            worksheet.Cell(row, 2).Value = employee.Name;
            worksheet.Cell(row, 3).Value = employee.Email;
            worksheet.Cell(row, 4).Value = employee.Department;
            worksheet.Cell(row, 5).Value = employee.Salary;
            worksheet.Cell(row, 6).Value = employee.JoiningDate;

            row++;
        }

        var lastRow = row - 1;

        // Title
        worksheet.Range("A1:F1").Style.Font.Bold = true;
        worksheet.Range("A1:F1").Style.Font.FontSize = 18;

        // Header
        var headerRange = worksheet.Range("A4:F4");

        headerRange.Style.Font.Bold = true;

        headerRange.Style.Alignment.Horizontal =
            XLAlignmentHorizontalValues.Center;

        // Currency
        worksheet.Column("E")
            .Style.NumberFormat.Format = "₹#,##0.00";

        // Date
        worksheet.Column("F")
            .Style.DateFormat.Format = "dd-MMM-yyyy";

        // Create Excel table
        if (lastRow >= 4)
        {
            worksheet.Range($"A4:F{lastRow}")
                .CreateTable("EmployeeTable");
        }

        // Borders
        if (lastRow >= 4)
        {
            worksheet.Range($"A4:F{lastRow}")
                .Style.Border.OutsideBorder =
                XLBorderStyleValues.Thin;

            worksheet.Range($"A4:F{lastRow}")
                .Style.Border.InsideBorder =
                XLBorderStyleValues.Thin;
        }

        // Freeze header
        worksheet.SheetView.FreezeRows(4);

        // Auto-size
        worksheet.Columns().AdjustToContents();

        // Reasonable maximum widths
        worksheet.Column("B").Width = 25;
        worksheet.Column("C").Width = 35;
        worksheet.Column("D").Width = 20;
    }
    public string GenerateApiUserReport(List<ApiUser> users)
    {
        try
        {
            var reportsFolder = Path.Combine(
                AppContext.BaseDirectory,
                "Reports");

            Directory.CreateDirectory(reportsFolder);

            var fileName =
                $"ApiUserReport_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

            var filePath = Path.Combine(reportsFolder, fileName);

            using var workbook = new XLWorkbook();

            var worksheet = workbook.Worksheets.Add("API Users");

            worksheet.Cell("A1").Value = "API User Report";
            worksheet.Range("A1:F1").Merge();

            worksheet.Cell("A2").Value =
                $"Generated On: {DateTime.Now:dd-MMM-yyyy HH:mm:ss}";

            worksheet.Range("A2:F2").Merge();

            worksheet.Cell("A4").Value = "ID";
            worksheet.Cell("B4").Value = "Name";
            worksheet.Cell("C4").Value = "Username";
            worksheet.Cell("D4").Value = "Email";
            worksheet.Cell("E4").Value = "Phone";
            worksheet.Cell("F4").Value = "Website";

            var row = 5;

            foreach (var user in users)
            {
                worksheet.Cell(row, 1).Value = user.Id;
                worksheet.Cell(row, 2).Value = user.Name;
                worksheet.Cell(row, 3).Value = user.Username;
                worksheet.Cell(row, 4).Value = user.Email;
                worksheet.Cell(row, 5).Value = user.Phone;
                worksheet.Cell(row, 6).Value = user.Website;

                row++;
            }

            var lastRow = row - 1;

            worksheet.Range("A1:F1").Style.Font.Bold = true;
            worksheet.Range("A1:F1").Style.Font.FontSize = 18;

            worksheet.Range("A4:F4").Style.Font.Bold = true;

            if (lastRow >= 4)
            {
                worksheet.Range($"A4:F{lastRow}")
                    .CreateTable("ApiUserTable");

                worksheet.Range($"A4:F{lastRow}")
                    .Style.Border.OutsideBorder =
                    XLBorderStyleValues.Thin;

                worksheet.Range($"A4:F{lastRow}")
                    .Style.Border.InsideBorder =
                    XLBorderStyleValues.Thin;
            }

            worksheet.Columns().AdjustToContents();

            worksheet.Column("D").Width = 35;

            worksheet.SheetView.FreezeRows(4);

            _logger.LogInformation(
        "Generating API user Excel report for {Count} users.",
        users.Count);

            workbook.SaveAs(filePath);

            _logger.LogInformation(
        "API user Excel report generated: {FilePath}",
        filePath);

            return filePath;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to generate API user Excel report.");

            throw;
        }
    }
}