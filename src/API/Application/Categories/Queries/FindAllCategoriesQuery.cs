using Microsoft.EntityFrameworkCore;

namespace Commentor.GivEtPraj.Application.Categories.Queries;

public record FindAllCategoriesQuery : IRequest<List<CategoryDto>>;

public class FindAllCategoriesQueryHandler : IRequestHandler<FindAllCategoriesQuery, List<CategoryDto>>
{
    private readonly IAppDbContext _db;
    private readonly IMapper _mapper;

    public FindAllCategoriesQueryHandler(IAppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<List<CategoryDto>> Handle(FindAllCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        var categories = await _db.Categories
            .Include(category => category.SubCategories)
            .ToListAsync(cancellationToken);


        return _mapper.Map<List<Category>, List<CategoryDto>>(categories);
    }
}