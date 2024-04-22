using TodoListApp.Api.Models;

namespace TodoListApp.Api.Services.Interface;

public interface ITaskService
{
    Task<List<TaskItem>?> GetAllTasks();
}