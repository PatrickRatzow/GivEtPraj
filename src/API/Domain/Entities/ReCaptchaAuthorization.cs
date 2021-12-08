namespace Commentor.GivEtPraj.Domain.Entities;

public class ReCaptchaAuthorization
{
    public Guid DeviceId { get; set; }
    public DateTimeOffset ExpiresAt { get; set; }
}