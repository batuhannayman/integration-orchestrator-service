using Integration.Orchestrator.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Integration.Orchestrator.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SyncJobsController : ControllerBase
{
    private readonly SyncJobService _service;

    public SyncJobsController(SyncJobService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpPost]
    public async Task<IActionResult> Create(CreateJobRequest request)
    {
        var id = await _service.CreateAsync(
            request.Name,
            request.SourceSystem,
            request.TargetSystem,
            request.IntervalMinutes);

        return Ok(id);
    }

    [HttpPost("{id}/trigger")]
    public async Task<IActionResult> Trigger(Guid id, CancellationToken cancellationToken)
    {
        var result = await _service.TriggerAsync(id, cancellationToken);

        return result ? Ok("Triggered") : NotFound();
    }
}

public record CreateJobRequest(
    string Name,
    string SourceSystem,
    string TargetSystem,
    int IntervalMinutes);
