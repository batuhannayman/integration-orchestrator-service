using Integration.Orchestrator.Domain.Entities;

namespace Integration.Orchestrator.Application.Abstractions;

public interface ISyncJobRepository
{
    Task<List<SyncJob>> GetAllAsync();
    Task<List<SyncJob>> GetActiveJobsAsync();
    Task<SyncJob?> GetByIdAsync(Guid id);
    Task AddAsync(SyncJob job);
    Task UpdateAsync(SyncJob job);
}
