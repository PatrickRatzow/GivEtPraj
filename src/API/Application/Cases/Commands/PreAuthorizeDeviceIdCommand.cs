using Commentor.GivEtPraj.Application.Common.Security;

namespace Commentor.GivEtPraj.Application.Cases.Commands;

[ReCaptcha]
public class PreAuthorizeDeviceCommand : IRequest<OneOf<Unit>>
{
}

public class PreAuthorizeDeviceCommandHandler : IRequestHandler<PreAuthorizeDeviceCommand, OneOf<Unit>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IDeviceService _deviceService;

    public PreAuthorizeDeviceCommandHandler(IAppDbContext context, IMapper mapper, IDeviceService deviceService)
    {
        _context = context;
        _mapper = mapper;
        _deviceService = deviceService;
    }

    public async Task<OneOf<Unit>> Handle(PreAuthorizeDeviceCommand request, CancellationToken cancellationToken)
    {
        _context.QueueKeys.Add(new()
        {
            DeviceId = _deviceService.DeviceIdentifier,
            CaptchaScore = 1,
            ExpiresAt = DateTimeOffset.UtcNow.AddDays(30)
        });

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}