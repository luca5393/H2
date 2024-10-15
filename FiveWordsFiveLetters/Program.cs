using System.Diagnostics;
using System.Linq;

namespace FiveWordsFiveLetters
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Console.WriteLine("Starting cleaning");
            string[] lines = File.ReadAllLines(@"C:\\Users\\HFGF\\Documents\\GitHub\\H2\\FiveWordsFiveLetters\\Words.txt");

            List<string> cleanedLines = new List<string>();

            foreach (string line in lines)
            {
                if (line.Length == 5 && line.Distinct().Count() == 5)
                {
                    cleanedLines.Add(line);
                }
            }
            Console.WriteLine("Cleaning done");
            HashSet<string> sentences = new HashSet<string>();

            Console.WriteLine("Starting Search");
            findNext(cleanedLines, new HashSet<string>(), 0, sentences, "");
            Console.WriteLine("Search Done");

            sw.Stop();

            
            foreach (string word in sentences) 
            {
                Console.WriteLine(word);
            }
            
            Console.WriteLine("Total count: " + sentences.Count);
            Console.WriteLine("Total time: " + sw.ElapsedMilliseconds);
        }

        static void findNext(List<string> words, HashSet<string> currentWords, int usedChars, HashSet<string> result, string lastword)
        {
            if (currentWords.Count == 5)
            {
                result.Add(string.Join(" ", currentWords));
                return;
            }

            /*
            if (currentWords.Count + (words.Count - currentWords.Count) < 5)
            {
                return;
            }
            */

            for (int i = 0; i < words.Count; i++)
            {
                string word = words[i];
                if (string.Compare(word, lastword) > 0 && canUseWord(word, usedChars))
                {
                    currentWords.Add(word);
                    int newUsedChars = usedChars;
                    foreach (char c in word)
                    {
                        newUsedChars |= (1 << (c - 'a'));
                    }

                    findNext(words, currentWords, newUsedChars, result, word);
                    currentWords.Remove(word);
                }
            }
        }


        static bool canUseWord(string word, int usedChars)
        {
            foreach (char c in word) 
            {
                if ((usedChars & (1 << (c - 'a'))) != 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
