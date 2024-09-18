namespace Kata.Core.PatternMatching;

public static class PatterMatchingWithReversePolishNotation
{
    private static readonly IReadOnlyDictionary<string, int> OperatorsWithPriorities = new Dictionary<string, int>
    {
        {"or", 1},
        {"and", 2},
        {"not", 3},
        {"startsWith", 4},
        {"endsWith", 4},
        {"contains", 4}
    };
    
    public static bool EvaluateReversePolishNotation(string pattern, string inputText)
    {
        if (string.IsNullOrEmpty(pattern) || string.IsNullOrEmpty(inputText))
            return false;
        
        var evalStack = new Stack<bool>();
        var patternAsRpn = ConvertPatternToReversePolishNotation(pattern);

        while (patternAsRpn.Count > 0)
        {
            var token = patternAsRpn.Dequeue();

            if (IsOperator(token))
            {
                if (token == "not")
                {
                    if (evalStack.Count < 1)
                        throw new InvalidOperationException("Cannot evaluate 'not' operator for empty stack. Nothing to apply negation.");
                    
                    var operand = evalStack.Pop();
                    evalStack.Push(!operand);
                }
                else
                {
                    if (evalStack.Count < 2)
                        throw new InvalidOperationException("Cannot apply and/or operator for single or zero elements.");

                    var rightSideExpression = evalStack.Pop();
                    var leftSideExpression = evalStack.Pop();

                    switch (token)
                    {
                        case "and":
                            evalStack.Push(leftSideExpression && rightSideExpression);
                            break;
                        case "or":
                            evalStack.Push(leftSideExpression || rightSideExpression);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException($"Unknown logical operator: {token}");
                    }
                }
            }
            else if (IsTokenAnKnownFunctionWithArgument(token))
            {
                var result = EvaluateFunctionWithArgument(inputText, token);
                evalStack.Push(result);
            }
            else
                throw new InvalidOperationException($"Unknown token '{token}'.");
        }

        if (evalStack.Count != 1)
            throw new InvalidOperationException("Invalid expression evaluation. Expected single item (end result) in evaluation stack.");

        return evalStack.Pop();
    }

    private static bool IsTokenAnKnownFunctionWithArgument(string token)
    {
        return token.StartsWith("startsWith", StringComparison.InvariantCultureIgnoreCase) || 
               token.StartsWith("endsWith", StringComparison.InvariantCultureIgnoreCase) || 
               token.StartsWith("contains", StringComparison.InvariantCultureIgnoreCase);
    }

    private static bool IsOperator(string token)
    {
        return OperatorsWithPriorities.ContainsKey(token);
    }
    
    private static Queue<string> ConvertPatternToReversePolishNotation(string pattern)
    {
        var operatorStack = new Stack<string>();
        var outputQueue = new Queue<string>();

        var tokensFromPattern = TokenizePattern(pattern);

        foreach (var token in tokensFromPattern)
        {
            if (IsOperator(token))
            {
                while (operatorStack.Count > 0 && IsOperator(operatorStack.Peek()) && 
                       OperatorsWithPriorities[operatorStack.Peek()] >= OperatorsWithPriorities[token])
                {
                    outputQueue.Enqueue(operatorStack.Pop());
                }
                operatorStack.Push(token);
            }
            else if (token == "(")
            {
                operatorStack.Push(token);
            }
            else if (token == ")")
            {
                while (operatorStack.Count > 0 && operatorStack.Peek() != "(")
                {
                    outputQueue.Enqueue(operatorStack.Pop());
                }
                if (operatorStack.Count > 0 && operatorStack.Peek() == "(")
                {
                    operatorStack.Pop();
                }
            }
            else
            {
                outputQueue.Enqueue(token);
            }
        }

        while (operatorStack.Count > 0)
            outputQueue.Enqueue(operatorStack.Pop());

        return outputQueue;
    }
    
    private static IReadOnlyCollection<string> TokenizePattern(string pattern)
    {
        var tokens = new List<string>();
        var indexInPattern = 0;
        
        while (indexInPattern < pattern.Length)
        {
            if (char.IsWhiteSpace(pattern[indexInPattern]))
            {
                indexInPattern++;
                continue;
            }

            if (pattern[indexInPattern] == '(' || pattern[indexInPattern] == ')')
            {
                tokens.Add(pattern[indexInPattern].ToString());
                indexInPattern++;
            }
            else if (IsStringAtGivenIndexIsKnownFunctionWithArgument(pattern, indexInPattern))
            {
                var functionToken = ExtractFunctionWithArgument(pattern, ref indexInPattern);
                tokens.Add(functionToken);
            }
            else if (IsStringAtGivenIndexIsKnownLogicalOperator(pattern, indexInPattern))
            {
                var logicalOperator = ExtractOperator(pattern, ref indexInPattern);
                tokens.Add(logicalOperator);
            }
        }

        return tokens.ToArray();
    }
    
    private static bool IsStringAtGivenIndexIsKnownLogicalOperator(string pattern, int index)
    {
        var subStringFromPattern = pattern.Substring(index);

        return subStringFromPattern.StartsWith("and", StringComparison.InvariantCultureIgnoreCase) ||
               subStringFromPattern.StartsWith("or", StringComparison.InvariantCultureIgnoreCase) ||
               subStringFromPattern.StartsWith("not", StringComparison.InvariantCultureIgnoreCase);
    }

    private static bool IsStringAtGivenIndexIsKnownFunctionWithArgument(string pattern, int index)
    {
        var subStringFromPattern = pattern.Substring(index);
        return IsTokenAnKnownFunctionWithArgument(subStringFromPattern);
    }
    
    private static string ExtractOperator(string pattern, ref int indexInPattern)
    {
        if (pattern.Substring(indexInPattern).StartsWith("and", StringComparison.InvariantCultureIgnoreCase))
        {
            indexInPattern += "and".Length;
            return "and";
        }

        if (pattern.Substring(indexInPattern).StartsWith("or", StringComparison.InvariantCultureIgnoreCase))
        {
            indexInPattern += "or".Length;
            return "or";
        }

        if (pattern.Substring(indexInPattern).StartsWith("not", StringComparison.InvariantCultureIgnoreCase))
        {
            indexInPattern += "not".Length;
            return "not";
        }
        
        throw new ArgumentException("Unknown operator detected.");
    }
    
    private static string ExtractFunctionWithArgument(string pattern, ref int indexInPattern)
    {
        var startIndex = indexInPattern;
        while (indexInPattern < pattern.Length && pattern[indexInPattern] != ')')
            indexInPattern++;
        
        indexInPattern++;
        
        return pattern.Substring(startIndex, indexInPattern - startIndex);
    }
    
    private static bool EvaluateFunctionWithArgument(string inputText, string functionCall)
    {
        var startIndex = functionCall.IndexOf('(') + 1;
        var endIndex = functionCall.LastIndexOf(')');
        var argument = functionCall.Substring(startIndex, endIndex - startIndex).Trim('"');

        if (functionCall.StartsWith("startsWith", StringComparison.InvariantCultureIgnoreCase))
            return inputText.StartsWith(argument);

        if (functionCall.StartsWith("endsWith", StringComparison.InvariantCultureIgnoreCase))
            return inputText.EndsWith(argument);

        if (functionCall.StartsWith("contains", StringComparison.InvariantCultureIgnoreCase))
            return inputText.Contains(argument);

        throw new ArgumentException("Unsupported function call.");
    }
}