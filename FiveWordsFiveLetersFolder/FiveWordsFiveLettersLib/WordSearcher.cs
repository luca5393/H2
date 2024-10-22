using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FiveWordsFiveLettersLib
{
    public class WordSearcher
    {
        private ConcurrentBag<string> results = new ConcurrentBag<string>();
        private ConcurrentDictionary<int, string> wordDictionary = new ConcurrentDictionary<int, string>();
        private int[] words;
        private int wordAmount = 5;
        private int wordLength = 5;
        public int wordProgress = 0;
        public int totalWords = 0;

        public event Action<int, int> ProgressUpdated;

        public ConcurrentBag<string> SearchWords(string filePath, int wordA = 5, int wordL = 5)
        {
            results = new ConcurrentBag<string>();
            wordDictionary = new ConcurrentDictionary<int, string>();
            wordLength = wordL;
            wordAmount = wordA;

            CleanWords(filePath);
            wordProgress = 0;
            totalWords = words.Length;

            FindNext(new ConcurrentBag<int>(), 0, 0, 0);
            return results;
        }

        private void CleanWords(string filePath)
        {
            List<int> tempWordList = new List<int>();
            string[] lines = File.ReadAllLines(filePath);


            foreach (string line in lines)
            {
                if (line.Length == wordLength)
                {
                    int bitmask = 0;
                    bool unique = true;
                    foreach (char c in line)
                    {
                        int charbit = 1 << (c - 'a');
                        if ((bitmask & charbit) != 0)
                        {
                            unique = false;
                            break;
                        }
                        bitmask |= charbit;
                    }

                    if (unique)
                    {
                        tempWordList.Add(bitmask);
                        wordDictionary.TryAdd(bitmask, line);
                    }
                }
            }

            words = tempWordList.ToArray();
        } 
        private void FindNext(ConcurrentBag<int> currentWords, int index, int count, int currentWordsAsOne)
        {
            if (count == wordAmount)
            {
                results.Add(string.Join(" ", currentWords.Select(word => wordDictionary[word])));
                return;
            }

            Parallel.For(index, words.Length, i =>
            {
                if ((currentWordsAsOne & words[i]) == 0)
                {
                    if (index == 0)
                    {
                        int progress = Interlocked.Increment(ref wordProgress);
                        ProgressUpdated?.Invoke(progress, totalWords);
                    }
                    FindNext(currentWords, i + 1, count + 1, currentWordsAsOne | words[i]);
                }
            });
            
        }

        public float CalculatePercentage()
        {
            return totalWords == 0 ? 0 : (float)wordProgress / totalWords * 100;
        }
    }
}
