using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using FiveWordsFiveLettersLib;

namespace FiveWordsFiveLetters
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string filePath = @"C:\Users\HFGF\Documents\GitHub\H2\FiveWordsFiveLetters\Words.txt";
            var wordSearcher = new WordSearcher();

            Stopwatch sw = new Stopwatch();
            sw.Start();

            Console.WriteLine("Starting search...");
            var results = wordSearcher.SearchWords(filePath);
            sw.Stop();

            Console.WriteLine("\nSearch done.");

            foreach (string word in results)
            {
                Console.WriteLine(word);
            }

            Console.WriteLine("Total count: " + results.Count);
            Console.WriteLine($"Total time: {sw.ElapsedMilliseconds} ms");
        }
    }
}
