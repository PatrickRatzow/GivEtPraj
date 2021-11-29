using Commentor.GivEtPraj.Application.Common.Security;

namespace Commentor.GivEtPraj.Application.Cases.Commands;

[ReCaptcha]
public class CreateQueueKeyCommand : IRequest<OneOf<QueueKeyDto>>
{
}

public class CreateQueueKeyCommandHandler : IRequestHandler<CreateQueueKeyCommand, OneOf<QueueKeyDto>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IDeviceService _deviceService;

    public CreateQueueKeyCommandHandler(IAppDbContext context, IMapper mapper, IDeviceService deviceService)
    {
        _context = context;
        _mapper = mapper;
        _deviceService = deviceService;
    }

    public async Task<OneOf<QueueKeyDto>> Handle(CreateQueueKeyCommand request, CancellationToken cancellationToken)
    {
        var queueKey = _context.QueueKeys.Add(new()
        {
            DeviceId = _deviceService.DeviceIdentifier,
            CaptchaScore = 1,
            CreatedAt = DateTimeOffset.UtcNow,
            ExpiresAt = DateTimeOffset.UtcNow.AddDays(30)
        });

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<QueueKey, QueueKeyDto>(queueKey.Entity);
    }
}