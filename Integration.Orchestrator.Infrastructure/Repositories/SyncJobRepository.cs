using Integration.Orchestrator.Application.Abstractions;
using Integration.Orchestrator.Domain.Entities;
using Integration.Orchestrator.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Integration.Orchestrator.Infrastructure.Repositories;

public class SyncJobRepository : ISyncJobRepository
{
    private readonly OrchestratorDbContext _context;

    public SyncJobRepository(OrchestratorDbContext context)
    {
        _context = context;
    }

    public async Task<List<SyncJob>> GetAllAsync()
        => await _context.SyncJobs.ToListAsync();

    public async Task<List<SyncJob>> GetActiveJobsAsync()
        => await _context.SyncJobs.ToListAsync();

    public async Task<SyncJob?> GetByIdAsync(Guid id)
        => await _context.SyncJobs.FindAsync(id);

    public async Task AddAsync(SyncJob job)
    {
        _context.SyncJobs.Add(job);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(SyncJob job)
    {
        _context.SyncJobs.Update(job);
        await _context.SaveChangesAsync();
    }
}
