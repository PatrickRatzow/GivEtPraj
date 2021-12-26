namespace FluentTests;

internal static class TypeExtensions
{
    internal static bool IsAssignableTo(this Type from, Type to)
        => to.IsAssignableFrom(from);
}