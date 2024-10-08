namespace Kata.Common.Texts;

public static class StringExtension
{
    public static string SortCharactersWithLinq(this string text)
    {
        // Less efficient than converting string to array of char,
        // sorting array and then converting to string
        // See StringCharactersSortingPerformanceTests for details
        return string.Concat(text.OrderBy(c => c));
    }

    public static string SortCharactersWithArrayOfChar(this string text)
    {
        // Seems to be more efficient than method with LINQ
        // See StringCharactersSortingPerformanceTests for details
        var textAsArrayOfChar = text.ToCharArray();
        Array.Sort(textAsArrayOfChar);
        return string.Concat(textAsArrayOfChar);
    }
}