using FluentAssertions;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using TodoListApp.Api.Configuration;
using TodoListApp.Api.Models;
using TodoListApp.Api.Services;
using TodoListApp.Tests.Fixtures;
using TodoListApp.Tests.Helpers;

namespace TodoListApp.Tests.Systems.Services;

public class TestTaskItemService
{
    [Fact]
    public async Task GetAllTaskItems_OnInvoked_HttpGet()
    {
        //Arrange
        var Url = "https://google.com";
        var response = TaskItemFixture.GetTaskItem();
        var mockHandler = MockHttpHandler<TaskItem>.SetupGetRequest(response);
        var httpClient = new HttpClient(mockHandler.Object);

        var config = Options.Create(new ApiServiceConfig()
        {
            Url = Url
        });

        var taskService = new TaskService(httpClient, config);
        //Act

        await taskService.GetAllTasks();

        //assert
        mockHandler.Protected().Verify(
            "SendAsync", Times.Once(),
            ItExpr.Is<HttpRequestMessage>(r => 
                r.Method == HttpMethod.Get && r.RequestUri.ToString() == Url),
            ItExpr.IsAny<CancellationToken>());
    }
    [Fact]
    public async Task GetAllTaskItems_OnInvoked_HttpListOfFans()
    {
        //Arrange
        var Url = "https://google.com";
        var response = TaskItemFixture.GetTaskItem();
        var mockHandler = MockHttpHandler<TaskItem>.SetupGetRequest(response);
        var httpClient = new HttpClient(mockHandler.Object);

        var config = Options.Create(new ApiServiceConfig()
        {
            Url = Url
        });

        var taskService = new TaskService(httpClient, config);
        //Act

        var result = await taskService.GetAllTasks();

        //assert
        result.Should().BeOfType<List<TaskItem>>();
    }
    [Fact]
    public async Task GetAllTaskItems_OnInvoked_ReturnEmptyList()
    {
        //Arrange
        var Url = "https://google.com";
        var mockHandler = MockHttpHandler<TaskItem>.SetupReturnNotFound();
        var httpClient = new HttpClient(mockHandler.Object);

        var config = Options.Create(new ApiServiceConfig()
        {
            Url = Url
        });

        var taskService = new TaskService(httpClient, config);
        //Act

        var result = await taskService.GetAllTasks();

        //assert
        result.Count.Should().Be(0);
    }

}