using Commentor.GivEtPraj.Application.Cases.Commands;
using Commentor.GivEtPraj.Application.Cases.Queries;
using Commentor.GivEtPraj.WebApi.Filters;

namespace Commentor.GivEtPraj.WebApi.Controllers;

[ApiController]
[Route("v1/cases")]
public class CasesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CasesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCase([FromBody] CreateCaseRequest request,
        CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateCaseRequest, CreateCaseCommand>(request);
        var ipAddress = HttpContext.Connection.RemoteIpAddress;
        if (ipAddress is null) throw new("IP Address is null??");

        command.IpAddress = ipAddress;

        var result = await _mediator.Send(command, cancellationToken);

        return result.MatchResponse(
            caseSummaryDto => CreatedAtAction(nameof(FindCase),
                new
                {
                    Id = caseSummaryDto.Id
                }, caseSummaryDto)
        );
    }

    [HttpGet]
    public async Task<IActionResult> FindAllCases(CancellationToken cancellationToken)
    {
        var query = new FindAllCasesQuery();
        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> FindCase(int id, CancellationToken cancellationToken)
    {
        var query = new FindCaseQuery(id);
        var result = await _mediator.Send(query, cancellationToken);

        return result.MatchResponse();
    }
}