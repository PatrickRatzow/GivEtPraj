using Commentor.GivEtPraj.Application.Categories.Queries;
using Commentor.GivEtPraj.Application.Contracts;

namespace Commentor.GivEtPraj.WebApi.Controllers;

[ApiController]
[Route("v1/categories")]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> FindAllCategories(Language language, CancellationToken cancellationToken)
    {
        var query = new FindAllCategoriesQuery(language);
        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result);
    }
}