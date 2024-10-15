using System.Diagnostics;
using System.Linq;

namespace FiveWordsFiveLetters
{
    internal class Program
    {
        private static List<string> results = new List<string>();
        private static Dictionary<int, string> wordDictionary = new Dictionary<int, string>();
        private static List<int> words = new List<int>();
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Console.WriteLine("Starting cleaning");
            string[] lines = File.ReadAllLines(@"C:\\Users\\HFGF\\Documents\\GitHub\\H2\\FiveWordsFiveLetters\\Words.txt");


            foreach (string line in lines)
            {
                if (line.Length == 5 && line.Distinct().Count() == 5)
                {
                    int bitmask = 0;
                    foreach (char c in line)
                    {
                        bitmask |= 1 << (c - 'a');
                    }
                    words.Add(bitmask);
                    wordDictionary.TryAdd(bitmask, line);
                }
            }
            Console.WriteLine("Cleaning done");
            HashSet<string> sentences = new HashSet<string>();

            Console.WriteLine("Starting Search");
            findNext(new int[0], 0, 0);
            Console.WriteLine("Search Done");

            sw.Stop();

            
            foreach (string word in results) 
            {
                Console.WriteLine(word);
            }
            
            Console.WriteLine("Total count: " + results.Count);
            Console.WriteLine("Total time: " + sw.ElapsedMilliseconds);
        }

        static void findNext(int[] mask, int index, int fullmask)
        {
            if (mask.Length == 5)
            {
                string result = string.Empty;
                foreach (int word in mask)
                {
                    result += wordDictionary[word];
                }
                results.Add(result);
                return;
            }

            for (int i = index; i < words.Count(); i++)
            {
                if ((fullmask & words[i]) == 0)
                {
                    findNext(mask.Append(words[i]).ToArray(), i+1, (fullmask | words[i]));
                }
            }
        }
    }
}
