using Integration.Orchestrator.Worker;
using Integration.Orchestrator.Application.Abstractions;
using Integration.Orchestrator.Application.Services;
using Integration.Orchestrator.Infrastructure.Persistence;
using Integration.Orchestrator.Infrastructure.Repositories;
using Integration.Orchestrator.Infrastructure.External;
using Microsoft.EntityFrameworkCore;
using Serilog;


Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithThreadId()
    .Enrich.WithProcessId()
    .WriteTo.Console()
    .WriteTo.File("logs/worker-.log",
        rollingInterval: RollingInterval.Day,
        outputTemplate:
        "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
    .CreateLogger();


try
{
    var builder = Host.CreateApplicationBuilder(args);

    builder.Services.AddDbContext<OrchestratorDbContext>(options => options.UseSqlite("Data Source=C:\\Users\\Batuhann\\Desktop\\Integration.Orchestrator\\orchestrator.db"));

    builder.Services.AddScoped<ISyncJobRepository, SyncJobRepository>();
    builder.Services.AddScoped<SyncJobService>();
    builder.Services.AddScoped<ISyncJobStrategy, CrmToErpCustomerSyncStrategy>();
    builder.Services.AddScoped<ISyncJobStrategy, StockSyncStrategy>();
    builder.Services.AddScoped<ISyncExecutor, FakeSyncExecutor>();

    builder.Services.AddHostedService<Worker>();

    var host = builder.Build();

    using (var scope = host.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<OrchestratorDbContext>();
        db.Database.EnsureCreated();
    }


    host.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Worker crashed");
}
finally
{
    Log.CloseAndFlush();
}

