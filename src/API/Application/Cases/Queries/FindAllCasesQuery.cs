using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace Commentor.GivEtPraj.Application.Cases.Queries;

public record FindAllCasesQuery : IRequest<List<CaseSummaryDto>>;

public class FindAllCasesQueryHandler : IRequestHandler<FindAllCasesQuery, List<CaseSummaryDto>>
{
    private readonly IAppDbContext _db;
    private readonly IMapper _mapper;

    public FindAllCasesQueryHandler(IAppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<List<CaseSummaryDto>> Handle(FindAllCasesQuery request, CancellationToken cancellationToken)
    {
        var cases = await _db.Cases
            .Include(c => c.Pictures)
            .Include(c => c.Category)
            .ToListAsync(cancellationToken);
            
        return _mapper.Map<List<Case>, List<CaseSummaryDto>>(cases);
    }
}