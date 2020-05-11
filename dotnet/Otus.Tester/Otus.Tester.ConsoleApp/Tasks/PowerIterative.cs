using System;
using Otus.Tester.ConsoleApp.Base;

namespace Otus.Tester.ConsoleApp.Tasks
{
    public class PowerIterative : ITask
    {
        public string Run(string[] data)
        {
            long l_number, l_power;
            if (long.TryParse(data[0], out l_number) && long.TryParse(data[1], out l_power))
            {
                return Power(l_number, l_power).ToString("0.0");
            }

            double d_number, d_power;
            if (double.TryParse(data[0], out d_number) && double.TryParse(data[1], out d_power))
            {
                return Math.Round(Math.Pow(d_number, d_power), 11).ToString();
            }

            return "-1";
        }

        private long Power(long number, long power)
        {
            if (number == 0)
            {
                return 1;
            }

            long result = 1;

            for (var i = 0; i < Math.Abs(power); i++)
            {
                result *= number;
            }

            return result;
        }
    }
}
