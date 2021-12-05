using System.Diagnostics.CodeAnalysis;

namespace Commentor.GivEtPraj.Domain.ValueObjects;

// Don't let ReSharper remove the private set because EF Core needs it
[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local")]
[SuppressMessage("ReSharper", "UnusedMember.Local")]
public class LocalizedString : ValueObject
{
    private LocalizedString()
    {
    }

    private LocalizedString(string danish, string english)
    {
        Danish = danish;
        English = english;
    }

    public string Danish { get; private set; } = null!;
    public string English { get; private set; } = null!;

    public static LocalizedString From(string danish, string english)
    {
        return new(danish, english);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Danish;
        yield return English;
    }
}