using System;
using System.Diagnostics;
using System.IO;
using Otus.Tester.ConsoleApp.Base;

namespace Otus.Tester.ConsoleApp
{
    public class TestRunner
    {
        private ITask _task;
        private string _path;

        public TestRunner(ITask task, string path)
        {
            _task = task;
            _path = path;
        }

        public void Run()
        {
            int count = 0;
            while(true)
            {
                string inputFile = $"{_path}/test.{count}.in";
                string outputFile = $"{_path}/test.{count}.out";
                Stopwatch sw = new Stopwatch();

                if (!File.Exists(inputFile) || !File.Exists(outputFile))
                {
                    break;
                }

                sw.Start();
                var result = ExecuteTest(inputFile, outputFile);
                sw.Stop();
                
                PrintTestResult(count, result, sw.Elapsed);

                count++;
            }

            Console.WriteLine("Completed. Press ENTER to exit.");
        }

        private void PrintTestResult(int testNumber, Tuple<bool, string, string> result, TimeSpan elapsedTime)
        {
            Console.Write($"Test #{testNumber} - ");
            Console.ForegroundColor = ConsoleColor.Black;
            if (result.Item1)
            {
                Console.BackgroundColor = ConsoleColor.Green;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Red;
            }
            Console.Write("{0}", result.Item1 ? "PASSED" : "FAILED");
            Console.ResetColor();

            Console.WriteLine("\tElapsed = {0}", elapsedTime);

            //if(!result.Item1)
            //{
            //    Console.WriteLine($"expected: {result.Item2}; actual: {result.Item3}");
            //}
        }

        private Tuple<bool, string, string> ExecuteTest(string inputFile, string outputFile)
        {
            string[] data = File.ReadAllLines(inputFile);
            string expected = File.ReadAllText(outputFile).Trim();
            string actual = _task.Run(data);
            var result = actual.Equals(expected, StringComparison.InvariantCultureIgnoreCase);
            return new Tuple<bool, string, string>(result, expected, actual);
        }
    }
}
