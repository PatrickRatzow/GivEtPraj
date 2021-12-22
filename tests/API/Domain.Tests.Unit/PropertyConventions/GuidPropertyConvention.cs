using System;
using System.Reflection;

namespace Commentor.GivEtPraj.Domain.Tests.Unit.PropertyConventions;

public class GuidPropertyConvention : IEntityPropertyConvention<Guid>
{
    public IEntityPropertyBuilder<Guid, TEntity> Run<TEntity>(PropertyInfo propertyInfo,
        IEntityPropertyBuilder<Guid, TEntity> builder)
        => builder
            .Valid(Guid.NewGuid())
            .Invalid(Guid.Empty);
}