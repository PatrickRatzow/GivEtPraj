using Commentor.GivEtPraj.Application.Common.Security;

namespace Commentor.GivEtPraj.Application.Cases.Commands;

[ReCaptcha]
public class PreAuthorizeDeviceCommand : IRequest<OneOf<Unit, IsAlreadyPreAuthorized>>
{
}

public class PreAuthorizeDeviceCommandHandler 
    : IRequestHandler<PreAuthorizeDeviceCommand, OneOf<Unit, IsAlreadyPreAuthorized>>
{
    private readonly IAppDbContext _context;
    private readonly IDeviceService _deviceService;

    public PreAuthorizeDeviceCommandHandler(IAppDbContext context, IDeviceService deviceService)
    {
        _context = context;
        _deviceService = deviceService;
    }

    public async Task<OneOf<Unit, IsAlreadyPreAuthorized>> Handle(PreAuthorizeDeviceCommand request, 
        CancellationToken cancellationToken)
    {
        var deviceId = _deviceService.DeviceIdentifier;
        await using var trx = await _context.Database.BeginTransactionAsync(cancellationToken);

        var existingAuthorization = await _context.PreAuthorizations.FindAsync(new object[]
        {
            deviceId
        }, cancellationToken);
        if (existingAuthorization is not null) return new IsAlreadyPreAuthorized(deviceId);
        
        _context.PreAuthorizations.Add(new()
        {
            DeviceId = deviceId,
            ExpiresAt = DateTimeOffset.UtcNow.AddDays(30)
        });

        await _context.SaveChangesAsync(cancellationToken);
        await trx.CommitAsync(cancellationToken);

        return Unit.Value;
    }
}