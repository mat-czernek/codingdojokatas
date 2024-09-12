using Kata.Common.Texts;
using NUnit.Framework;

namespace Kata.Tests.Common.Texts;

public class StringCharactersSortingTests
{
    [Test]
    [TestCaseSource(nameof(StringCharactersSortingCases))]
    public void StringCharactersSorting_LINQ(string text, string expectedTextSorted)
    {
        var textSorted = text.SortCharactersWithLinq();
        Assert.That(textSorted, Is.EqualTo(expectedTextSorted));
    }
    
    [Test]
    [TestCaseSource(nameof(StringCharactersSortingCases))]
    public void StringCharactersSorting_ArrayOfChar(string text, string expectedTextSorted)
    {
        var textSorted = text.SortCharactersWithLinq();
        Assert.That(textSorted, Is.EqualTo(expectedTextSorted));
    }

    public static IEnumerable<TestCaseData> StringCharactersSortingCases()
    {
        yield return new TestCaseData("0987654321", "0123456789");
        yield return new TestCaseData("abcdefghijklmnoprstuwxyvz", "abcdefghijklmnoprstuvwxyz");
        yield return new TestCaseData("zvyxwutsrponmlkjihgfedcab", "abcdefghijklmnoprstuvwxyz");
        yield return new TestCaseData("fdsf88sdnfdklasf9324324dssdfdsg", "223344889addddddfffffgklnssssss");
        yield return new TestCaseData("998877665544332211", "112233445566778899");
    }
}