using System;
using Otus.Tester.ConsoleApp.Base;

namespace Otus.Tester.ConsoleApp.Tasks
{
    public class FiboLoopTask : ITask
    {
        public string Run(string[] data)
        {
            int number = int.Parse(data[0]);
            long result = 0;
            long a = 1;
            long b = 1;

            if (number == 1 || number == 2)
            {
                return 1.ToString();
            }

            for (int i = 2; i < number; i++)
            {
                long temp = b;
                result = a + b;
                a = temp;
                b = result;
            }

            return result.ToString();
        }
    }
}
