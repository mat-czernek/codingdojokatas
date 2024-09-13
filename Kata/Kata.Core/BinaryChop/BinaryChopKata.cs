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

    public static int FindFirstIndexInterpolated(int[] numbers, int target)
    {
        Array.Sort(numbers);
        
        var leftIndex = 0;
        var rightIndex = numbers.Length - 1;

        while ((leftIndex <= rightIndex) && (target >= numbers[leftIndex]) && (target <= numbers[rightIndex]))
        {
            if (leftIndex == rightIndex)
                return numbers[leftIndex] == target ? leftIndex : -1;
            
            var position = CalculateInterpolatedPosition(target, leftIndex, rightIndex, numbers);

            if (numbers[position] == target)
                return position;

            if (numbers[position] < target)
                leftIndex = position + 1;
            else
                rightIndex = position - 1;
        }

        return -1;
    }

    private static int CalculateInterpolatedPosition(int target, int leftIndex, int rightIndex, int[] numbers)
    {
        return leftIndex + ((target - numbers[leftIndex]) * (rightIndex - leftIndex)) /
            (numbers[rightIndex] - numbers[leftIndex]);
    }

    public static int FindFirstIndexParallel(int[] numbers, int target)
    {
        if (numbers.Length == 0)
            return -1;
        
        Array.Sort(numbers);

        var result = -1;
        var leftIndex = 0;
        var rightIndex = numbers.Length - 1;

        Parallel.Invoke(() =>
        {
            // Left side of array
            var leftIndexLocal = leftIndex;
            var rightIndexLocal = (leftIndex + rightIndex) / 2;

            while (leftIndexLocal <= rightIndexLocal && result == -1)
            {
                var middle = (leftIndexLocal + rightIndexLocal) / 2;

                if (numbers[middle] == target)
                    result = middle;
                else if (numbers[middle] < target)
                    leftIndexLocal = middle + 1;
                else
                    rightIndexLocal = middle - 1;
            }
        }, () =>
        {
            // Right side of array
            var leftIndexLocal = ((leftIndex + rightIndex) / 2) + 1;
            var rightIndexLocal = rightIndex;

            while (leftIndexLocal <= rightIndexLocal && result == -1)
            {
                var middle = (leftIndexLocal + rightIndexLocal) / 2;

                if (numbers[middle] == target)
                    result = middle;
                else if (numbers[middle] < target)
                    leftIndexLocal = middle + 1;
                else
                    rightIndexLocal = middle - 1;
            }
        });

        return result;
    }
}