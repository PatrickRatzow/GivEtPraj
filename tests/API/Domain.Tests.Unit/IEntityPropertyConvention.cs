using System;
using System.Reflection;

namespace Commentor.GivEtPraj.Domain.Tests.Unit;

public interface IEntityPropertyConvention
{
}

public interface IEntityPropertyConvention<TProperty> : IEntityPropertyConvention
{
    IEntityPropertyBuilder<TProperty, TEntity> Run<TEntity>(PropertyInfo propertyInfo, IEntityPropertyBuilder<TProperty, TEntity> builder);
}