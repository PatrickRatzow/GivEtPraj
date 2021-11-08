using Commentor.GivEtPraj.Domain.ValueObjects;

namespace Commentor.GivEtPraj.Application.Common.Mappings;

public class LocalizedStringConverter : ITypeConverter<LocalizedString, string>
{
    public string Convert(LocalizedString source, string destination, ResolutionContext context)
    {
        return context.GetLanguage() switch
        {
            Language.DK => source.Danish,
            Language.EN => source.English,
            _ => source.English
        };
    }
}

public static class LocalizedStringConversionExtensions
{
    private const string LanguageContextKey = "Language";

    public static Language GetLanguage(this ResolutionContext context)
    {
        if (context.Items.TryGetValue(LanguageContextKey, out var language)) return (Language)language!;

        throw new InvalidOperationException("Language not set");
    }

    public static IMappingOperationOptions SetLanguage(this IMappingOperationOptions options, Language language)
    {
        options.Items[LanguageContextKey] = language;

        return options;
    }
}