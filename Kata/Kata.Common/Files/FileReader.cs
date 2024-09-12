namespace Kata.Common.Files;

public class FileReader
{
    public static IEnumerable<string> ReadAllLines(string filePath)
    {
        if (File.Exists(filePath) == false)
            throw new FileNotFoundException($"Given file does not exist: {filePath}");

        return File.ReadLines(filePath);
    }

    public static IEnumerable<string> ReadNonEmptyLines(string filePath)
    {
        var allLines = ReadAllLines(filePath);

        foreach (var line in allLines)
        {
            if (string.IsNullOrEmpty(line) == false)
                yield return line;
        }
    }
}