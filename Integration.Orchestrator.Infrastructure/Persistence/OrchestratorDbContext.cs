using Integration.Orchestrator.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Integration.Orchestrator.Infrastructure.Persistence;

public class OrchestratorDbContext : DbContext
{
    public OrchestratorDbContext(DbContextOptions<OrchestratorDbContext> options)
        : base(options)
    {
    }

    public DbSet<SyncJob> SyncJobs => Set<SyncJob>();
}
