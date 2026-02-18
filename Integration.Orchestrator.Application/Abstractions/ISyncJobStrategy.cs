using Integration.Orchestrator.Domain.Entities;

public interface ISyncJobStrategy
{
    JobType JobType { get; }

    Task ExecuteAsync(SyncJob job, CancellationToken cancellationToken);
}
