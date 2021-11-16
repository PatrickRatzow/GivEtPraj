using Commentor.GivEtPraj.Application.Errors.Interfaces;

namespace Commentor.GivEtPraj.Application.Errors;

public record InvalidSubCategories : IValidationError
{
    public string ErrorMessage => $"At least one sub category is invalid";
}