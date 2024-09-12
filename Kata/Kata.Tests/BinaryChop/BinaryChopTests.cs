using Kata.Core.BinaryChop;
using NUnit.Framework;

namespace Kata.Tests.BinaryChop;

public class BinaryChopTests
{
    [Test]
    [TestCaseSource(nameof(BinaryChop_Cases))]
    public void BinaryChop_Iterative(int targetItemIndexInArray, int target, int[] numbers)
    {
        var foundItemOnIndex = BinaryChopKata.ChopIterative(numbers, target);
        Assert.That(foundItemOnIndex, Is.EqualTo(targetItemIndexInArray));
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