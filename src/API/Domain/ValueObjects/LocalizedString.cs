namespace Commentor.GivEtPraj.Domain.ValueObjects;

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

    public string Danish { get; } = null!;
    public string English { get; } = null!;

    public static LocalizedString From(string danish, string english)
    {
        return new(danish, english);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {

        throw new NotImplementedException();
    }
}