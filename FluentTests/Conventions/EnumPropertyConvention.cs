using System.Reflection;

namespace FluentTests.Conventions;

public class EnumPropertyConvention : IEnumPropertyConvention<Enum>
{
    public IGenericPropertyBuilder<Enum, TEntity> Run<TEntity>(PropertyInfo propertyInfo, 
        IGenericPropertyBuilder<Enum, TEntity> builder)
    {
        throw new NotImplementedException();
    }
    
    /*
     *
     * Property(x => x.Status)
            .Valid(x => Status.Done)
            .Valid(x => 0)
            .Invalid(x => Status.Done + 1)
            .Invalid(x => (Status)(-1));
     */
}