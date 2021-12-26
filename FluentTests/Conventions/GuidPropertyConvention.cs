using System.Reflection;

namespace FluentTests.Conventions;

public class GuidPropertyConvention : IPropertyConvention<Guid>
{
    public IGenericPropertyBuilder<Guid, TEntity> Run<TEntity>(PropertyInfo propertyInfo,
        IGenericPropertyBuilder<Guid, TEntity> builder)
        => builder
            .Valid(Guid.NewGuid())
            .Invalid(Guid.Empty);
}