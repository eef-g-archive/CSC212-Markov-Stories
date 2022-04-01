using System;
using SubstringCountLibrary;

namespace Program
{
    public class Program
    {
        static public void Main(string[] args)
        {
            string txt = "Hello there, It's Ethan here about to bring to the World my new program. It's called Ethan's code!";
            int len = 5;
            List<string> keys = new List<string>();

            for (int i = 0; i < txt.Length - len; i++)
            {
                string newKey = txt.Substring(i, len);
                keys.Add(newKey);
            }

            List<MarkovEntry> entries = new List<MarkovEntry>();
            foreach(string key in keys)
            {
                Console.WriteLine($"#== Testing MarkovEntry ==#");
                MarkovEntry entry = new MarkovEntry(key);
                entries.Add(entry);
                entry.ScanText(txt);
                Console.WriteLine("\n" + entry + "\n");
                entry.PrintDistinctKeys();
            }
        }
    }
}