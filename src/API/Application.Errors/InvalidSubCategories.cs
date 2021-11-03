using Commentor.GivEtPraj.Application.Errors.Interfaces;

namespace Commentor.GivEtPraj.Application.Errors;

public record InvalidSubCategories(string?[] SubCategories) : IValidationError 
{
    public string? ErrorMessage => $"The subcategories {string.Join(", ", SubCategories)} does not exist";

}