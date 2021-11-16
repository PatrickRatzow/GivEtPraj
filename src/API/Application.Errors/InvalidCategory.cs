using Commentor.GivEtPraj.Application.Errors.Interfaces;

namespace Commentor.GivEtPraj.Application.Errors;

public record InvalidCategory : IValidationError
{
    public string ErrorMessage => $"At least one category id is invalid";
}