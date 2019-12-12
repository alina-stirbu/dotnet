using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;
using System.IO;

namespace ConsoleAppFileProcessor
{
    class Program
    {
        private const string Path =
                "C:\\Users\\Alina\\source\\repos\\ConsoleAppFileProcessor\\files";
        private static SemaphoreSlim semaphore;
        static BlockingCollection<string> collection = new BlockingCollection<string>();

        static void Main(string[] args)
        {
            var stopwatch = Stopwatch.StartNew();
            semaphore = new SemaphoreSlim(0, 4);
            Task[] producersTasks = new Task[10];
            int numberOfFilesProcessed = 0;
                
            string[] fileEntries = Directory.GetFiles(Path, "*", SearchOption.TopDirectoryOnly);

            if (fileEntries.Length > 0)
            {
                foreach (string fileName in fileEntries)
                {
                    if (numberOfFilesProcessed < 10)
                    {
                        producersTasks[numberOfFilesProcessed] = Task.Run(() =>
                        {
                            // Each task begins by requesting the semaphore.
                            Console.WriteLine("Task {0} begins and waits for the semaphore.", Task.CurrentId);
                            semaphore.Wait();
                            Console.WriteLine("Task {0} enters the semaphore.", Task.CurrentId);
                            //add in the colection the file name and the content of file
                            collection.Add(fileName);
                            ReadFile(fileName);
                            Console.WriteLine("Task {0} releases the semaphore; previous count: {1}.", Task.CurrentId, semaphore.Release());
                        });
                    }
                    numberOfFilesProcessed++;
                }
            }
            var consumerTask = Task.Run(() =>
            {
                while (!collection.IsCompleted)
                {
                    string item = collection.Take();
                    Console.WriteLine(item);
                }
            });

            // Restore the semaphore count to its maximum value.
            Console.Write("Main thread calls Release(4) --> ");
            semaphore.Release(4);
            Console.WriteLine("{0} tasks can enter the semaphore.",semaphore.CurrentCount);
            // Main thread waits for the tasks to complete.
            Task.WaitAll(producersTasks);
            collection.CompleteAdding();
            consumerTask.Wait();

            Console.WriteLine("Main thread exits.");
        }

        public static void ReadFile(string filePath)
        {
            using (FileStream fs = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                StreamReader sr = new StreamReader(fs);
                string line = null;
                while ((line = sr.ReadLine()) != null)
                {
                    collection.Add(line);
                }

                sr.Close();
                fs.Close();

                Console.WriteLine("Read {0} words from file {1}", collection.Count, filePath);
            }
        }
    }
}
