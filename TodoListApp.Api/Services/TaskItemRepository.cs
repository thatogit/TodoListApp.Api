using System.Net;
using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoListApp.Api.Data;
using TodoListApp.Api.Models;
using TodoListApp.Api.Models.TodoDtos;
using TodoListApp.Api.Services.Interface;

namespace TodoListApp.Api.Services;

public class TaskItemRepository : ITaskRepository
{
    private readonly TodoDataContext _db;

    public TaskItemRepository(TodoDataContext todoDataContext)
    {
        _db = todoDataContext;
    }
    
    public async Task<TaskItem> AddTask(TaskItemDto taskItem)
    {

        TaskItem newTaskItem = new TaskItem
        {
             Name = taskItem.Name

        };
        
       await _db.AddAsync(newTaskItem);
        
        await _db.SaveChangesAsync();

        return newTaskItem;
    }

    public async Task DeleteTask(int id)
    {

        TaskItem taskToDelete = await _db.TaskItem.FirstOrDefaultAsync(p => p.Id == id);

        if (taskToDelete != null)
        {
            _db.TaskItem.Remove(taskToDelete);
            _db.SaveChanges(); 
            
        }

    }

    public async  Task<List<TaskItem>> GetTaskItems()
    {
        IEnumerable<TaskItem> taskItems = await _db.TaskItem.Where(p=>p.Id>0).ToListAsync();

        return  taskItems.ToList();
    }

    public async Task<TaskItem> UpdateTask(int id, [FromBody] TaskItemDto taskItemUpdate)
    {
        var taskExist = await _db.TaskItem.FirstOrDefaultAsync(c => c.Id == id);

        TaskItem taskItem = null;

        if ( taskExist.Id != null )
        {
            taskItem = new TaskItem
            {
                Name = taskItemUpdate.Name
            };

            _db.Update(taskItem);

            await _db.SaveChangesAsync();
            
        }

        return taskItem;
    }
}