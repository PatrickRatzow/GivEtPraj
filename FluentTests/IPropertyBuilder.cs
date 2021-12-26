using System.Linq.Expressions;

namespace FluentTests;

public interface IPropertyBuilder
{
}

// ReSharper disable once UnusedTypeParameter
public interface IPropertyBuilder<TProperty, TEntity> : IPropertyBuilder
{
}

public interface IGenericPropertyBuilder<TProperty, TEntity> : IPropertyBuilder<TProperty, TEntity>
{
    IGenericPropertyBuilder<TProperty, TEntity> Valid(params TProperty[] values);
    IGenericPropertyBuilder<TProperty, TEntity> Invalid(params TProperty[] values);
    IGenericPropertyBuilder<TProperty, TEntity> Valid(Expression<Func<TEntity, TProperty>> expression);
    IGenericPropertyBuilder<TProperty, TEntity> Invalid(Expression<Func<TEntity, TProperty>> expression);
}

public interface IConventionsPropertyBuilder<TProperty, TEntity> : IGenericPropertyBuilder<TProperty, TEntity>
{
    IConventionsPropertyBuilder<TProperty, TEntity> With<TConvention>() where TConvention : IPropertyConvention<TProperty>;
    IConventionsPropertyBuilder<TProperty, TEntity> Without<TConvention>() where TConvention : IPropertyConvention<TProperty>;
}