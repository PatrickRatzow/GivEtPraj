using Commentor.GivEtPraj.Application.Errors.Interfaces;

namespace Commentor.GivEtPraj.Application.Errors;

public record IsAlreadyPreAuthorized(Guid Id) : IAlreadyExistsError
{
    public string ErrorMessage => $"The Id {Id} is already authorized";
}