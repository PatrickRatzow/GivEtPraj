﻿using Commentor.GivEtPraj.Application.Common.Security;

namespace Commentor.GivEtPraj.Application.Cases.Commands;

[ReCaptcha]
public class CreateQueueKeyCommand : IRequest<OneOf<QueueKeyDto>>
{
    public Guid DeviceId { get; }

    public CreateQueueKeyCommand()
    {
    }
    
    public CreateQueueKeyCommand(Guid deviceId)
    {
        DeviceId = deviceId;
    } 
}

public class CreateQueueKeyCommandHandler : IRequestHandler<CreateQueueKeyCommand, OneOf<QueueKeyDto>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public CreateQueueKeyCommandHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<OneOf<QueueKeyDto>> Handle(CreateQueueKeyCommand request, CancellationToken cancellationToken)
    {
        var queueKey = _context.QueueKeys.Add(new()
        {
            DeviceId = request.DeviceId,
            CaptchaScore = 1,
            CreatedAt = DateTimeOffset.UtcNow,
            ExpiresAt = DateTimeOffset.UtcNow.AddDays(30)
        });

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<QueueKey, QueueKeyDto>(queueKey.Entity);
    }
}