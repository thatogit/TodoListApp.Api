using Microsoft.AspNetCore.Mvc;
using TodoListApp.Api.Services.Interface;

namespace TodoListApp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoTaskController:ControllerBase
{
    private readonly ITaskService _taskService;
    public TodoTaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }
    [HttpGet(Name = "GetTask")]
    public async Task<IActionResult> Get()
    {
        var tasks = await _taskService.GetAllTasks();

        if (tasks.Any())
            return Ok(tasks);

        return NotFound();
    }
}