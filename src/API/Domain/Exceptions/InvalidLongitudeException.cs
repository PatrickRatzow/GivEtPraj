namespace Commentor.GivEtPraj.Domain.Exceptions;

public class InvalidLongitudeException : Exception
{
    public InvalidLongitudeException(string? message) : base(message)
    {
    }
}