namespace Commentor.GivEtPraj.Domain.Entities;

public class QueueKey
{
    public Guid Id { get; set; }
    public Guid DeviceId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset ExpiresAt { get; set; }
    public float CaptchaScore { get; set; }
}