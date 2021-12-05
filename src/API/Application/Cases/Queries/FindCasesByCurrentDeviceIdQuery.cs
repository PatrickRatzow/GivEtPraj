using Microsoft.EntityFrameworkCore;

namespace Commentor.GivEtPraj.Application.Cases.Queries;

public record FindCasesByCurrentDeviceIdQuery : IRequest<OneOf<List<CaseDto>>>;

public class FindCasesByCurrentDeviceIdQueryHandler : IRequestHandler<FindCasesByCurrentDeviceIdQuery, OneOf<List<CaseDto>>>
{
    private readonly IAppDbContext _db;
    private readonly IMapper _mapper;
    private readonly IDeviceService _deviceService;

    public FindCasesByCurrentDeviceIdQueryHandler(IAppDbContext db, IMapper mapper, IDeviceService deviceService)
    {
        _db = db;
        _mapper = mapper;
        _deviceService = deviceService;
    }

    public async Task<OneOf<List<CaseDto>>> Handle(FindCasesByCurrentDeviceIdQuery request,
        CancellationToken cancellationToken)
    {
        var deviceId = _deviceService.DeviceIdentifier;
        var cases = await _db.Cases
            .Include(c => c.Images)
            .Include(c => c.Category)
            .Where(c => c.DeviceId == deviceId)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<BaseCase>, List<CaseDto>>(cases);
    }
}
