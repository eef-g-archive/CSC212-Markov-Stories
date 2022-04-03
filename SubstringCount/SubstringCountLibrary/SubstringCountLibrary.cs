using System;
using System.Collections.Generic;

namespace SubstringCountLibrary
{
    public class MarkovEntry
    {
        private string key;
        private int count;
        private Dictionary<string, int> distinctKeys = new Dictionary<string, int>();

        // This public variable is just for testing -- could be useful for later though.
        public int DictCount
        {
            get
            {
                int c = 0;
                foreach(string k in distinctKeys.Keys)
                {
                    for(int i = 0; i < distinctKeys[k]; i++)
                    {
                        c++;
                    }
                }
                return c;
            }
        }
        
        /// <summary>
        /// Creates a MarkovEntry object with the key value given in the parameter
        /// </summary>
        public MarkovEntry(string key)
        {
            this.key = key;
            this.count = 0;
        }
        
        /// <summary>
        /// Scans the entirety of the given string and checks for: how many times the MarkovEntry object key appears, what character is directly after the key
        /// as well as how many times that specific character is directly after the key.
        /// </summary>
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

        /// <summary>
        /// Goes through the MarkovEntry's dictionary and prints all of the UNIQUE suffixes attached to it. Does not show how many times each suffix occurs.
        /// </summary>
        public void PrintSuffixes()
        {
            Console.WriteLine($"Printing suffixes for all {distinctKeys.Count} keys");
            foreach(string k in distinctKeys.Keys)
            {
                Console.WriteLine($"  {k} ({distinctKeys[k]})");
            }
        }

        /// <summary>
        /// Creates a string that states the object is a MarkovEntry object, the key in quotes followed by how many times it appears in parenthesis and finally
        /// a list of each suffix and how many times it appears (will show up to 9 suffixes -- Including repeats)
        /// </summary>
        public override string ToString()
        {
            string keyOut = "";
            int o = 0;
            foreach(string k in distinctKeys.Keys)
            {
                for(int i = 0; i < distinctKeys[k]; i++)
                {
                    keyOut += $"'{k}'";
                    o++;
                    if((o < 9) && (o < DictCount))
                    {
                        keyOut += ", ";
                    }
                    else if(o >= 9)
                    {
                        keyOut += ", ...";
                        break;
                    }
                }
                Console.WriteLine(o);
                if(o >= 9)
                {
                    break;
                }
            }

            return $"MarkovEntry '{key}' ({count}) : {keyOut}";
        }
    }
}