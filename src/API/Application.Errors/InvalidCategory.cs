using Commentor.GivEtPraj.Application.Errors.Interfaces;

namespace Commentor.GivEtPraj.Application.Errors;

public record InvalidCategory() : IValidationError
{
    public string ErrorMessage => $"Atleast one category id is invalid";
}