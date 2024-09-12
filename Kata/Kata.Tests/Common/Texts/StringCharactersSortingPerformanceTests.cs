using System.Diagnostics;
using Kata.Common.Extensions;
using Kata.Common.Files;
using Kata.Common.Texts;
using NUnit.Framework;

namespace Kata.Tests.Common.Texts;

public class StringCharactersSortingPerformanceTests
{
    [OneTimeSetUp]
    public void BeforeTestsExecution()
    {
        FileHelper.DeleteWhenExists($"{nameof(SortingCharactersInString_WithLinq)}.txt".ToProjectDirectory());
        FileHelper.DeleteWhenExists($"{nameof(SortingCharactersInString_WithArrayOfChar)}.txt".ToProjectDirectory());
    }
    
    [Ignore("Created out of curiosity to check performance with comparision to array of char sorting")]
    [Test]
    [Repeat(30)]
    public void SortingCharactersInString_WithLinq()
    {
        var resultOutputFile = $"{nameof(SortingCharactersInString_WithLinq)}.txt".ToProjectDirectory();
        
        var filePathWithSample = @"TestData\Common\Texts\sampleTextFileWithWordsInLines.txt".ToProjectDirectory();
        var wordsFromFile = FileReader.ReadNonEmptyLines(filePathWithSample);

        var stopwatch = new Stopwatch();
        stopwatch.Start();
        foreach (var word in wordsFromFile)
        {
            word.SortCharactersWithLinq();
        }
        stopwatch.Stop();
        File.AppendAllText(resultOutputFile, $"{stopwatch.ElapsedMilliseconds}{Environment.NewLine}");
        stopwatch.Reset();
    }
    
    [Ignore("Created out of curiosity to check performance with comparision to LINQ sorting")]
    [Test]
    [Repeat(30)]
    public void SortingCharactersInString_WithArrayOfChar()
    {
        var resultOutputFile = $"{nameof(SortingCharactersInString_WithArrayOfChar)}.txt".ToProjectDirectory();
        
        var filePathWithSample = @"TestData\Common\Texts\sampleTextFileWithWordsInLines.txt".ToProjectDirectory();
        var wordsFromFile = FileReader.ReadNonEmptyLines(filePathWithSample);

        var stopwatch = new Stopwatch();
        stopwatch.Start();
        foreach (var word in wordsFromFile)
        {
            word.SortCharactersWithArrayOfChar();
        }
        stopwatch.Stop();
        File.AppendAllText(resultOutputFile, $"{stopwatch.ElapsedMilliseconds}{Environment.NewLine}");
        stopwatch.Reset();
    }
}


    /*
    +------------------------+---------------------------------+
    | Sorting with LINQ [ms] | Sorting with array of char [ms] |
    +------------------------+---------------------------------+
    | 311                    | 88                              |
    +------------------------+---------------------------------+
    | 254                    | 78                              |
    +------------------------+---------------------------------+
    | 157                    | 85                              |
    +------------------------+---------------------------------+
    | 148                    | 88                              |
    +------------------------+---------------------------------+
    | 147                    | 77                              |
    +------------------------+---------------------------------+
    | 146                    | 73                              |
    +------------------------+---------------------------------+
    | 147                    | 75                              |
    +------------------------+---------------------------------+
    | 150                    | 73                              |
    +------------------------+---------------------------------+
    | 152                    | 74                              |
    +------------------------+---------------------------------+
    | 150                    | 76                              |
    +------------------------+---------------------------------+
    | 151                    | 76                              |
    +------------------------+---------------------------------+
    | 162                    | 72                              |
    +------------------------+---------------------------------+
    | 147                    | 72                              |
    +------------------------+---------------------------------+
    | 147                    | 72                              |
    +------------------------+---------------------------------+
    | 147                    | 71                              |
    +------------------------+---------------------------------+
    | 155                    | 71                              |
    +------------------------+---------------------------------+
    | 147                    | 80                              |
    +------------------------+---------------------------------+
    | 143                    | 81                              |
    +------------------------+---------------------------------+
    | 145                    | 79                              |
    +------------------------+---------------------------------+
    | 145                    | 73                              |
    +------------------------+---------------------------------+
    | 148                    | 71                              |
    +------------------------+---------------------------------+
    | 148                    | 72                              |
    +------------------------+---------------------------------+
    | 154                    | 73                              |
    +------------------------+---------------------------------+
    | 148                    | 75                              |
    +------------------------+---------------------------------+
    | 144                    | 72                              |
    +------------------------+---------------------------------+
    | 142                    | 72                              |
    +------------------------+---------------------------------+
    | 147                    | 72                              |
    +------------------------+---------------------------------+
    | 147                    | 73                              |
    +------------------------+---------------------------------+
    | 144                    | 73                              |
    +------------------------+---------------------------------+
    | 144                    | 85                              |
    +------------------------+---------------------------------+
     */