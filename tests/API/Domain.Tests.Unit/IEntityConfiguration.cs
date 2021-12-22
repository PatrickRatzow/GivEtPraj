using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Commentor.GivEtPraj.Domain.Tests.Unit;

public interface IEntityConfiguration
{
    IEntityConventionsConfiguration Conventions { get; }
    void Configure();
}

public interface IEntityConfiguration<TEntity> : IEntityConfiguration where TEntity : BaseEntity
{
    public IEntityConfigurationBuilder<TEntity> ConfigurationBuilder { get; }
}

public interface IEntityConventionsConfiguration
{
    HashSet<IEntityPropertyConvention> AddedConventions { get; }
    HashSet<IEntityPropertyConvention> RemovedConventions { get; }
    IEntityConventionsConfiguration Add<TConvention>() where TConvention : IEntityPropertyConvention;
    IEntityConventionsConfiguration Add(Type type);
    IEntityConventionsConfiguration Remove<TConvention>() where TConvention : IEntityPropertyConvention;
    IEntityConventionsConfiguration Remove(Type type);
}

public class EntityConventionsConfiguration : IEntityConventionsConfiguration
{
    public HashSet<IEntityPropertyConvention> AddedConventions { get; } = new();
    public HashSet<IEntityPropertyConvention> RemovedConventions { get; } = new();

    public IEntityConventionsConfiguration Add<TConvention>() where TConvention : IEntityPropertyConvention
    {
        var instance = Activator.CreateInstance<TConvention>();

        AddedConventions.Add(instance);

        return this;
    }

    public IEntityConventionsConfiguration Add(Type type)
    {
        if (Activator.CreateInstance(type) is not IEntityPropertyConvention instance) 
            throw new InvalidOperationException($"{type.Name} is not an property convention");
        
        AddedConventions.Add(instance);

        return this;
    }

    public IEntityConventionsConfiguration Remove<TConvention>() where TConvention : IEntityPropertyConvention
    {
        var instance = Activator.CreateInstance<TConvention>();

        RemovedConventions.Add(instance);

        return this;
    }

    public IEntityConventionsConfiguration Remove(Type type)
    {
        if (Activator.CreateInstance(type) is not IEntityPropertyConvention instance) 
            throw new InvalidOperationException($"{type.Name} is not an property convention");
        
        RemovedConventions.Add(instance);

        return this;
    }
}

public abstract class AbstractEntityConfiguration<TEntity> : IEntityConfiguration<TEntity> where TEntity : BaseEntity
{
    public IEntityConfigurationBuilder<TEntity> ConfigurationBuilder { get; } 
        = new EntityConfigurationBuilder<TEntity>();
    public IEntityConventionsConfiguration Conventions { get; } 
        = new EntityConventionsConfiguration();

    public IEntityPropertyBuilder<TProperty, TEntity> Property<TProperty>(
        Expression<Func<TEntity, TProperty>> propertyExpression
    ) => ConfigurationBuilder.Property(propertyExpression);

    public abstract void Configure();
}