using System.Linq;
using System.Reflection;
using Commentor.GivEtPraj.Domain.ValueObjects;
using FluentValidation;

namespace Commentor.GivEtPraj.Domain.Entities;

public class BaseCaseValidator : AbstractValidator<BaseCase>
{
    public BaseCaseValidator()
    {
        RuleFor(x => x.Category).NotNull();
        RuleFor(x => x.Id).NotEmpty();
    }
}

public abstract class BaseCase : BaseEntity
{
    public int Id { get; set; }
    public Guid DeviceId { get; set; }
    public Category Category { get; set; } = null!;
    public int CategoryId { get; set; }
    public List<CaseImage> Images { get; set; } = new();
    public GeographicLocation GeographicLocation { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public List<CaseUpdate> CaseUpdates { get; set; } = null!;

    public void AddImage(CaseImage image)
    {
        Images.Add(image);
    }
}

public abstract class BaseEntity
{
    private static List<IValidator>? _validators;
    private static List<IValidator> Validators 
        => _validators ??= Assembly.GetExecutingAssembly()
            .GetExportedTypes()
            .Where(t => t.IsAssignableTo(typeof(IValidator)))
            .Select(t => Activator.CreateInstance(t) as IValidator)
            .Where(t => t is not null)
            .ToList()!;

    public void Validate()
    {
        var type = GetType();
        ValidateBaseClasses(type);
        Validate(type);
    }

    private void ValidateBaseClasses(Type? type)
    {
        if (type is null) return;
        if (type == typeof(BaseEntity)) return;

        ValidateBaseClasses(type.BaseType);
        Validate(type);
    }

    private void Validate(Type type)
    {
        var validator = FindValidators(type);
        var context = new ValidationContext<object>(this);
        var errors = validator
            .Select(x => x.Validate(context))
            .SelectMany(x => x.Errors)
            .Where(x => x is not null)
            .ToList();

        if (errors.Any()) throw new ValidationException(errors);
    }

    private IEnumerable<IValidator> FindValidators(Type type)
    {
        return Validators
            .Where(x =>
            {
                var interfaces = x.GetType().GetInterfaces();
                var genericTypeArguments = interfaces.Select(i => i.GenericTypeArguments);

                return genericTypeArguments
                    .Any(genericType => genericType.Any(generic => generic == type));
            });
    }
}