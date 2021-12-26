using System.Reflection;

namespace FluentTests.Conventions;

public class BooleanPropertyConvention : IPropertyConvention<bool>
{
    public IGenericPropertyBuilder<bool, TEntity> Run<TEntity>(PropertyInfo propertyInfo, 
        IGenericPropertyBuilder<bool, TEntity> builder)
    {
        if (builder is not ConventionsPropertyBuilder<bool, TEntity> entityBuilder) return builder;
        if (entityBuilder.InvalidExpressions.Count != 0 || entityBuilder.ValidExpressions.Count != 0) return builder;
            
        builder.Valid(true);
        builder.Valid(false);

        return builder;
    }
}