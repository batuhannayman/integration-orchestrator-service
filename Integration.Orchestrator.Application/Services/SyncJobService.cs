using Integration.Orchestrator.Application.Abstractions;
using Integration.Orchestrator.Domain.Entities;
using Integration.Orchestrator.Domain.Enums;

namespace Integration.Orchestrator.Application.Services;

public class SyncJobService
{
    private readonly ISyncJobRepository _repository;
    private readonly ISyncExecutor _executor;
    public SyncJobService(ISyncJobRepository repository, ISyncExecutor executor)
    {
        _repository = repository;
        _executor = executor;
    }

    public async Task<List<SyncJob>> GetAllAsync()
        => await _repository.GetAllAsync();

    public async Task<Guid> CreateAsync(string name, string source, string target, int interval)
    {
        var job = new SyncJob
        {
            Name = name,
            SourceSystem = source,
            TargetSystem = target,
            IntervalMinutes = interval,
            Status = SyncJobStatus.Idle
        };

        await _repository.AddAsync(job);

        return job.Id;
    }

    public async Task<bool> TriggerAsync(Guid id, CancellationToken cancellationToken)
    {
        var job = await _repository.GetByIdAsync(id);
        if (job == null)
            return false;

        job.Status = SyncJobStatus.Running;
        await _repository.UpdateAsync(job);

        var attempt = 0;
        var success = false;

        while (attempt < job.MaxRetryCount && !success)
        {
            attempt++;

            success = await _executor.ExecuteAsync(id, cancellationToken);

            if (!success)
            {
                job.RetryCount = attempt;
                await _repository.UpdateAsync(job);

                await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, attempt)), cancellationToken);
            }
        }

        job.Status = success ? SyncJobStatus.Success : SyncJobStatus.Failed;
        job.LastRunAt = DateTime.UtcNow;

        if (success)
            job.RetryCount = 0;

        await _repository.UpdateAsync(job);

        return success;
    }


}