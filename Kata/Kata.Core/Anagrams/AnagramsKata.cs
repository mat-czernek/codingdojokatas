using Kata.Common.Files;

namespace Kata.Core.Anagrams;

public static class AnagramsKata
{
    public static IEnumerable<string> FindAnagramsInFile(string pathToFileWithWords)
    {
        if (string.IsNullOrEmpty(pathToFileWithWords))
            throw new ArgumentException("Value cannot be null or empty.", nameof(pathToFileWithWords));

        var anagramsGroups = new Dictionary<string, List<string>>();
        var wordsFromFile = FileReader.ReadNonEmptyLines(pathToFileWithWords);

        foreach (var word in wordsFromFile)
        {
            var wordAsArrayOfChar = word.ToCharArray();
            Array.Sort(wordAsArrayOfChar);

            var anagramGroupKey = string.Concat(wordAsArrayOfChar);

            if (anagramsGroups.TryGetValue(anagramGroupKey, out var anagramGroupList))
                anagramGroupList.Add(word);
            else
                anagramsGroups.Add(anagramGroupKey, [word]);
        }
        
        foreach (var group in anagramsGroups)
        {
            if (group.Value.Count <= 1) continue;
            
            foreach (var anagram in group.Value)
                yield return anagram;
        }
    }
}