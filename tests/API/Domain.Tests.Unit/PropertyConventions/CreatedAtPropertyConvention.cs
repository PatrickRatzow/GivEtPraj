using System;
using System.Reflection;
using Commentor.GivEtPraj.Domain.Tests.Unit.ConfigurationBuilderExtensions;

namespace Commentor.GivEtPraj.Domain.Tests.Unit.PropertyConventions;

public class CreatedAtPropertyConvention : IEntityPropertyConvention<DateTime>, IEntityPropertyConvention<DateTimeOffset>
{
    public IEntityPropertyBuilder<DateTime, TEntity> Run<TEntity>(PropertyInfo propertyInfo, 
        IEntityPropertyBuilder<DateTime, TEntity> builder)
    {
        if (propertyInfo.Name != "CreatedAt") return builder;

        return builder.ShouldBeInThePast();
    }

    public IEntityPropertyBuilder<DateTimeOffset, TEntity> Run<TEntity>(PropertyInfo propertyInfo, 
        IEntityPropertyBuilder<DateTimeOffset, TEntity> builder)
    {
        if (propertyInfo.Name != "CreatedAt") return builder;

        return builder.ShouldBeInThePast();
    }
}