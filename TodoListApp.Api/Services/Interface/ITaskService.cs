namespace TodoListApp.Api.Services.Interface;

public interface ITaskService
{
    Task<List<Task>> GetAllTasks();
}