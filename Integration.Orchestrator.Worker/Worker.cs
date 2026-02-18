using Integration.Orchestrator.Application.Services;
using Integration.Orchestrator.Domain.Enums;

namespace Integration.Orchestrator.Worker;

public class Worker : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<Worker> _logger;
    private readonly IEnumerable<ISyncJobStrategy> _strategies;

    public Worker(
    ILogger<Worker> logger,
    IServiceScopeFactory scopeFactory,
    IEnumerable<ISyncJobStrategy> strategies)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
        _strategies = strategies;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Orchestrator Worker started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _scopeFactory.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<SyncJobService>();

            var jobs = await service.GetAllAsync();

            foreach (var job in jobs)
            {
                if (job.LastRunAt == null ||
                    job.LastRunAt.Value.AddMinutes(job.IntervalMinutes) <= DateTime.UtcNow)
                {
                    _logger.LogInformation(
                        "Sync job started for {JobId} - {JobName} from {SourceSystem} to {TargetSystem}",
                        job.Id,
                        job.Name,
                        job.SourceSystem,
                        job.TargetSystem
                    );

                    _logger.LogInformation("Executing scheduled job {JobId}", job.Id);

                    var strategy = _strategies
                            .FirstOrDefault(s => s.JobType == job.JobType);

                    if (strategy == null)
                    {
                        _logger.LogWarning("No strategy found for {JobType}", job.JobType);
                        continue;
                    }

                    try
                    {
                        await strategy.ExecuteAsync(job, stoppingToken);
                        await service.TriggerAsync(job.Id, stoppingToken);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex,
                            "Job execution failed for {JobId}", job.Id);
                    }

                }
            }

            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }
}
