using ApiExcelAutomation.Models;

namespace ApiExcelAutomation.Services;

public interface IApiService
{
    Task<List<ApiUser>> GetUsersAsync();
}