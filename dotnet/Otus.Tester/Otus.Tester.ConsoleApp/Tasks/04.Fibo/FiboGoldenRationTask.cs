using System;
using Otus.Tester.ConsoleApp.Base;

namespace Otus.Tester.ConsoleApp.Tasks
{
    public class FiboGoldenRationTask : ITask
    {
        public string Run(string[] data)
        {
            int number = int.Parse(data[0]);
            if(number == 0)
            {
                return number.ToString();
            }

            double phi = (1 + Math.Sqrt(5)) / 2;
            var result = (int)Math.Round(Math.Pow(phi, number) / Math.Sqrt(5));

            return result.ToString();
        }
    }
}
