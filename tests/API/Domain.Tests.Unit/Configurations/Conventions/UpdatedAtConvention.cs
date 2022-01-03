using System;
using System.Reflection;
using DomainFixture;

namespace Commentor.GivEtPraj.Domain.Tests.Unit.Configurations.Conventions;

public class UpdatedAtConvention : IPropertyConvention<DateTimeOffset?>
{
    public IGenericPropertyBuilder<DateTimeOffset?, TEntity> Run<TEntity>(PropertyInfo propertyInfo,
        IGenericPropertyBuilder<DateTimeOffset?, TEntity> builder) 
        => propertyInfo.Name switch
        {
            "UpdatedAt" => builder.ShouldBeInThePast().Valid(new DateTimeOffset?()),
            _ => builder
        };
}