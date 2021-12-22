using System;

namespace Commentor.GivEtPraj.Domain.Tests.Unit.ConfigurationBuilderExtensions;

public static class GuidEntityPropertyBuilderExtensions 
{
    public static IEntityPropertyBuilder<Guid, TEntity> ShouldBeGuid<TEntity>(
        this IEntityPropertyBuilder<Guid, TEntity> propertyBuilder)
    {
        propertyBuilder.Valid(Guid.NewGuid());
        propertyBuilder.Invalid(Guid.Empty);
        
        return propertyBuilder;
    }
}

