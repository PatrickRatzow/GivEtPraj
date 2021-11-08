using System.Linq;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Commentor.GivEtPraj.Application.Cases.Queries;

public record FindCaseQuery(int Id) : IRequest<OneOf<CaseDto, CaseNotFound>>;

public class FindCaseQueryHandler : IRequestHandler<FindCaseQuery, OneOf<CaseDto, CaseNotFound>>
{
    private readonly IAppDbContext _db;
    private readonly IMapper _mapper;

    public FindCaseQueryHandler(IAppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<OneOf<CaseDto, CaseNotFound>> Handle(FindCaseQuery request,
        CancellationToken cancellationToken)
    {
        var @case = await _db.Cases
            .Include(c => c.Images)
            .Include(c => c.Category)
            .Where(c => c.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (@case is null) return new CaseNotFound(request.Id);

        return _mapper.Map<BaseCase, CaseDto>(@case);
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