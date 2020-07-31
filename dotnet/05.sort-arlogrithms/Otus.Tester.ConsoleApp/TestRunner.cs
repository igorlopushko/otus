using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
            Console.ResetColor();

            var count = 0;

            while (true)
            {
                var inputFile = $"{_path}/test.{count}.in";
                var outputFile = $"{_path}/test.{count}.out";
                var timer = new Stopwatch();

                if (!File.Exists(inputFile) || !File.Exists(outputFile))
                {
                    break;
                }

                timer.Start();

                var isStopped = false;
                var tokenSource = new CancellationTokenSource();
                var testRunner = Task.Run(() => ExecuteTest(inputFile, outputFile), tokenSource.Token);

                while (!testRunner.IsCompleted)
                {
                    if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)
                    {
                        tokenSource.Cancel();
                        PrintCancellationMessage(count, timer);
                        isStopped = true;
                        break;
                    }

                    PrintProgressMessage(count, timer);
                }

                timer.Stop();

                if (!isStopped)
                {
                    PrintTestResult(count, testRunner.Result, timer.Elapsed);
                }

                count++;
            }
        }

        private Tuple<bool, string[], string[]> ExecuteTest(string inputFile, string outputFile)
        {
            var data = File.ReadAllLines(inputFile);
            var expected = File.ReadAllLines(outputFile);
            var actual = _task.Run(data);

            if (expected.Length != actual.Length)
            {
                return new Tuple<bool, string[], string[]>(false, expected, actual);
            }

            // check that all results are correct
            var isEqual = !expected
                .Where((t, i) => !actual[i].Equals(t, StringComparison.InvariantCultureIgnoreCase))
                .Any();

            return new Tuple<bool, string[], string[]>(isEqual, expected, actual);
        }

        private static void PrintProgressMessage(int testNumber, Stopwatch timer)
        {
            Console.Write($"Test #{testNumber} - ");
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("RUNNING");
            Console.ResetColor();
            Console.Write("\tElapsed = {0}", timer.Elapsed.ToString());
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\tpress ESC to cancel.");
            Console.ResetColor();
            Console.Write('\r');
        }

        private static void PrintCancellationMessage(int testNumber, Stopwatch timer)
        {
            ClearConsoleLine();
            Console.Write($"Test #{testNumber} - ");
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("CANCELLED");
            Console.ResetColor();
            Console.Write("\tElapsed = {0}", timer.Elapsed.ToString());
            Console.WriteLine();
        }

        private static void PrintTestResult(
            int testNumber, 
            Tuple<bool, string[], string[]> result, 
            TimeSpan elapsedTime)
        {
            ClearConsoleLine();
            Console.Write($"Test #{testNumber} - ");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = result.Item1 ? ConsoleColor.Green : ConsoleColor.Red;
            Console.Write("{0}", result.Item1 ? "PASSED" : "FAILED");
            Console.ResetColor();
            Console.WriteLine("\tElapsed = {0}", elapsedTime);
        }

        private static void ClearConsoleLine()
        {
            var currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}