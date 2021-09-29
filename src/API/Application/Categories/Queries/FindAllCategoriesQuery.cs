using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace Commentor.GivEtPraj.Application.Categories.Queries;

public record FindAllCategoriesQuery : IRequest<List<CaseCategoryDto>>;

public class FindAllCategoriesQueryHandler : IRequestHandler<FindAllCategoriesQuery, List<CaseCategoryDto>>
{
    private readonly IAppDbContext _db;
    private readonly IMapper _mapper;

    public FindAllCategoriesQueryHandler(IAppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<List<CaseCategoryDto>> Handle(FindAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _db.Categories.ToListAsync(cancellationToken);

        return _mapper.Map<List<Category>, List<CaseCategoryDto>>(categories);
    }
}