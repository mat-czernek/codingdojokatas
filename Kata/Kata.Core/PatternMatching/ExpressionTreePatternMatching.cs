using System.Linq.Expressions;

namespace Kata.Core.PatternMatching;

public static class ExpressionTreePatternMatching
{
    public static Func<string, bool> BuildExpression(string pattern)
    {
        var inputTextParameter = Expression.Parameter(typeof(string), "inputText");
        var body = BuildExpressionBody(pattern, inputTextParameter);
        var expression = Expression.Lambda<Func<string, bool>>(body, inputTextParameter);

        return expression.Compile();
    }

    private static Expression BuildExpressionBody(string pattern, ParameterExpression inputText)
    {
        pattern = pattern.Trim();

        pattern = RemoveOuterBrackets(pattern);
        
        if (pattern.StartsWith("not "))
        {
            var innerPattern = pattern.Substring(4).Trim();
            return Expression.Not(BuildExpressionBody(innerPattern, inputText));
        }
        
        if (pattern.Contains(" and "))
        {
            var parts = SplitLogicalPattern(pattern, "and");
            var left = BuildExpressionBody(parts.Item1, inputText);
            var right = BuildExpressionBody(parts.Item2, inputText);
            return Expression.AndAlso(left, right);
        }

        if (pattern.Contains(" or "))
        {
            var parts = SplitLogicalPattern(pattern, "or");
            var left = BuildExpressionBody(parts.Item1, inputText);
            var right = BuildExpressionBody(parts.Item2, inputText);
            return Expression.OrElse(left, right);
        }
        
        if (pattern.StartsWith("startsWith"))
        {
            var startsWithArgument = ExtractMethodArgument(pattern);
            return Expression.Call(inputText, typeof(string).GetMethod("StartsWith", new[] { typeof(string) }), Expression.Constant(startsWithArgument));
        }

        if (pattern.StartsWith("endsWith"))
        {
            var endsWithArgument = ExtractMethodArgument(pattern);
            return Expression.Call(inputText, typeof(string).GetMethod("EndsWith", new[] { typeof(string) }), Expression.Constant(endsWithArgument));
        }

        if (pattern.StartsWith("contains"))
        {
            var containsArgument = ExtractMethodArgument(pattern);
            return Expression.Call(inputText, typeof(string).GetMethod("Contains", new[] { typeof(string) }), Expression.Constant(containsArgument));
        }

        throw new ArgumentException("Unsupported pattern format.");
    }

    private static string RemoveOuterBrackets(string pattern)
    {
        if (pattern.StartsWith($"(") && pattern.EndsWith($")"))
            pattern = pattern.Substring(1, pattern.Length - 2).Trim();

        return pattern;
    }
    
    private static Tuple<string, string> SplitLogicalPattern(string pattern, string logicalOperator)
    {
        var bracketCount = 0;
        
        for (var i = 0; i < pattern.Length; i++)
        {
            var characterFromPattern = pattern[i];
            
            switch (characterFromPattern)
            {
                case '(':
                    bracketCount++;
                    break;
                case ')':
                    bracketCount--;
                    break;
            }

            if (bracketCount == 0 && pattern.Substring(i).StartsWith($" {logicalOperator} "))
            {
                var left = pattern.Substring(0, i).Trim();
                var right = pattern.Substring(i + logicalOperator.Length + 2).Trim();
                return Tuple.Create(left, right);
            }
        }

        throw new ArgumentException($"Operator {logicalOperator} not found in the pattern.");
    }
    
    private static string ExtractMethodArgument(string pattern)
    {
        var openBracketIndex = pattern.IndexOf('(') + 1;
        var closingBracketIndex = pattern.LastIndexOf(')');
        return pattern.Substring(openBracketIndex, closingBracketIndex - openBracketIndex).Trim('"');
    }
}