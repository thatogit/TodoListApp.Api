using TodoListApp.Api.Configuration;
using TodoListApp.Api.Data;
using TodoListApp.Api.Services;
using TodoListApp.Api.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<ITaskRepository, TaskItemRepository>();

string todoDbConnectionString = builder.Configuration.GetValue<string>("todoDbString");

builder.Services.AddDbContext<TodoDataContext>(options =>
    options.UseSqlServer(todoDbConnectionString));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddHttpClient<ITaskService, TaskService>();
builder.Services.Configure<ApiServiceConfig>(builder.Configuration.GetSection(("ApiServiceConfig")));

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

app.Run();