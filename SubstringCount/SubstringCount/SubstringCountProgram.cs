using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using SubstringCountLibrary;

namespace Program
{
    public class MainClass
    {
        static public void Main(string[] args)
        {
            // Check and make sure all the command line arguments are used correctly
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
            
            if (!int.TryParse(args[2], out int len))
            {
                Console.WriteLine("Error: Invalid story length 'm'. Please enter an integer");
                return;
            }

            // Prep variables for reading the file
            string filename = args[0];
            string text = null;
            string[] textArr;
            Stopwatch watch = new Stopwatch();
            

            // Start the stopwatch to time how long the story generation takes
            watch.Start();
            // Try to read the file
            try
            {
                textArr = File.ReadAllLines(filename);

                // Take each line and combine them into one long string, that way the MarkovEntry objects don't accidentally pick up the new line characters as well
                foreach(string line in textArr)
                {
                    text += line + " ";
                }
            }
            // If there isn't a file by the name provided in the command line, print the exception and tell the user about the error. 
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Error: Please move your file to the right directory");
                return;
            }

            // Create a linked list of all the unique keys in the text that are 'n' length (The 'n' value is decided by the user.
            List<string> keys = new List<string>();

            // Go through the text and use value 'k' (which is the 'n' value) to cut substrings and use them as the keys
            for (int i = 0; i < text.Length - k; i++)
            {
                string newKey = text.Substring(i, k);
                if(!keys.Contains(newKey))
                {
                    keys.Add(newKey);
                }
            }



            // Create a .NET BST of MarkovEntry objects
            // This is now a SortedDictionary instead of a Dictionary, since that's what Tallman wants in the final project
            SortedDictionary<string, MarkovEntry> entries = new SortedDictionary<string, MarkovEntry>();

            // Go through all the unique keys, make a MarkovEntry object using that key, add the object to the linked list (in case we need to access them later)
            // and then use the MarkovEntry object to scan the text for it's key and pick up all the data it collects.
            foreach (string key in keys)
            {
                MarkovEntry entry = new MarkovEntry(key);
                entries.Add(key, entry);
                entry.ScanText(text);
            }

            // To print the story, need to go through all the MarkovEntries and print a letter until you've printed the amount of how long you want it to be.
            int curr = 0; // This int keeps track of the current character count of the generated story
            string story = text.Substring(0, k + 1); // This string is the generated story, it gets each character added to it one at a time
            Console.WriteLine(story);
            // Enter the story-generating loop here
            while (curr < len)
            {
                // string ch is found by:
                // 1) Getting a string from the array of keys from earlier in the code via the idx int
                // 2) Using keys[idx], which retruns a string such as "veni," and looks in the symbol table of entries using that key
                // 3) Assumes that if a key is found, a MarkovEntry object is attached to it
                // 4) Calls the RandomLetter() function of that MarkovEntry object
                // 5) Converts the char variable that is returned from the RandomLetter() function to a string
                string ch = entries[story.Substring(story.Length - k, k)].RandomLetter().ToString();
                
                // Add the randomly picked character, ch, to the string of the random story.
                story += ch;
                
                // Change curr to be the length of the story.
                curr = story.Length;
            }
            watch.Stop();
            
            // Print out the length of the text, the overall time it took, and the generated story
            Console.WriteLine($"Text Length: {text.Length} characters");
            Console.WriteLine($"Time taken: {watch.ElapsedMilliseconds} ms");
            Console.WriteLine(story);
        }
    }
}