using Integration.Orchestrator.Application.Abstractions;
using Integration.Orchestrator.Application.Services;
using Integration.Orchestrator.Infrastructure.Persistence;
using Integration.Orchestrator.Infrastructure.Repositories;
using Integration.Orchestrator.Infrastructure.External;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);




builder.Services.AddDbContext<OrchestratorDbContext>(options =>
    options.UseSqlite("Data Source=C:\\Users\\Batuhann\\Desktop\\Integration.Orchestrator\\orchestrator.db"));

builder.Services.AddScoped<ISyncJobRepository, SyncJobRepository>();
builder.Services.AddScoped<SyncJobService>();

builder.Services.AddScoped<ISyncExecutor, FakeSyncExecutor>();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<OrchestratorDbContext>();
    db.Database.EnsureCreated();
}

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
