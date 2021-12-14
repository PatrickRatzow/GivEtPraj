using FluentValidation;

namespace Commentor.GivEtPraj.Domain.Entities;

public class ReCaptchaAuthorization : BaseEntity
{
    public Guid DeviceId { get; private set; }
    public DateTimeOffset ExpiresAt { get; private set; }

    private ReCaptchaAuthorization()
    {

    }
    public ReCaptchaAuthorization(Guid deviceId, DateTimeOffset expiresAt)
    {
        DeviceId = deviceId;
        ExpiresAt = expiresAt;
        Validate();
    }
}

public class ReCaptchaAuthorizationValidator : AbstractValidator<ReCaptchaAuthorization>
{
    public ReCaptchaAuthorizationValidator()
    {
        RuleFor(x => x.DeviceId).NotEmpty();
        RuleFor(x => x.ExpiresAt).NotEmpty();
    }
}