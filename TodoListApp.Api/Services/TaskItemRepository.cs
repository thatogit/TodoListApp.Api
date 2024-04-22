using AutoMapper;
using TodoListApp.Api.Data;
using TodoListApp.Api.Models;
using TodoListApp.Api.Models.TodoDtos;
using TodoListApp.Api.Services.Interface;

namespace TodoListApp.Api.Services;

public class TaskItemRepository :ITaskRepository
{
    private readonly TodoDataContext _db;
    private readonly IMapper _mapper;

    public TaskItemRepository(TodoDataContext todoDataContext, IMapper mapper)
    {
        _db = todoDataContext;
        _mapper = mapper;
    }
    
    public async Task<TaskItemDto> AddTask(TaskItemDto taskItemDto)
    {
        TaskItem newTask = null;

        newTask = new TaskItem
        {
             Name = taskItemDto.Name

        };
        
        _db.AddAsync(newTask);
        
        return taskItemDto;
    }
}