using System;
using System.Collections.Generic;

namespace SubstringCountLibrary
{
    public class MarkovEntry
    {
        private string key;
        private int count;
        private Dictionary<string, int> distinctKeys = new Dictionary<string, int>();

        public MarkovEntry(string key)
        {
            this.key = key;
            this.count = 0;
        }
        
        public void ScanText(string text)
        {
            char [] suffixes = GetChars(text);
            foreach(char c in suffixes)
            {
                Add(c);
            }
        }

        /// <summary>
        /// Goes through the given text and locates the character immediately after the MarkovEntry's key value. Adds each character it 
        /// finds to a Linked List in order of how early it appears. Afterwards returns a list of all the characters in the same order they were in.
        /// </summary>
        private char[] GetChars(string text)
        {
            // Can replace List with MyList if needed, but for now using the C# default list class since it's just easier tbh
            List<char> chList = new List<char>();
            int M = key.Length;
            int N = text.Length;
            for (int i = 0; i <= N - M; i++)
            {
                int j;
                for(j = 0; j < M; j++)
                {
                    if (text[i + j] != key[j])
                    {
                        break;
                    }
                }

                if(j == M)
                {
                    char ch;
                    if (i + key.Length >= text.Length)
                    {
                        ch = '_';
                    }
                    else
                    {
                        ch = text[i + key.Length];
                    }
                    chList.Add(ch);
                }

            }
            
            return chList.ToArray();
         }


        /// <summary>
        /// Takes a character, ch, and prints it out with both the MarkovEntry key value followed by ch. Adds 1 to the count value of the MarkovEntry
        /// object since the character comes after where the key was located.
        /// </summary>
        private void Add(char ch)
        {
            if(distinctKeys.ContainsKey(ch.ToString()))
            {
                distinctKeys[ch.ToString()]++;
            }
            else
            {
                distinctKeys.Add(ch.ToString(), 1);
            }
            count++;
        }

        public void PrintDistinctKeys()
        {
            Console.WriteLine($"{distinctKeys.Keys.Count} unique keys");
            foreach(string k in distinctKeys.Keys)
            {
                Console.WriteLine($"  {k} ({distinctKeys[k]})");
            }
            Console.WriteLine("\n");
        }
        public override string ToString()
        {
            return $"MarkovEntry '{key}': {count}";
        }
    }
}