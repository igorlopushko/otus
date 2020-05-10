using System;
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

                if(!File.Exists(inputFile) || !File.Exists(outputFile))
                {
                    break;
                }

                Console.WriteLine($"Test #{count} - {ExecuteTest(inputFile, outputFile)}");
                count++;
            }
        }

        private bool ExecuteTest(string inputFile, string outputFile)
        {
            try
            {
                string[] data = File.ReadAllLines(inputFile);
                string expected = File.ReadAllText(outputFile).Trim();
                string actual = _task.Run(data);
                return actual.Equals(expected, StringComparison.InvariantCultureIgnoreCase);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
