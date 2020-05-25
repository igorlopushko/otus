using Otus.Tester.ConsoleApp.Base;

namespace Otus.Tester.ConsoleApp.Tasks
{
    public class FiboRecursionTask : ITask
    {
        public string Run(string[] data)
        {
            int number = int.Parse(data[0]);

            if(number == 0 || number == 1)
            {
                return number.ToString();
            }

            return Fibonacci(0, 1, 1, number).ToString();
        }

        private int Fibonacci(int a, int b, int counter, int length)
        {
            if (counter <= length)
            {
                return Fibonacci(b, a + b, counter + 1, length);
            }
            return a;
        }
    }
}
