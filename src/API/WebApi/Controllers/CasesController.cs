using Commentor.GivEtPraj.Application.Cases.Commands;
using Commentor.GivEtPraj.Application.Cases.Queries;

namespace Commentor.GivEtPraj.WebApi.Controllers;

[ApiController]
[Route("v1/cases")]
public class CasesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CasesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCase([FromBody] CreateCasesRequest request,
        CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();
        var command = new CreateCaseCommand(id, request.Cases);
        var result = await _mediator.Send(command, cancellationToken);

        return result.MatchResponse();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> FindCase(Guid id, CancellationToken cancellationToken)
    {
        var query = new FindCaseQuery(id);
        var result = await _mediator.Send(query, cancellationToken);

        return result.MatchResponse();
    }

    // The device ID is handled via middleware, it does not need to be included as a parameter here
    [HttpGet("mine")]
    public async Task<IActionResult> FindCasesByDeviceId(CancellationToken cancellationToken)
    {
        var query = new FindCasesByCurrentDeviceIdQuery();
        var result = await _mediator.Send(query, cancellationToken);

        return result.MatchResponse();
    }
}