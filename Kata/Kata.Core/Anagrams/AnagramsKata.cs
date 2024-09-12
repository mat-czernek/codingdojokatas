using Kata.Common.Files;
using Kata.Common.Texts;

namespace Kata.Core.Anagrams;

// Source of the Kata (along with sample file)
// http://codekata.com/kata/kata06-anagrams/

public static class AnagramsKata
{
    public static IReadOnlyDictionary<string, List<string>> FindWordsWithAnagramsInFile(string pathToFileWithWords)
    {
        if (string.IsNullOrEmpty(pathToFileWithWords))
            throw new ArgumentException("Value cannot be null or empty.", nameof(pathToFileWithWords));
        
        var wordsFromFile = FileReader.ReadNonEmptyLines(pathToFileWithWords);
        var anagramsCandidates = new Dictionary<string, List<string>>();
        
        foreach (var word in wordsFromFile)
        {
            var wordWithSortedCharactersAsGroupKey = word.SortCharactersWithArrayOfChar();

            if (anagramsCandidates.TryGetValue(wordWithSortedCharactersAsGroupKey, out var anagramGroupList))
                anagramGroupList.Add(word);
            else
                anagramsCandidates.Add(wordWithSortedCharactersAsGroupKey, [word]);
        }
        
        var wordsWithAnagrams = new Dictionary<string, List<string>>();
        foreach (var group in anagramsCandidates)
        {
            if (group.Value.Count <= 1) continue;
            wordsWithAnagrams.Add(group.Key, group.Value);
        }

        return wordsWithAnagrams;
    }
}