using Commentor.GivEtPraj.Application.Errors.Interfaces;

namespace Commentor.GivEtPraj.Application.Errors;

public record InvalidSubCategories(int[] SubCategories) : IValidationError
{
    public string ErrorMessage => $"The subcategories with the Ids '{string.Join(", ", SubCategories)}' does not exist";
}