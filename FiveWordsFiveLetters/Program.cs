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
            findNext(cleanedLines, new List<string>(), new HashSet<char>(), sentences, "");
            Console.WriteLine("Search Done");

            sw.Stop();

            foreach (string word in sentences) 
            {
                Console.WriteLine(word);
            }
            Console.WriteLine("Total count: " + sentences.Count);
            Console.WriteLine("Total time: " + sw.ElapsedMilliseconds);
        }

        static void findNext(List<string> lines, List<string> currentWords, HashSet<char> usedChars, HashSet<string> result, string lastword)
        {
            if (currentWords.Count == 5)
            {
                result.Add(string.Join(" ", currentWords));
                return;
            }

            foreach (string line in lines)
            {
                if(string.Compare(line, lastword) > 0 && canUseWord(line, usedChars))
                {
                    currentWords.Add(line);
                    foreach (char c in line) 
                    {
                        usedChars.Add(c);
                    }

                    findNext(lines, currentWords, usedChars, result, line);

                    currentWords.Remove(line);
                    foreach(char c in line)
                    {
                        usedChars.Remove(c);
                    }
                }
            }
        }

        static bool canUseWord(string word, HashSet<char> usedChars)
        {
            foreach (char c in word) 
            {
                if(usedChars.Contains(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
