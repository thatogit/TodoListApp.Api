using System.ComponentModel.DataAnnotations;

namespace TodoListApp.Api.Models;

public class TaskItem
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}