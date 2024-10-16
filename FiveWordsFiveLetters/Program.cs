using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FiveWordsFiveLetters
{
    internal class Program
    {
        private static List<string> results = new List<string>();
        private static Dictionary<int, string> wordDictionary = new Dictionary<int, string>();
        private static int[] words;

        private static int wordAmount = 5;
        private static int wordLength = 5;
        static void Main(string[] args)
        {

            List<int> tempWordList = new List<int>();

            Stopwatch sw = new Stopwatch();
            Stopwatch ssw = new Stopwatch();
            sw.Start();
            ssw.Start();
            Console.WriteLine("Starting cleaning");
            string[] lines = File.ReadAllLines(@"C:\\Users\\HFGF\\Documents\\GitHub\\H2\\FiveWordsFiveLetters\\Words.txt");

            foreach (string line in lines)
            {
                if (line.Length == wordLength)
                {
                    int bitmask = 0;
                    bool unique = true;
                    foreach (char c in line)
                    {
                        if ((bitmask & 1 << (c - 'a')) != 0)
                        {
                            unique = false;
                            break;
                        }
                        bitmask |= 1 << (c - 'a');
                    }

                    if (unique)
                    {
                        tempWordList.Add(bitmask);
                        wordDictionary.TryAdd(bitmask, line);
                    }
                }
            }
            words = tempWordList.ToArray();
            ssw.Stop();
            Console.WriteLine("Cleaning done");
            Console.WriteLine($"Time for clean: {ssw.ElapsedMilliseconds}");
            HashSet<string> sentences = new HashSet<string>();

            Console.WriteLine("Starting Search");
            int[] currentWords = new int[wordAmount];
            findNext(currentWords, 0, 0, 0);
            Console.WriteLine("Search Done");

            sw.Stop();

            foreach (string word in results) 
            {
                Console.WriteLine(word);
            }

            Console.WriteLine("Total count: " + results.Count);
            Console.WriteLine($"Total time: { sw.ElapsedMilliseconds}");
        }

        static void findNext(int[] currentWords, int index, int count, int currentWordsAsOne)
        {
            if (count == wordAmount)
            {

                results.Add(string.Concat(currentWords.Select(word => wordDictionary[word])));
                return;

            }
            else if (words.Length - index < wordAmount - count)
            {
                return;
            }

            for (int i = index; i < words.Length; i++)
            {
                if ((currentWordsAsOne & words[i]) == 0)
                {
                    currentWords[count] = words[i];
                    findNext(currentWords, i + 1, count + 1, currentWordsAsOne | words[i]);
                }
            }
            
        }
    }
}
