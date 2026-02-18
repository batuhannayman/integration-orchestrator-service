using Integration.Orchestrator.Application.Abstractions;
using Microsoft.Extensions.Logging;

namespace Integration.Orchestrator.Infrastructure.External;

public class FakeSyncExecutor : ISyncExecutor
{
    private readonly ILogger<FakeSyncExecutor> _logger;
    private readonly Random _random = new();

    public FakeSyncExecutor(ILogger<FakeSyncExecutor> logger)
    {
        _logger = logger;
    }

    public async Task<bool> ExecuteAsync(Guid jobId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Job {JobId} execution started.", jobId);

        await Task.Delay(2000, cancellationToken); // Simulated external API delay

        var success = _random.Next(0, 100) > 30; // %70 success

        if (success)
            _logger.LogInformation("Job {JobId} executed successfully.", jobId);
        else
            _logger.LogWarning("Job {JobId} failed during execution.", jobId);

        return success;
    }
}
