namespace Kata.Core.BinaryChop;

public static class BinaryChopKata
{
    public static int ChopIterative(int[] numbers, int target)
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
}