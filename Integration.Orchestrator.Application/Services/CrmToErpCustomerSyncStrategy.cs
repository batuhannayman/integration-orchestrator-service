using Integration.Orchestrator.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public class CrmToErpCustomerSyncStrategy : ISyncJobStrategy
{
    private readonly ILogger<CrmToErpCustomerSyncStrategy> _logger;

    public CrmToErpCustomerSyncStrategy(
        ILogger<CrmToErpCustomerSyncStrategy> logger)
    {
        _logger = logger;
    }

    public JobType JobType => JobType.CrmToErpCustomerSync;

    public async Task ExecuteAsync(SyncJob job, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CRM → ERP sync started for {JobId}", job.Id);

        await Task.Delay(2000, cancellationToken); // fake işlem

        _logger.LogInformation("CRM → ERP sync completed for {JobId}", job.Id);
    }
}

