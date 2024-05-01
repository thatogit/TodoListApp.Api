using Microsoft.AspNetCore.Mvc;
using TodoListApp.Api.Models;
using TodoListApp.Api.Models.TodoDtos;

namespace TodoListApp.Api.Services.Interface;

public interface ITaskRepository
{
    Task<TaskItemDto> AddTask(TaskItemDto task);
    Task<int> UpdateTask([FromBody] TaskItemDto taskItemUpdate);
    Task<int> DeleteTask(int id);  
    Task<List<TaskItem>> GetTaskItems(); 
}