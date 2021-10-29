using Commentor.GivEtPraj.WebApi.Filters;

namespace Commentor.GivEtPraj.WebApi.Controllers;

[ApiController]
[Route("v1/captcha")]
public class CaptchaController : ControllerBase
{
    private readonly IMediator _mediator;

    public CaptchaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ReCaptcha]
    public async Task<IActionResult> PostCaptcha(CancellationToken cancellationToken)
    {
        return Ok();
    }
}