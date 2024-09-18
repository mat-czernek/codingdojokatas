using Kata.Core.PatternMatching;
using NUnit.Framework;

namespace Kata.Tests.PatternMatching;

public class PatternMatchingTests
{
    [Test]
    [Ignore("Approach with expression tree does not yet cover all edge cases")]
    [TestCaseSource(nameof(PatternMatching_ExpressionTree_ExpectedSuccess_TestCases))]
    public void PatternMatching_ExpressionTree_Test(string inputText, string pattern, bool hasFoundPattern)
    {
        var patternMatcher = ExpressionTreePatternMatching.BuildExpression(pattern);
        var hasTextMatchPattern = patternMatcher(inputText);
        
        Assert.That(hasTextMatchPattern, Is.EqualTo(hasFoundPattern));
    }
    
    [Test]
    [TestCaseSource(nameof(PatternMatching_ExpressionTree_ExpectedSuccess_TestCases))]
    public void PatternMatching_RPN_Test(string inputText, string pattern, bool hasFoundPattern)
    {
        var result = PatterMatchingWithReversePolishNotation.EvaluateReversePolishNotation(pattern, inputText);
        
        Assert.That(result, Is.EqualTo(hasFoundPattern));
    }

    public static IEnumerable<TestCaseData> PatternMatching_ExpressionTree_ExpectedSuccess_TestCases()
    {
        yield return new TestCaseData("", "", false);
        yield return new TestCaseData("sample-file_name-12345.txt", "", false);
        yield return new TestCaseData("", "startsWith(\"sample\")", false);
        
        yield return new TestCaseData("sample-file_name-12345.txt", "startsWith(\"sample\")", true);
        yield return new TestCaseData("sample-file_name-12345.txt", "not startsWith(\"sample\")", false);
        yield return new TestCaseData("sample-file_name-12345.txt", "startsWith(\"test\")", false);
        yield return new TestCaseData("sample-file_name-12345.txt", "not startsWith(\"test\")", true);
        
        
        yield return new TestCaseData("sample-file_name-12345.txt", "endsWith(\".txt\")", true);
        yield return new TestCaseData("sample-file_name-12345.txt", "not endsWith(\".txt\")", false);
        yield return new TestCaseData("sample-file_name-12345.txt", "endsWith(\".exe\")", false);
        yield return new TestCaseData("sample-file_name-12345.txt", "not endsWith(\".exe\")", true);
        
        yield return new TestCaseData("sample-file_name-12345.txt", "contains(\"name\")", true);
        yield return new TestCaseData("sample-file_name-12345.txt", "not contains(\"name\")", false);
        yield return new TestCaseData("sample-file_name-12345.txt", "contains(\"someText\")", false);
        
        yield return new TestCaseData("sample-file_name-12345.txt", "contains(\"someText\") and contains(\"123\")", false);
        yield return new TestCaseData("sample-file_name-12345.txt", "contains(\"sample\") and contains(\"123\")", true);
        yield return new TestCaseData("sample-file_name-12345.txt", "contains(\"someText\") or contains(\"123\")", true);
        
        yield return new TestCaseData("someFile-23-name-for-more-12-Complex-pattern.txt", "startsWith(\"some\") and contains(\"55\") and (not endsWith(\"exe\"))", false);
        yield return new TestCaseData("someFile-23-name-for-more-12-Complex-pattern.txt", "startsWith(\"some\") and endsWith(\"txt\") and (not contains(\"test\") and not contains(\"otherText\") )", true);
        yield return new TestCaseData("someFile-23-name-for-more-12-Complex-pattern.txt", "startsWith(\"s\") and startsWith(\"q\")", false);
        yield return new TestCaseData("someFile-23-name-for-more-12-Complex-pattern.txt", "    startsWith(\"some\")  and         endsWith(\"txt\") and     (not     contains(\"test\") and            not contains(\"otherText\") )       ", true);
        yield return new TestCaseData("someFile-23-name-for-more-12-Complex-pattern.txt", "startsWith(\"s\") and not contains(\"version\") and not contains(\"parent\") and (contains(\"umbrella\") or contains(\"car\"))", false);
        yield return new TestCaseData("someFile-23-name-for-more-12-Complex-pattern.txt", "startsWith(\"s\") or not contains(\"version\") or not contains(\"parent\") or (contains(\"pattern\") or contains(\"car\"))", true);
        yield return new TestCaseData("someFile-23-name-for-more-12-Complex-pattern.txt", "startsWith(\"s\") and (not contains(\"version\") or contains(\"pattern\"))", true);
        yield return new TestCaseData("someFile-23-name-for-more-12-Complex-pattern.txt", "startsWith(\"som\") and contains(\"more\") and (not contains(\"2024\") and not contains(\"contract\"))", true);
        yield return new TestCaseData("someFile-23-name-for-more-12-Complex-pattern.txt", "startsWith(\"xyz\") and endsWith(\"zip\") and contains(\"zebra\") or (startsWith(\"abc\") or startsWith(\"xyz\"))", false);
        
        yield return new TestCaseData("sample-file_name-12345.txt", "((((contains(\"someText\"))) or ((contains(\"123\")))))", true);
        yield return new TestCaseData("sample-file_name-12345.txt", "((((contains(\"someText\"))) or ((contains(\"123\"))))) or ((((contains(\"someText\"))) or ((contains(\"123\")))))", true);
        yield return new TestCaseData("sample-file_name-12345.txt", "((((contains(\"someText\"))) or ((contains(\"123\"))))) and ((((contains(\"someText\"))) or ((contains(\"123\")))))", true);
        yield return new TestCaseData("sample-file_name-12345.txt", "((((contains(\"someText\"))) and ((contains(\"123\"))))) or ((((contains(\"someText\"))) and ((contains(\"123\")))))", false);
    }
}