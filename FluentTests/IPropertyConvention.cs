using System.Reflection;

namespace FluentTests;

public interface IPropertyConvention
{
}

public interface IPropertyConvention<TProperty> : IPropertyConvention
{
    IGenericPropertyBuilder<TProperty, TEntity> Run<TEntity>(PropertyInfo propertyInfo, 
        IGenericPropertyBuilder<TProperty, TEntity> builder);
}

public interface IEnumPropertyConvention<TProperty> : IPropertyConvention
{
    
}