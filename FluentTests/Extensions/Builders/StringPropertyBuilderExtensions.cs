using System.Linq.Expressions;

namespace FluentTests;

public abstract partial class AbstractClassConfiguration<TEntity> where TEntity : class
{
    //public IStringPropertyBuilder<TEntity> Property(Expression<Func<TEntity, string>> propertyExpression) 
    //    => new StringPropertyBuilder<TEntity>(propertyExpression);
}

public interface IStringPropertyBuilder<TEntity> : IConventionsPropertyBuilder<string, TEntity>
{
    public IStringPropertyBuilder<TEntity> Length(Range range);
    public IStringPropertyBuilder<TEntity> Length(int start, int end);
    public IStringPropertyBuilder<TEntity> Length(int length);
}

public class StringPropertyBuilder<TEntity> : ConventionsPropertyBuilder<string, TEntity>, IStringPropertyBuilder<TEntity>
{
    public StringPropertyBuilder(Expression<Func<TEntity, string>> propertyExpression) : base(propertyExpression)
    {
    }

    public IStringPropertyBuilder<TEntity> Length(Range range)
    {
        throw new NotImplementedException();
    }

    public IStringPropertyBuilder<TEntity> Length(int start, int end)
    {
        throw new NotImplementedException();
    }

    public IStringPropertyBuilder<TEntity> Length(int length)
    {
        throw new NotImplementedException();
    }
}