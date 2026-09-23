using ApiExcelAutomation.Models;

namespace ApiExcelAutomation.Services;

public interface IExcelService
{
    string GenerateEmployeeReport(List<Employee> employees);
    string GenerateApiUserReport(List<ApiUser> users);
}