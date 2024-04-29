using System.ComponentModel.DataAnnotations;

namespace TodoListApp.Api.Models.TodoDtos;

public class TaskItemDto
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
}