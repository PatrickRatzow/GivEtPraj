namespace Commentor.GivEtPraj.Application.Cases.Commands;

public record CreateQueueKeyCommand(Guid DeviceId, float CaptchaScore) : IRequest<QueueKeyDto>;

public class CreateQueueKeyCommandHandler : IRequestHandler<CreateQueueKeyCommand, QueueKeyDto>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public CreateQueueKeyCommandHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<QueueKeyDto> Handle(CreateQueueKeyCommand request, CancellationToken cancellationToken)
    {
        var queueKey = _context.QueueKeys.Add(new()
        {
            DeviceId = request.DeviceId,
            CaptchaScore = request.CaptchaScore,
            ExpiresAt = DateTimeOffset.UtcNow.AddDays(30)
        });

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<QueueKey, QueueKeyDto>(queueKey.Entity);
    }
}