using Kata.Common.Extensions;
using Kata.Common.Files;
using NUnit.Framework;

namespace Kata.Tests.Common.Files;

public class ReadTextFilesTests
{
    [Test]
    [TestCaseSource(nameof(TextFiles_TestCases))]
    public void ReadTextFile_EmptyFile(string filePath, int expectedNumberOfAllLines, int expectedNumberOfNoneEmptyLines)
    {
        var allLines = FileReader.ReadAllLines(filePath);
        Assert.That(allLines.Count(), Is.EqualTo(expectedNumberOfAllLines));

        var noneEmptyLines = FileReader.ReadNonEmptyLines(filePath);
        Assert.That(noneEmptyLines.Count(), Is.EqualTo(expectedNumberOfNoneEmptyLines));
    }

    public static IEnumerable<TestCaseData> TextFiles_TestCases()
    {
        yield return new TestCaseData(@"TestData\Common\TextFileLoading\emptyTextFile.txt".ToProjectDirectory(), 0, 0);
        yield return new TestCaseData(@"TestData\Common\TextFileLoading\oneLineTextFile.txt".ToProjectDirectory(), 1, 1);
        yield return new TestCaseData(@"TestData\Common\TextFileLoading\multipleLinesTextFile.txt".ToProjectDirectory(), 10, 10);
        yield return new TestCaseData(@"TestData\Common\TextFileLoading\multipleLinesWithEmptyOnes.txt".ToProjectDirectory(), 10, 8);
    }
}