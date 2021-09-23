using System.Threading;
using System.Threading.Tasks;
using Commentor.GivEtPraj.Application.Cases.Commands;
using Commentor.GivEtPraj.Application.Cases.Queries;
using Commentor.GivEtPraj.WebApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Commentor.GivEtPraj.WebApi.Controllers
{
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
        public async Task<ActionResult> CreateCase(CreateCaseCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
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
}