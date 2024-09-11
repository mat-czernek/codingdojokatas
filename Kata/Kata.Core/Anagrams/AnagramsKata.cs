using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using Kata.Common.Files;

namespace Kata.Core.Anagrams;

// Source of the Kata (along with sample file)
// http://codekata.com/kata/kata06-anagrams/

public static class AnagramsKata
{
    public static IReadOnlyDictionary<string, List<string>> FindWordsWithAnagramsInFile(string pathToFileWithWords)
    {
        if (string.IsNullOrEmpty(pathToFileWithWords))
            throw new ArgumentException("Value cannot be null or empty.", nameof(pathToFileWithWords));
        
        var wordsWithAnagrams = new Dictionary<string, List<string>>();
        var wordsFromFile = FileReader.ReadNonEmptyLines(pathToFileWithWords);

        var anagramsCandidates = new Dictionary<string, List<string>>();
        
        foreach (var word in wordsFromFile)
        {
            var wordAsArrayOfChar = word.ToCharArray();
            Array.Sort(wordAsArrayOfChar);

            var anagramGroupKey = string.Concat(wordAsArrayOfChar);

            if (anagramsCandidates.TryGetValue(anagramGroupKey, out var anagramGroupList))
                anagramGroupList.Add(word);
            else
                anagramsCandidates.Add(anagramGroupKey, [word]);
        }
        
        foreach (var group in anagramsCandidates)
        {
            if (group.Value.Count <= 1) continue;
            wordsWithAnagrams.Add(group.Key, group.Value);
        }

        return wordsWithAnagrams;
    }
}