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
        var anagramsFromFile = AnagramsKata.FindAnagramsInFile(filePathWithSmallSample).ToArray();
        
        Assert.That(anagramsFromFile.Count, Is.EqualTo(8));
        Assert.That(anagramsFromFile, Is.EquivalentTo(new[] { "maters", "stream", "gallery", "largely", "regally", "art", "rat", "tar" }));
    }
}