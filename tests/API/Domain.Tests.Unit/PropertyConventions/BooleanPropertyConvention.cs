using System.Reflection;

namespace Commentor.GivEtPraj.Domain.Tests.Unit.PropertyConventions;

public class BooleanPropertyConvention : IEntityPropertyConvention<bool>
{
    public IEntityPropertyBuilder<bool, TEntity> Run<TEntity>(PropertyInfo propertyInfo, 
        IEntityPropertyBuilder<bool, TEntity> builder)
    {
        if (builder is not EntityPropertyBuilder<bool, TEntity> entityBuilder) return builder;
        if (entityBuilder.InvalidExpressions.Count != 0 || entityBuilder.ValidExpressions.Count != 0) return builder;
            
        builder.Valid(true);
        builder.Valid(false);

        return builder;
    }
}