using Commentor.GivEtPraj.Application.Cases.Commands;

namespace Commentor.GivEtPraj.WebApi.Controllers;

[ApiController]
[Route("v1/queue-keys")]
public class QueueKeysController : ControllerBase
{
    private readonly IMediator _mediator;

    public QueueKeysController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateQueueKey([FromBody] CreateQueueKeyCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        return result.MatchResponse();
    }
}