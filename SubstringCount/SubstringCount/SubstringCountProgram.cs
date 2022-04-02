using System;
using System.Collections.Generic;
using System.IO;
using SubstringCountLibrary;

namespace Program
{
    public class MainClass
    {
        static public void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Error: Please enter a source file name and a Markov degree 'n'");
                return;
            }

            if (!args[0].Contains(".txt"))
            {
                Console.WriteLine("Error: Please enter a '.txt' file");
                return;
            }

            if (!int.TryParse(args[1], out int k))
            {
                Console.WriteLine("Error: invalid Markov degree 'n'. Please enter an integer");
                return;
            }

            string filename = args[0];
            string text = null;

            try
            {
                text = File.ReadAllText(filename);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Error: Please move your file to the right directory");
                return;
            }

            List<string> keys = new List<string>();


            for (int i = 0; i < text.Length - k; i++)
            {
                string newKey = text.Substring(i, k);
                keys.Add(newKey);
            }



            List<MarkovEntry> entries = new List<MarkovEntry>();
            foreach (string key in keys)
            {
                Console.WriteLine($"#== Testing MarkovEntry ==#");
                MarkovEntry entry = new MarkovEntry(key);
                entries.Add(entry);
                entry.ScanText(text);
                Console.WriteLine("\n" + entry + "\n");
                entry.PrintDistinctKeys();
            }
        }
    }
}