namespace Commentor.GivEtPraj.Domain.ValueObjects;
public class LocalizedString : ValueObject
{
    public string Danish { get; private set; } = null!;
    public string English { get; private set; } = null!;

    private LocalizedString()
    {

    }

    private LocalizedString(string danish, string english)
    {
        Danish = danish;
        English = english;
    }

    public static LocalizedString From(string danish, string english)
    {
        return new LocalizedString(danish, english);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {

        throw new NotImplementedException();
    }
}
