using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace Commentor.GivEtPraj.Application.Cases.Queries;

public record FindCaseQuery(int Id) : IRequest<OneOf<CaseSummaryDto, CaseNotFound>>;

public class FindCaseQueryHandler : IRequestHandler<FindCaseQuery, OneOf<CaseSummaryDto, CaseNotFound>>
{
    private readonly IAppDbContext _db;
    private readonly IMapper _mapper;

    public FindCaseQueryHandler(IAppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<OneOf<CaseSummaryDto, CaseNotFound>> Handle(FindCaseQuery request, 
        CancellationToken cancellationToken)
    {
        var @case = await _db.Cases
            .Include(c => c.Pictures)
            .Include(c => c.Category)
            .Where(c => c.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
            
        if (@case is null) return new CaseNotFound(request.Id);

        return _mapper.Map<Case, CaseSummaryDto>(@case);
    }
}

public class FindCaseQueryValidator : AbstractValidator<FindCaseQuery>
{
    public FindCaseQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}