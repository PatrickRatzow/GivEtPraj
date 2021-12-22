using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Commentor.GivEtPraj.Domain.Tests.Unit;

public interface IEntityConfigurationBuilder
{
}

public interface IEntityConfigurationBuilder<TEntity> : IEntityConfigurationBuilder where TEntity : BaseEntity
{
    IEntityPropertyBuilder<TProperty, TEntity> Property<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression);
}

public class EntityConfigurationBuilder : IEntityConfigurationBuilder
{
    public List<IEntityPropertyBuilder> PropertyBuilders { get; } = new();
}

public class EntityConfigurationBuilder<TEntity> : EntityConfigurationBuilder, 
    IEntityConfigurationBuilder<TEntity> where TEntity : BaseEntity
{
    public IEntityPropertyBuilder<TProperty, TEntity> Property<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression)
    {
        var builder = new EntityPropertyBuilder<TProperty, TEntity>(propertyExpression);

        PropertyBuilders.Add(builder);

        return builder;
    }
}