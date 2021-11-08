using Commentor.GivEtPraj.Application.Common.Mappings;

namespace Commentor.GivEtPraj.Application.Common.Services;

public class LocalizedMapper : Mapper, IMapper
{
    private readonly ILanguageService _languageService = null!;

    public LocalizedMapper(IConfigurationProvider configurationProvider) : base(configurationProvider)
    {
    }

    public LocalizedMapper(IConfigurationProvider configurationProvider,
        Func<Type, object> serviceCtor,
        ILanguageService languageService) : base(configurationProvider, serviceCtor)
    {
        _languageService = languageService;
    }

    public new TDestination Map<TSource, TDestination>(TSource source)
    {
        return base.Map<TSource, TDestination>(source, opts => opts.SetLanguage(_languageService.Language));
    }

    public new TDestination Map<TSource, TDestination>(TSource source,
        Action<IMappingOperationOptions<TSource, TDestination>> opts)
    {
        return base.Map(source, default(TDestination)!, options =>
        {
            opts.Invoke(options);
            options.SetLanguage(_languageService.Language);
        });
    }

    public new TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
    {
        return base.Map(source, destination, opts => opts.SetLanguage(_languageService.Language));
    }

    public new TDestination Map<TSource, TDestination>(TSource source, TDestination destination,
        Action<IMappingOperationOptions<TSource, TDestination>> opts)
    {
        return base.Map(source, destination, options =>
        {
            opts.Invoke(options);
            options.SetLanguage(_languageService.Language);
        });
    }
}