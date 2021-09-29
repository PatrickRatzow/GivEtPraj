using Commentor.GivEtPraj.Application.Errors.Interfaces;

namespace Commentor.GivEtPraj.Application.Errors;

public record InvalidCategory(string Category) : IValidationError
{
    public string ErrorMessage => $"The category with the name {Category} does not exist";
}