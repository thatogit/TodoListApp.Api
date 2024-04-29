using Microsoft.AspNetCore.Mvc;
using TodoListApp.Api.Models;
using TodoListApp.Api.Models.TodoDtos;

namespace TodoListApp.Api.Services.Interface;

public interface ITaskRepository
{
    Task<TaskItem> AddTask(TaskItemDto taskItemDto);
    Task<TaskItem> UpdateTask(int id,[FromBody] TaskItemDto taskItemUpdate);
    Task DeleteTask(int id);  
    Task<List<TaskItem>> GetTaskItems(); 
}