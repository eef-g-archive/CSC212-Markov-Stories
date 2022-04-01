using System;
using SubstringCountLibrary;

namespace Program
{
    public class Program
    {
        static public void Main(string[] args)
        {
            string txt = "Hellothere,It'sEthanhereabouttobringtotheWorldmynewprogram.It'scalledEthan'scode!";
            int len = 2;
            string [] keys = new string [txt.Length / len];
            
            int j = 0;
            for (int i = 0; i < txt.Length; i+=len)
            {
                keys[j] = txt.Substring(i, i + len);
                Console.WriteLine(keys[j]);
                j++;
            }
            MarkovEntry[] entries = new MarkovEntry[keys.Length];

            for(int i = 0; i < keys.Length; i ++)
            {
                Console.WriteLine($"#== Testing MarkovEntry {i} ==#");
                entries[i] = new MarkovEntry(keys[i]);
                entries[i].ScanText(txt);
                Console.WriteLine("\n" + entries[i] + "\n");
                entries[i].PrintDistinctKeys();
            }
        }
    }
}