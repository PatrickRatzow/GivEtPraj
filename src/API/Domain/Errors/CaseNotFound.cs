using Commentor.GivEtPraj.Domain.Errors.Interfaces;

namespace Commentor.GivEtPraj.Domain.Errors
{
    public record CaseNotFound(int Id) : INotFoundError
    {
        public string ErrorMessage => $"Unable to find any error with the ID {Id}";
    }
}