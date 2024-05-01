using Microsoft.AspNetCore.Mvc;
using TodoListApp.Api.Models.TodoDtos;
using TodoListApp.Api.Services.Interface;

namespace TodoListApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoTaskController:Controller
{
   // private readonly ITaskService _taskService;
    private readonly ITaskRepository _taskRepository;
    public TodoTaskController(//ITaskService taskService
        ITaskRepository taskRepository)
    {
        //_taskService = taskService;
        _taskRepository = taskRepository;
    }
    [HttpPost("AddTask")]
    public async Task<IActionResult> AddTask([FromBody] TaskItemDto task)
    {
        if (task == null)
        {
            return BadRequest("Task item not provided.");

        }
        else
        {
            var response = await _taskRepository.AddTask(task);

            return Ok(response);

        }

    }

    [HttpPut("UpdateTask")]
    public async Task<IActionResult> UpdateTask([FromBody] TaskItemDto taskItemUpdate)
    {

        var task = await _taskRepository.UpdateTask(taskItemUpdate);

        if (task == -1)
        {
            return BadRequest("Task not found");

        }

        return Ok(task);
    }

    [HttpGet("GetTasks")]
    public async Task<IActionResult> GetTasks()
    {
        var tasks = await _taskRepository.GetTaskItems();
        if (tasks.Count == 0)
        {
            return BadRequest("No items found from database");

        }

        return Ok(tasks);
    }

    [HttpDelete("DeleteTask")]
    public async Task<IActionResult> DeleteTask(int Id)
    {
        var task = await _taskRepository.DeleteTask(Id);

        if (task == -1)
            return BadRequest("Delete was not successful");

        return Ok(task);
    }

    //[HttpGet(Name = "GetTask")]
    //public async Task<IActionResult> Get()
    //{
    //    var tasks = await _taskService.GetAllTasks();

    //    if (tasks.Any())
    //        return Ok(tasks);

    //    return NotFound();
    //}
}