using Kata.Common.Extensions;
using Kata.Core.Anagrams;
using NUnit.Framework;

namespace Kata.Tests.Anagrams;

public class AnagramsTests
{
    [Test]
    public void Anagrams_Find_SmallSample()
    {
        var filePathWithSmallSample = @"TestData\Anagrams\small_sample.txt".ToProjectDirectory();
        var wordsWithAnagrams = AnagramsKata.FindWordsWithAnagramsInFile(filePathWithSmallSample).ToArray();
        Assert.That(wordsWithAnagrams.Count, Is.EqualTo(3));
        
        var anagramsOnly = wordsWithAnagrams.SelectMany(x => x.Value);
        Assert.That(anagramsOnly,
            Is.EquivalentTo(new[] { "maters", "stream", "gallery", "largely", "regally", "art", "rat", "tar" }));
    }

    [Test]
    public void Anagrams_Find_BigSample()
    {
        // Source of the Kata (along with sample file)
        // http://codekata.com/kata/kata06-anagrams/

        var filePathWithBigSample = @"TestData\Anagrams\wordlist_codingdojo_expected_20683_anagrams.txt".ToProjectDirectory();
        var wordsWithAnagrams = AnagramsKata.FindWordsWithAnagramsInFile(filePathWithBigSample).ToArray();

        Assert.That(wordsWithAnagrams.Count, Is.EqualTo(20683));
    }
}