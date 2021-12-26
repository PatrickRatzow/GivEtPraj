using System.Collections;
using System.Linq.Expressions;
using System.Reflection;

namespace FluentTests;

public static class FluentTests
{
    private static readonly Dictionary<Type, EntityDataDictionary> EntityConfigDictionary = new();
    internal static readonly HashSet<Type> Bases = new();
    internal static Assembly Assembly { get; private set; } = null!;
    public static void AddBase(Type type) => Bases.Add(type);
    public static void AddAssembly(Assembly assembly) => Assembly = assembly;
    
    internal static bool TryGetEntityData(Type entityType, out EntityDataDictionary entityDataDictionary)
    {
        ScanEntityConfigurations(entityType);

        return RecursiveEntityDataDictionary(entityType, out entityDataDictionary);
    }

    private static bool RecursiveEntityDataDictionary(Type? entityType, out EntityDataDictionary entityData)
    {
        entityData = new();

        while (entityType is not null && entityType != typeof(object) && Bases.All(x => x != entityType))
        {
            if (!EntityConfigDictionary.TryGetValue(entityType, out var entityDataDictionary))
                return false;

            foreach (var tuple in entityDataDictionary!)
            {
                entityData[tuple.Key] = tuple.Value;
            }
            
            entityType = entityType.BaseType;
        }

        return true;
    }
    
    private static void ScanEntityConfigurations(Type entityType)
    {
        if (EntityConfigDictionary.Any()) return;
        
        var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes());
        var configurations = types
            .Where(t => !t.IsInterface && !t.IsAbstract)
            .Where(t => t.GetInterfaces().Any(x => IsDerivedOfGenericType(x, typeof(IClassConfiguration<>))))
            .ToList();

        foreach (var configuration in configurations)
        {
            var configInterface = configuration.GetInterfaces()
                .First(x => IsDerivedOfGenericType(x, typeof(IClassConfiguration<>)));
            var type = configInterface.GenericTypeArguments
                .Where(t => !t.IsInterface)
                .First(t => Bases.Any(t.IsAssignableTo));
            
            // TODO: I'm not very comfortable with the use of dynamic... but it works, for now...
            if (Activator.CreateInstance(configuration) is not IClassConfiguration config) continue;
            config.Configure();

            var entityBuilder = ((dynamic)config).ConfigurationBuilder;
            ApplyPropertyConventions(config, entityBuilder, type);

            var entityData = GetEntityData(entityBuilder);
            if (entityData is null) continue;

            EntityConfigDictionary.Add(type, entityData);
        }
    }

    private static Dictionary<Type, List<IPropertyConvention>>? _conventions;
    private static void ApplyPropertyConventions(IClassConfiguration config, EntityConfigurationBuilder entityBuilder, 
        Type entityType)
    {
        ScanConventions();

        var properties = entityType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (var property in properties)
        {
            var propertyType = property.PropertyType;
            if (!_conventions!.TryGetValue(propertyType, out var conventions))
            {
                if (propertyType.BaseType != typeof(Enum)) continue;
                if (!_conventions.TryGetValue(propertyType.BaseType, out conventions)) continue;
            }   

            foreach (var convention in conventions!.Where(x => config.Conventions.RemovedConventions.All(c => x.GetType() != c.GetType())))
            {
                // <Guid, TEntity>
                var runMethod = convention.GetType().GetMethods()
                    .Where(m => m.Name is "Run")
                    .Single(m => m.ReturnType.GenericTypeArguments.First() == propertyType);
                var generic = runMethod!.MakeGenericMethod(entityType);

                var constructedType = typeof(ConventionsPropertyBuilder<,>).MakeGenericType(propertyType, entityType);
                var propertyExpression = GetPropertyExpression(property.Name, propertyType, entityType);
                var propertyBuilder = Activator.CreateInstance(constructedType, propertyExpression) as dynamic;
                
                generic.Invoke(convention, new object?[] { property, propertyBuilder });
                
                entityBuilder.PropertyBuilders.Add(propertyBuilder);
            }
        }
    }

    private static void ScanConventions()
    {
        if (_conventions is not null) return;
        
        _conventions = new();

        var assemblyTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes());
        var conventions = assemblyTypes
            .Where(t => !t.IsInterface && !t.IsAbstract)
            .Where(t => t.GetInterfaces().Any(x => IsDerivedOfGenericType(x, typeof(IPropertyConvention<>))))
            .ToList();

        foreach (var convention in conventions)
        {
            var interfaces = convention.GetInterfaces()
                .Where(i => IsDerivedOfGenericType(i, typeof(IPropertyConvention<>)));

            foreach (var @interface in interfaces)
            {
                var propertyType = @interface.GenericTypeArguments.First();
                if (!_conventions.TryGetValue(propertyType, out var conventionList))
                {
                    conventionList = new();
                }

                var conventionInstance = Activator.CreateInstance(convention) as IPropertyConvention;
                if (conventionInstance is null)
                    throw new InvalidCastException(
                        $"Unable to cast {convention.Name} to {nameof(IPropertyConvention)}");

                conventionList!.Add(conventionInstance);

                _conventions[propertyType] = conventionList;
            }
        }
    }

    private static Expression GetPropertyExpression(string propertyName, Type propertyType, Type entityType)
    {
        var entityLambdaExpression = Expression.Parameter(entityType, "x");
        var propertyExpression = Expression.Parameter(propertyType, $"x.{propertyName}");
        var func = typeof(Func<,>).MakeGenericType(entityType, propertyType);
        var lambda = Expression.Lambda(func, propertyExpression, entityLambdaExpression);

        return lambda;
    }

    // TODO: This needs to use less Reflection, very dangerous code
    private static EntityDataDictionary? GetEntityData(EntityConfigurationBuilder entityBuilder)
    {
        var entityDataDictionary = new EntityDataDictionary();
        foreach (var propertyBuilder in entityBuilder.PropertyBuilders)
        {
            var validExpressions = GetValidExpressions(propertyBuilder);
            if (validExpressions is null) return null;
            var invalidExpressions = GetInvalidExpressions(propertyBuilder);
            if (invalidExpressions is null) return null;
            var propertyName = GetPropertyName(propertyBuilder);
            if (propertyName is null) return null;

            entityDataDictionary[propertyName] = (validExpressions, invalidExpressions);
        }

        return entityDataDictionary;
    }

    private static List<dynamic>? GetValidExpressions(IPropertyBuilder propertyBuilder)
    {
        var type = propertyBuilder.GetType();
        var validExpressionsProperty = type
            .GetProperty("ValidExpressions", BindingFlags.Instance | BindingFlags.NonPublic)
            ?.GetValue(propertyBuilder);
        
        return validExpressionsProperty is not ICollection validExpressions ? null : GetExpressions(validExpressions);
    }
    
    private static List<dynamic>? GetInvalidExpressions(IPropertyBuilder propertyBuilder)
    {
        var type = propertyBuilder.GetType();
        var invalidExpressionsProperty = type
            .GetProperty("InvalidExpressions", BindingFlags.Instance | BindingFlags.NonPublic)
            ?.GetValue(propertyBuilder);
        
        return invalidExpressionsProperty is not ICollection invalidExpressions ? null : GetExpressions(invalidExpressions);
    }

    private static string? GetPropertyName(IPropertyBuilder propertyBuilder)
    {
        var type = propertyBuilder.GetType();
        var validExpressionsProperty = type
            .GetProperty("PropertyExpression", BindingFlags.Instance | BindingFlags.NonPublic)
            ?.GetValue(propertyBuilder);

        var expression = validExpressionsProperty as dynamic;
        if (expression is null) return null;
        if (expression.Body is ParameterExpression parameterExpression)
            return string.Join(".", parameterExpression.Name!.Split('.').Skip(1));
        
        return expression.Body.Member.Name;
    }

    private static List<dynamic> GetExpressions(IEnumerable collection)
    {
        var list = new List<dynamic>();
        
        var enumerator = collection.GetEnumerator();
        while (enumerator.MoveNext())
        {
            list.Add(enumerator.Current as dynamic);
        }
        
        return list;
    }

    private static bool IsDerivedOfGenericType(Type type, Type genericType)
    {
        if (type.IsGenericType && type.GetGenericTypeDefinition() == genericType) return true;

        return type.BaseType is not null && IsDerivedOfGenericType(type.BaseType, genericType);
    }
}