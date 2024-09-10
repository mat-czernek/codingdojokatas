namespace Kata.Common.Extensions;

public static class StringExtension
{
    public static string ToProjectDirectory(this string pathPart)
    {
        var projectDirectory = Environment.CurrentDirectory;

        return Path.Combine(projectDirectory, pathPart);
    }
    
}