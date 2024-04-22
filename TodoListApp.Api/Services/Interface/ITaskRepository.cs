using TodoListApp.Api.Models;
using TodoListApp.Api.Models.TodoDtos;

namespace TodoListApp.Api.Services.Interface;

public interface ITaskRepository
{
    Task<TaskItemDto> AddTask(TaskItemDto taskItemDto);
}