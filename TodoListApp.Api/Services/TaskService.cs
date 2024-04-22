using System.Net;
using Microsoft.Extensions.Options;
using TodoListApp.Api.Configuration;
using TodoListApp.Api.Models;
using TodoListApp.Api.Services.Interface;

namespace TodoListApp.Api.Services;

public class TaskService : ITaskService
{
    private readonly HttpClient _httpClient;
    private readonly ApiServiceConfig _config;
    public TaskService(HttpClient httpClient,
        IOptions<ApiServiceConfig> config)
    {
        _httpClient = httpClient;
        _config = config.Value;
    }
    public async Task<List<TaskItem>?> GetAllTasks()
    {
        var response = await _httpClient.GetAsync(_config.Url);
        switch (response.StatusCode)
        {
            case HttpStatusCode.NotFound:
                return new List<TaskItem>();
            case HttpStatusCode.Unauthorized:
                return null;
            default:
            {
                var taskItems = await response.Content.ReadFromJsonAsync<List<TaskItem>>();

                return taskItems;
            }
        }
    }
}