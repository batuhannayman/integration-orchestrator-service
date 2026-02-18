using Integration.Orchestrator.Domain.Enums;

namespace Integration.Orchestrator.Domain.Entities;

public class SyncJob
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public JobType JobType { get; set; }

    public string Name { get; set; } = default!;

    public string SourceSystem { get; set; } = default!;

    public string TargetSystem { get; set; } = default!;

    public SyncJobStatus Status { get; set; } = SyncJobStatus.Idle;

    public DateTime? LastRunAt { get; set; }

    public int IntervalMinutes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int RetryCount { get; set; } = 0;
    public int MaxRetryCount { get; set; } = 3;

}