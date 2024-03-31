using System;
using System.Collections.Generic;
using System.Linq;
using Features.WordsProvider.Api;

namespace Features.WordsProvider.Impl
{
public class ShuffledWordsProvider : IWordsProvider
{
    private IEnumerable<string> words;
    private Queue<string> wordsForGuess = new();
    private Random rnd = new();


    public ShuffledWordsProvider(IEnumerable<string> words)
    {
        this.words = words;
    }

    public string GetNextWord()
    {
        if (wordsForGuess.Count == 0)
        {
            ShuffleWords();
        }

        return wordsForGuess.Dequeue();
    }

    private void ShuffleWords()
    {
        var myRandomArray = words
            .Select(x => (x, rnd.Next()))
            .OrderBy(tuple => tuple.Item2)
            .Select(tuple => tuple.Item1)
            .ToArray();

        wordsForGuess.Clear();
        foreach (var word in myRandomArray)
        {
            wordsForGuess.Enqueue(word);
        }
    }
}
}