namespace Kata.Common.Files;

public static class FileHelper
{
    public static void DeleteWhenExists(string filePath)
    {
        if(File.Exists(filePath) == false)
            return;
        
        File.Delete(filePath);
    }
}