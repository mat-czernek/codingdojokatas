namespace Kata.Core.BinaryChop;

public static class BinaryChopKata
{
    public static int FindFirstIndexIterative(int[] numbers, int target)
    {
        Array.Sort(numbers);

        var leftIndex = 0;
        var rightIndex = numbers.Length - 1;

        while (leftIndex <= rightIndex)
        {
            var middleIndex = (leftIndex + rightIndex) / 2;

            if (numbers[middleIndex] == target)
                return middleIndex;

            if (numbers[middleIndex] < target)
                leftIndex = middleIndex + 1;

            if (numbers[middleIndex] > target)
                rightIndex = middleIndex - 1;
        }
        
        return -1;
    }
    
    public static int FindFirstIndexRecursive(int[] numbers, int target)
    {
        Array.Sort(numbers);
        return FindFirstIndexRecursiveInternal(numbers, target, 0, numbers.Length - 1);
    }

    private static int FindFirstIndexRecursiveInternal(int[] numbers, int target, int leftIndex, int rightIndex)
    {
        if (leftIndex > rightIndex) return -1;
        
        var middleIndex = (leftIndex + rightIndex) / 2;
            
        if (numbers[middleIndex] == target)
            return middleIndex;

        if (numbers[middleIndex] < target)
            return FindFirstIndexRecursiveInternal(numbers, target, middleIndex + 1, rightIndex);

        return FindFirstIndexRecursiveInternal(numbers, target, leftIndex, rightIndex - 1);

    }
}