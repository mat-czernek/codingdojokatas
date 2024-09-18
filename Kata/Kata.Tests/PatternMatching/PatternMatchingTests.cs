using Kata.Core.PatternMatching;
using NUnit.Framework;

namespace Kata.Tests.PatternMatching;

public class PatternMatchingTests
{
    [Test]
    [TestCaseSource(nameof(PatternMatching_ExpressionTree_ExpectedSuccess_TestCases))]
    public void PatternMatching_ExpressionTree_Test(string inputText, string pattern, bool hasFoundPattern)
    {
        var patternMatcher = ExpressionTreePatternMatching.BuildExpression(pattern);
        var hasTextMatchPattern = patternMatcher(inputText);
        
        Assert.That(hasTextMatchPattern, Is.EqualTo(hasFoundPattern));
    }

    public static IEnumerable<TestCaseData> PatternMatching_ExpressionTree_ExpectedSuccess_TestCases()
    {
        yield return new TestCaseData("sample-file_name-12345.txt", "startsWith(\"sample\")", true);
        yield return new TestCaseData("sample-file_name-12345.txt", "startsWith(\"test\")", false);
        yield return new TestCaseData("sample-file_name-12345.txt", "endsWith(\".txt\")", true);
        yield return new TestCaseData("sample-file_name-12345.txt", "endsWith(\".exe\")", false);
        yield return new TestCaseData("sample-file_name-12345.txt", "contains(\"name\")", true);
        yield return new TestCaseData("sample-file_name-12345.txt", "contains(\"someText\")", false);
        yield return new TestCaseData("sample-file_name-12345.txt", "contains(\"someText\") and contains(\"123\")", false);
        yield return new TestCaseData("sample-file_name-12345.txt", "contains(\"sample\") and contains(\"123\")", true);
        yield return new TestCaseData("sample-file_name-12345.txt", "contains(\"someText\") or contains(\"123\")", true);
        yield return new TestCaseData("someFile-23-name-for-more-12-Complex-pattern.txt", "startsWith(\"some\") and contains(\"55\") and (not endsWith(\"exe\"))", false);
        yield return new TestCaseData("someFile-23-name-for-more-12-Complex-pattern.txt", "startsWith(\"some\") and endsWith(\"txt\") and (not contains(\"test\") and not contains(\"otherText\") )", true);
        yield return new TestCaseData("someFile-23-name-for-more-12-Complex-pattern.txt", "startsWith(\"s\") and startsWith(\"q\")", false);
        yield return new TestCaseData("someFile-23-name-for-more-12-Complex-pattern.txt", "    startsWith(\"some\")  and         endsWith(\"txt\") and     (not     contains(\"test\") and            not contains(\"otherText\") )       ", true);

        // This one is currently failing
        // It building following expression: $inputText.StartsWith("s") && !($inputText.Contains("version") && !( $inputText.Contains("parent") && ( $inputText.Contains("umbrella") || $inputText.Contains("car"))))
        yield return new TestCaseData("someFile-23-name-for-more-12-Complex-pattern.txt", "startsWith(\"s\") and not contains(\"version\") and not contains(\"parent\") and (contains(\"umbrella\") or contains(\"car\"))", false);
    }
}