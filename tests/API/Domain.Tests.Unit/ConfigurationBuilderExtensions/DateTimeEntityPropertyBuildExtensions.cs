using System;

namespace Commentor.GivEtPraj.Domain.Tests.Unit.ConfigurationBuilderExtensions;

public static class DateTimeEntityPropertyBuildExtensions
{
    public static IEntityPropertyBuilder<DateTimeOffset, TEntity> ShouldBeInTheFuture<TEntity>(
        this IEntityPropertyBuilder<DateTimeOffset, TEntity> propertyBuilder)
    {
        propertyBuilder.Valid(_ => DateTimeOffset.UtcNow.AddSeconds(1));
        propertyBuilder.Invalid(_ => DateTimeOffset.UtcNow.AddSeconds(-1));
    
        return propertyBuilder;
    }

    public static IEntityPropertyBuilder<DateTimeOffset, TEntity> ShouldBeInThePast<TEntity>(
        this IEntityPropertyBuilder<DateTimeOffset, TEntity> propertyBuilder)
    {
        propertyBuilder.Valid(_ => DateTimeOffset.UtcNow.AddSeconds(-1));
        propertyBuilder.Invalid(_ => DateTimeOffset.UtcNow.AddSeconds(1));

        return propertyBuilder;
    }

    public static IEntityPropertyBuilder<DateTimeOffset, TEntity> ShouldBeNow<TEntity>(
        this IEntityPropertyBuilder<DateTimeOffset, TEntity> propertyBuilder)
    {
        propertyBuilder.Valid(_ => DateTimeOffset.UtcNow);
    
        return propertyBuilder;
    }

    public static IEntityPropertyBuilder<DateTime, TEntity> ShouldBeInTheFuture<TEntity>(
        this IEntityPropertyBuilder<DateTime, TEntity> propertyBuilder)
    {
        propertyBuilder.Valid(_ => DateTime.UtcNow.AddSeconds(1));
        propertyBuilder.Invalid(_ => DateTime.UtcNow.AddSeconds(-1));

        return propertyBuilder;
    }
    
    public static IEntityPropertyBuilder<DateTime, TEntity> ShouldBeInThePast<TEntity>(
        this IEntityPropertyBuilder<DateTime, TEntity> propertyBuilder)
    {
        propertyBuilder.Valid(_ => DateTime.UtcNow.AddSeconds(-1));
        propertyBuilder.Invalid(_ => DateTime.UtcNow.AddSeconds(1));

        return propertyBuilder;
    }
    
        
    public static IEntityPropertyBuilder<DateTime, TEntity> ShouldBeNow<TEntity>(
        this IEntityPropertyBuilder<DateTime, TEntity> propertyBuilder)
    {
        propertyBuilder.Valid(_ => DateTime.UtcNow);

        return propertyBuilder;
    }
}