
using CATodo.BLLContracts;
using CATodo.BLLImplementation;
using CATodo.DAL;
using CATodo.WebApi.Hubs;
using CATodo.WebApi.Middlewares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<ICATodoService, CATodoServiceImplementation>(); 
builder.Services.AddDbContext<CATodoContext>(
    optionsBuilder => optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("CATodosDb"))
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(
    config => config.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader())
);

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSampleTodosMiddleware();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapHub<TodoHub>("todo-hub");

app.UseCors();
app.MapControllers();

app.Run();
