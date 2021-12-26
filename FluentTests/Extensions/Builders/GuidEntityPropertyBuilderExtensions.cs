namespace FluentTests;

public static class GuidEntityPropertyBuilderExtensions 
{
    public static IGenericPropertyBuilder<Guid, TEntity> ShouldBeGuid<TEntity>(
        this IGenericPropertyBuilder<Guid, TEntity> propertyBuilder)
    {
        propertyBuilder.Valid(Guid.NewGuid());
        propertyBuilder.Invalid(Guid.Empty);
        
        return propertyBuilder;
    }
}

