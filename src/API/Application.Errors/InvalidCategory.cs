using Commentor.GivEtPraj.Application.Errors.Interfaces;

namespace Commentor.GivEtPraj.Application.Errors;

public record InvalidCategory(int CategoryId) : IValidationError
{
    public string ErrorMessage => $"The category with the name {CategoryId} does not exist";
}