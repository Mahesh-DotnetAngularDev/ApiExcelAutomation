using System.Net.Http.Json;
using ApiExcelAutomation.Models;
using Microsoft.Extensions.Logging;

namespace ApiExcelAutomation.Services;

public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _usersApiUrl;
    private readonly ILogger<ApiService> _logger;

    public ApiService(
        HttpClient httpClient,
        string usersApiUrl,
        ILogger<ApiService> logger)
    {
        _httpClient = httpClient;
        _usersApiUrl = usersApiUrl;
        _logger = logger;
    }

    public async Task<List<ApiUser>> GetUsersAsync()
    {
        try
        {
            _logger.LogInformation(
                "Calling users API: {ApiUrl}",
                _usersApiUrl);

            var users = await _httpClient
                .GetFromJsonAsync<List<ApiUser>>(_usersApiUrl);

            var result = users ?? [];

            _logger.LogInformation(
                "Successfully retrieved {Count} users from API.",
                result.Count);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to retrieve users from API.");

            throw;
        }
    }
}