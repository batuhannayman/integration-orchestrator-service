using Integration.Orchestrator.Domain.Entities;
using Microsoft.Extensions.Logging;

public class StockSyncStrategy : ISyncJobStrategy
{
    private readonly ILogger<StockSyncStrategy> _logger;

    public StockSyncStrategy(
        ILogger<StockSyncStrategy> logger)
    {
        _logger = logger;
    }

    public JobType JobType => JobType.StockSync;

    public async Task ExecuteAsync(SyncJob job, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stock sync started for {JobId}", job.Id);

        await Task.Delay(1500, cancellationToken);

        _logger.LogInformation("Stock sync completed for {JobId}", job.Id);
    }
}
