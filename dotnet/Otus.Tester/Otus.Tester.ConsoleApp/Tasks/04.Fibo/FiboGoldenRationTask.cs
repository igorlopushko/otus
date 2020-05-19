using System;
using Otus.Tester.ConsoleApp.Base;

namespace Otus.Tester.ConsoleApp.Tasks
{
    public class FiboGoldenRationTask : ITask
    {
        private const double PhiValue = 1.6180339;
        private readonly int[] numbers = { 0, 1, 1, 2, 3, 5 };

        public string Run(string[] data)
        {
            int number = int.Parse(data[0]);
            if(number < numbers.Length)
            {
                return numbers[number].ToString();
            }

            long result = numbers[5];
            var count = numbers.Length - 1;
            while(count < number)
            {
                result = (long)Math.Round(PhiValue * result);
                count++;
            }

            return result.ToString();
        }
    }
}
