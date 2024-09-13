using Kata.Common.Extensions;
using Kata.Core.BinaryChop;
using NUnit.Framework;

namespace Kata.Tests.BinaryChop;

public class BinaryChopTests
{
    [Test]
    [TestCaseSource(nameof(BinaryChop_Cases))]
    [TestCaseSource(nameof(BinaryChop_ChatGPT_SampleCases))]
    public void BinaryChop_Iterative(int targetItemIndexInArray, int target, int[] numbers)
    {
        var foundItemOnIndex = BinaryChopKata.FindFirstIndexIterative(numbers, target);
        Assert.That(foundItemOnIndex, Is.EqualTo(targetItemIndexInArray));
    }
    
    [Test]
    [TestCaseSource(nameof(BinaryChop_Cases))]
    [TestCaseSource(nameof(BinaryChop_ChatGPT_SampleCases))]
    public void BinaryChop_Recursive(int targetItemIndexInArray, int target, int[] numbers)
    {
        var foundItemOnIndex = BinaryChopKata.FindFirstIndexRecursive(numbers, target);
        Assert.That(foundItemOnIndex, Is.EqualTo(targetItemIndexInArray));
    }
    
    [Test]
    [TestCaseSource(nameof(BinaryChop_Cases))]
    [TestCaseSource(nameof(BinaryChop_ChatGPT_SampleCases))]
    public void BinaryChop_Interpolated(int targetItemIndexInArray, int target, int[] numbers)
    {
        var foundItemOnIndex = BinaryChopKata.FindFirstIndexInterpolated(numbers, target);
        Assert.That(foundItemOnIndex, Is.EqualTo(targetItemIndexInArray));
    }

    public static IEnumerable<TestCaseData> BinaryChop_ChatGPT_SampleCases()
    {
        var filePathWithSample = @"TestData\BinaryChop\sample-chatGPT-generated-for-fun.txt".ToProjectDirectory();

        foreach (var lineFromFile in File.ReadLines(filePathWithSample))
        {
            var lineSplit = lineFromFile.Split(";");
            var expectedIndex = int.Parse(lineSplit[0]);
            var targetNumber = int.Parse(lineSplit[1]);
            var numbers = Array.ConvertAll(lineSplit[2].Split(","), int.Parse);

            yield return new TestCaseData(expectedIndex, targetNumber, numbers);
        }
    }

    public static IEnumerable<TestCaseData> BinaryChop_Cases()
    {
        yield return new TestCaseData(-1, 3, Array.Empty<int>());
        yield return new TestCaseData(-1, 3, new int[] { 1 });
        yield return new TestCaseData(0, 1, new int[] { 1 });

        yield return new TestCaseData(0, 1, new int[] { 1, 3, 5 });
        yield return new TestCaseData(1, 3, new int[] { 1, 3, 5 });
        yield return new TestCaseData(2, 5, new int[] { 1, 3, 5 });
        yield return new TestCaseData(-1, 0, new int[] { 1, 3, 5 });
        yield return new TestCaseData(-1, 2, new int[] { 1, 3, 5 });
        yield return new TestCaseData(-1, 4, new int[] { 1, 3, 5 });
        yield return new TestCaseData(-1, 6, new int[] { 1, 3, 5 });

        yield return new TestCaseData(0, 1, new int[] { 1, 3, 5, 7 });
        yield return new TestCaseData(1, 3, new int[] { 1, 3, 5, 7 });
        yield return new TestCaseData(2, 5, new int[] { 1, 3, 5, 7 });
        yield return new TestCaseData(3, 7, new int[] { 1, 3, 5, 7 });
        yield return new TestCaseData(-1, 0, new int[] { 1, 3, 5, 7 });
        yield return new TestCaseData(-1, 2, new int[] { 1, 3, 5, 7 });
        yield return new TestCaseData(-1, 4, new int[] { 1, 3, 5, 7 });
        yield return new TestCaseData(-1, 6, new int[] { 1, 3, 5, 7 });
        yield return new TestCaseData(-1, 8, new int[] { 1, 3, 5, 7 });

        yield return new TestCaseData(4, 9, new int[] { 9, 0, 8, 3, 1 });
    }
}