using Microsoft.EntityFrameworkCore;
using System;
using TodoListApp.Api.Configuration;
using TodoListApp.Api.Data;
using TodoListApp.Api.Models;
using TodoListApp.Api.Services;
using TodoListApp.Api.Services.Interface;

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

// Add services to the container.


builder.Services.AddScoped<ITaskRepository, TaskItemRepository>();


//builder.Services.AddDbContext<TodoDataContext>(options =>
//  options.UseSqlServer(builder.Configuration.GetValue<string>("DbConnectionString")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddScoped<ITaskService, TaskService>();
//builder.Services.AddHttpClient<ITaskService, TaskService>();
builder.Services.Configure<ApiServiceConfig>(builder.Configuration.GetSection(("ApiServiceConfig")));

var connection = String.Empty;
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");
    
    connection = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
}
else
{
    connection = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING");
}

builder.Services.AddDbContext<TodoDataContext>(options =>
    options.UseSqlServer(connection));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/TaskItem", (TodoDataContext context) =>
{
    return context.TaskItem.ToList();
})
.WithName("GetTaskItems")
.WithOpenApi();

app.MapPost("/TaskItem", (TaskItem taskItem, TodoDataContext context) =>
{
    context.Add(taskItem);
    context.SaveChanges();
})
.WithName("CreateTaskItem")
.WithOpenApi();

app.Run();