using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleAppCountWords
{
    
    /// <summary>
    /// class that read words and process them
    /// </summary>
    class WordClass
    {
        public List<string> wordList { get; private set; }
        public Dictionary<string, string> wordDictionary { get; private set; }
        public WordClass()
        {
            wordList = new List<string>();
            wordDictionary = new Dictionary<string, string>();
        }
        /// <summary>
        /// 
        /// read file line by line and put in the list of stings
        /// </summary>
        /// <param name="filePath"></param>
        public void ReadFile(string filePath)
        {
            using (FileStream fs = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                StreamReader sr = new StreamReader(fs);
                string line = null;
                while ((line = sr.ReadLine()) != null)
                {
                    wordList.Add(line);
                }

                sr.Close();
                fs.Close();

                Console.WriteLine("Read {0} words from file {1}",wordList.Count, filePath);
            }
        }
        /// <summary>
        /// get number of words from a file
        /// </summary>
        /// <returns></returns>
        public int GetWordCount()
        {
            return wordList.Count;
        }
        /// <summary>
        /// get number of distinct words
        /// </summary>
        /// <returns></returns>
        public int GetDistinctWordCount()
        {
            return wordList.Distinct().Count();
        }
        /// <summary>
        /// find if a given word existis in the list of words
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public bool FindWord(string word)
        {
            return wordList.Contains(word);
        }
        /// <summary>
        /// group words by length
        /// </summary>
        public void GroupWords()
        {
            var x = wordList.GroupBy(w =>
            {
                if (w.Length >0 && w.Length <= 5) return "xs"; if (w.Length > 5 && w.Length <= 10) return "s";
                if (w.Length > 10 && w.Length <= 15) return "m"; return "l";
            });
        }

        
    }
}
