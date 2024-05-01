using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

    public async Task<List<TaskItem>> GetTaskItems()
    {
        IEnumerable<TaskItem> taskItems = await _db.TaskItem.Where(p => p.Id > 0).ToListAsync();

        return taskItems.ToList();
    }
    public async Task<TaskItemDto> AddTask(TaskItemDto task)
    {
        TaskItem newTaskItem = new TaskItem
        {
             Name = task.Name

        };
        
        await _db.AddAsync(newTaskItem);
        
        await _db.SaveChangesAsync();

        return task;
    }

    public async Task<int> UpdateTask([FromBody] TaskItemDto taskItemUpdate)
    {
        var taskExist = await _db.TaskItem.FirstOrDefaultAsync(c => c.Id == taskItemUpdate.Id);

            if (taskItemUpdate == null || taskExist == null)
                return -1;

        if (taskExist != null)
        {
            TaskItem taskItem = new TaskItem
            {
                Name = taskItemUpdate.Name
            };

            _db.Update(taskItem);
            await _db.SaveChangesAsync();

        }

            return 1;
  
        
    }

    public async Task<int> DeleteTask(int id)
    {
        TaskItem taskToDelete = await _db.TaskItem.FirstOrDefaultAsync(p => p.Id == id);

        if (taskToDelete == null)
        {
            return -1;

        }

        _db.TaskItem.Remove(taskToDelete);
        _db.SaveChanges();

        return 1;


    }

}