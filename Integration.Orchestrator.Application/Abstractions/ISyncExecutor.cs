namespace Integration.Orchestrator.Application.Abstractions;

public interface ISyncExecutor
{
    Task<bool> ExecuteAsync(Guid jobId, CancellationToken cancellationToken);
}
