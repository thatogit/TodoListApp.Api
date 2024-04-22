using TodoListApp.Api.Models;

namespace TodoListApp.Tests.Fixtures;

public class TaskItemFixture
{
    public static List<TaskItem> GetTaskItem() => new()
    {
        new TaskItem()
        {
            Id = 1,
            Name = "Learn driving"
        },
        new TaskItem()
        {
            Id = 2,
            Name = "Learn azure"
        }

    };
}