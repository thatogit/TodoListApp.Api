using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TodoListApp.Api.Controllers;
using TodoListApp.Api.Models;
using TodoListApp.Api.Services.Interface;
using TodoListApp.Tests.Fixtures;

namespace TodoListApp.Tests.Systems.Controllers;

public class TodoTaskControllerTest
{
    [Fact]
    public async Task Get_OnSuccess_ReturnStatusCode200()
    {
        //Arrange
        var mockTaskService = new Mock<ITaskService>();
        mockTaskService.Setup(service => service.GetAllTasks())
            .ReturnsAsync(TaskItemFixture.GetTaskItem());
        var todoController = new TodoTaskController(mockTaskService.Object);
        //Act
        var result = (OkObjectResult)await todoController.Get();

        //Assert
        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task Get_OnSuccess_InvokeService()
    {
        //Arrange
        var mockTaskService = new Mock<ITaskService>();
        mockTaskService.Setup(service => service.GetAllTasks())
            .ReturnsAsync(TaskItemFixture.GetTaskItem());
        var todoController = new TodoTaskController(mockTaskService.Object);
        //Act
        var result = (OkObjectResult)await todoController.Get();

        //Assert
        mockTaskService.Verify(service=>service.GetAllTasks(),Times.Once);
    }

    [Fact]
    public async Task Get_OnSuccess_ReturnListOfTask()
    {
        //Arrange
        var mockTaskService = new Mock<ITaskService>();
        mockTaskService.Setup(service => service.GetAllTasks())
            .ReturnsAsync(TaskItemFixture.GetTaskItem());
        
        var todoController = new TodoTaskController(mockTaskService.Object);
        //Act
        var result = (OkObjectResult) await todoController.Get();
        
        //Assert
        result.Should().BeOfType<OkObjectResult>();

        result.Value.Should().BeOfType<List<TaskItem>>();
    }
    
    [Fact]
    public async Task Get_OnNoTask_ReturnNoFound()
    {
        //Arrange
        var mockTaskService = new Mock<ITaskService>();
        mockTaskService.Setup(service => service.GetAllTasks())
            .ReturnsAsync(new List<TaskItem>());
        
        var todoController = new TodoTaskController(mockTaskService.Object);
        //Act
        var result = (NotFoundResult) await todoController.Get();
        
        //Assert
        result.Should().BeOfType<NotFoundResult>();
    }
}