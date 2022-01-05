using Commentor.GivEtPraj.Application.Cases.Commands;

namespace Commentor.GivEtPraj.WebApi.Controllers;

[ApiController]
[Route("v1/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> PreAuthorizeDeviceId([FromBody] PreAuthorizeDeviceCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        return result.MatchResponse();
    }
}