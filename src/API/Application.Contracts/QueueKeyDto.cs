using System;

namespace Commentor.GivEtPraj.Application.Contracts;

public class QueueKeyDto
{
    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset ExpiresAt { get; set; }
}