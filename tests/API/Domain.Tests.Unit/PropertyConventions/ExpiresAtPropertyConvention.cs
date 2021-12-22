using System;
using System.Reflection;
using Commentor.GivEtPraj.Domain.Tests.Unit.ConfigurationBuilderExtensions;

namespace Commentor.GivEtPraj.Domain.Tests.Unit.PropertyConventions;

public class ExpiresAtPropertyConvention : IEntityPropertyConvention<DateTime>, IEntityPropertyConvention<DateTimeOffset>
{
    public IEntityPropertyBuilder<DateTime, TEntity> Run<TEntity>(PropertyInfo propertyInfo, 
        IEntityPropertyBuilder<DateTime, TEntity> builder)
    {
        if (propertyInfo.Name != "ExpiresAt") return builder;

        return builder.ShouldBeInTheFuture();
    }

    public IEntityPropertyBuilder<DateTimeOffset, TEntity> Run<TEntity>(PropertyInfo propertyInfo, 
        IEntityPropertyBuilder<DateTimeOffset, TEntity> builder)
    {
        if (propertyInfo.Name != "ExpiresAt") return builder;

        return builder.ShouldBeInTheFuture();
    }
}