namespace Commentor.GivEtPraj.Domain.Entities;

public class RecaptchaAuthorization
{
    public Guid DeviceId { get; set; }
    public DateTimeOffset ExpiresAt { get; set; }
    public float CaptchaScore { get; set; }
}