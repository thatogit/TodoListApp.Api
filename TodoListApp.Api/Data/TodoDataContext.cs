using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TodoListApp.Api.Models;

namespace TodoListApp.Api.Data;

public class TodoDataContext : DbContext
{
    public TodoDataContext(DbContextOptions<TodoDataContext> options) : base(options)
    {
        
    }
    public DbSet<TaskItem> TaskItem { get; set; }
    
}