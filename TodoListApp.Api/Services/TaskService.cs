using TodoListApp.Api.Services.Interface;

namespace TodoListApp.Api.Services;

public class TaskService : ITaskService
{
    public Task<List<Task>> GetAllTasks()
    {
        throw new NotImplementedException();
    }
}