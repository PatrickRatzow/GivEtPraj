﻿using Microsoft.EntityFrameworkCore;

namespace Commentor.GivEtPraj.Application.Cases.Queries;

public record FindAllCasesQuery : IRequest<List<CaseDto>>;

public class FindAllCasesQueryHandler : IRequestHandler<FindAllCasesQuery, List<CaseDto>>
{
    private readonly IAppDbContext _db;
    private readonly IMapper _mapper;

    public FindAllCasesQueryHandler(IAppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<List<CaseDto>> Handle(FindAllCasesQuery request, CancellationToken cancellationToken)
    {
        var cases = await _db.Cases
            .Include(c => c.Images)
            .Include(c => c.Category)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<BaseCase>, List<CaseDto>>(cases);
    }
}