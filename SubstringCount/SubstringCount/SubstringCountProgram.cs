using System;
using SubstringCountLibrary;

namespace Program
{
    public class Program
    {
        static public void Main(string[] args)
        {
            string[] words = {"Hello", "World", "It's", "Ethan", "!" };
            string txt = "Hellothere,It'sEthanhereabouttobringtotheWorldmynewprogram.It'scalledEthan'scode!";
            
            MarkovEntry[] entries = new MarkovEntry[words.Length];
            for(int i = 0; i < words.Length; i++)
            {
                Console.WriteLine($"#== Testing MarkovEntry {i} ==#");
                entries[i] = new MarkovEntry(words[i]);
                char[] indexes = entries[i].GetChars(txt);
                foreach(char j in indexes)
                {
                    entries[i].Add(j);
                }
                Console.WriteLine("\n" + entries[i] + "\n");
            }
        }
    }
}