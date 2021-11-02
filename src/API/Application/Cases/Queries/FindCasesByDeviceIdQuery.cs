using Microsoft.EntityFrameworkCore;

namespace Commentor.GivEtPraj.Application.Cases.Queries;

public record FindCasesByDeviceIdQuery(Guid DeviceId) : IRequest<OneOf<List<CaseDto>>>;

public class FindAllCasesByDeviceIdQueryHandler : IRequestHandler<FindCasesByDeviceIdQuery, OneOf<List<CaseDto>>>
{
    private readonly IAppDbContext _db;
    private readonly IMapper _mapper;

    public FindAllCasesByDeviceIdQueryHandler(IAppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<OneOf<List<CaseDto>>> Handle(FindCasesByDeviceIdQuery request,
        CancellationToken cancellationToken)
    {
        var cases = await _db.Cases
            .Include(c => c.Pictures)
            .Include(c => c.Category)
            .Where(c => c.DeviceId == request.DeviceId)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<BaseCase>, List<CaseDto>>(cases);
    }
}