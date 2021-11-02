using Commentor.GivEtPraj.Application.Cases.Commands;
using Commentor.GivEtPraj.WebApi.Filters;

namespace Commentor.GivEtPraj.WebApi.Controllers;

[ApiController]
[Route("v1/queue-keys")]
public class QueueKeysController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    
    public QueueKeysController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    [ReCaptcha]
    public async Task<IActionResult> CreateQueueKey([FromBody] CreateQueueKeyRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateQueueKeyRequest, CreateQueueKeyCommand>(request);
        var result = await _mediator.Send(command, cancellationToken);

        return result.MatchResponse();
    }
}