using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Commentor.GivEtPraj.Domain.Tests.Unit;

public class EntityPropertyBuilder<TProperty, TEntity> : IEntityPropertyBuilder<TProperty, TEntity>
{
    internal List<Expression<Func<TEntity, TProperty>>> ValidExpressions { get; } = new();
    internal List<Expression<Func<TEntity, TProperty>>> InvalidExpressions { get; } = new();
    internal Expression<Func<TEntity, TProperty>> PropertyExpression { get; }
    internal IEntityConventionsConfiguration Conventions { get; } = new EntityConventionsConfiguration();

    public EntityPropertyBuilder(Expression<Func<TEntity, TProperty>> propertyExpression)
    {
        PropertyExpression = propertyExpression;
    }

    public IEntityPropertyBuilder<TProperty, TEntity> Valid(params TProperty[] values)
    {
        foreach (var value in values)
        {
            Valid(_ => value);
        }
        
        return this;
    }
    
    public IEntityPropertyBuilder<TProperty, TEntity> Invalid(params TProperty[] values)
    {
        foreach (var value in values)
        {
            Invalid(_ => value);
        }
        
        return this;
    }

    public IEntityPropertyBuilder<TProperty, TEntity> Valid(Expression<Func<TEntity, TProperty>> func)
    {
        ValidExpressions.Add(func);

        return this;
    }

    public IEntityPropertyBuilder<TProperty, TEntity> Invalid(Expression<Func<TEntity, TProperty>> func)
    { 
        InvalidExpressions.Add(func);

        return this;
    }

    public IEntityPropertyBuilder<TProperty, TEntity> With<TConvention>() where TConvention : IEntityPropertyConvention<TProperty>
    {
        Conventions.Add<TConvention>();
        
        return this;
    }
    
    public IEntityPropertyBuilder<TProperty, TEntity> Without<TConvention>() where TConvention : IEntityPropertyConvention<TProperty>
    {
        Conventions.Remove<TConvention>();
        
        return this;
    }
}