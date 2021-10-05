namespace Commentor.GivEtPraj.Domain.Exceptions
{
    public class InvalidLatitudeException : Exception
    {
        public InvalidLatitudeException(string? message) : base(message)
        {
        }
    }
}