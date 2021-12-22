using System;
using System.Linq;
using System.Linq.Expressions;

namespace Commentor.GivEtPraj.Domain.Tests.Unit;

public interface IEntityPropertyBuilder
{
}

public interface IEntityPropertyBuilder<TProperty, TEntity> : IEntityPropertyBuilder
{
    IEntityPropertyBuilder<TProperty, TEntity> Valid(params TProperty[] values);
    IEntityPropertyBuilder<TProperty, TEntity> Invalid(params TProperty[] values);
    IEntityPropertyBuilder<TProperty, TEntity> Valid(Expression<Func<TEntity, TProperty>> expression);
    IEntityPropertyBuilder<TProperty, TEntity> Invalid(Expression<Func<TEntity, TProperty>> expression);
    IEntityPropertyBuilder<TProperty, TEntity> With<TConvention>() where TConvention : IEntityPropertyConvention<TProperty>;
    IEntityPropertyBuilder<TProperty, TEntity> Without<TConvention>() where TConvention : IEntityPropertyConvention<TProperty>;
}