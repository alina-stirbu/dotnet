using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleAppCountWords
{
    class Program
    {
        private const string Path =
                "C:\\Users\\Alina\\source\\repos\\ConsoleAppCountWords\\files";
        static void Main(string[] args)
        {
            var stopwatch = Stopwatch.StartNew();
            List<WordClass> wList = new List<WordClass>();
            List<Task> tasks = new List<Task>();

            try
            {
                //get the number of files from path
                string[] fileEntries = Directory.GetFiles(Path, "*", SearchOption.TopDirectoryOnly);
                if (fileEntries.Length > 0)
                {
                    foreach (string fileName in fileEntries)
                    {
                        var word = new WordClass();
                        wList.Add(word);
                        Action someAction = () => { word.ReadFile(fileName); };
                        var t = new Task(someAction);
                        tasks.Add(t);
                        t.Start();
                    }

                    for (int i= 0;i< tasks.Count;i++)
                    {
                        tasks[i].Wait();
                        wList[i].GetDistinctWordCount();
                        wList[i].GroupWords();

                    }
                    stopwatch.Stop();

                    Console.WriteLine($"Elapsed time: {stopwatch.Elapsed.TotalMilliseconds} ms");
                    
                }
            }
            catch (Exception e)
            {

            }
            
        }
    }
}