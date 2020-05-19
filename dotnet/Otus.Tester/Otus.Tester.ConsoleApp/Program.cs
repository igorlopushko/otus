using Otus.Tester.ConsoleApp.Tasks;
using Otus.Tester.ConsoleApp.Base;
using System;

namespace Otus.Tester.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = PrintMenu();
            ITask task = null;
            string path = string.Empty;

            switch (result)
            {
                case 0:
                    task = new StringLengthTask();
                    path = $"data/00.String/";
                    break;
                case 1:
                    task = new LuckyTicketsTask();
                    path = $"data/01.Tickets/";
                    break;
                case 2:
                    task = new EuclidGcdMinus();
                    path = $"data/02.GCD/";
                    break;
                case 22:
                    task = new EuclidGcdMod();
                    path = $"data/02.GCD/";
                    break;
                case 222:
                    task = new EuclidGcdBitShift();
                    path = $"data/02.GCD/";
                    break;
                case 3:
                    task = new PowerIterative();
                    path = $"data/03.Power/";
                    break;
                case 33:
                    task = new PowerBinary();
                    path = $"data/03.Power/";
                    break;
                case 4:
                    task = new FiboRecursionTask();
                    path = $"data/04.Fibo/";
                    break;
                case 44:
                    task = new FiboLoopTask();
                    path = $"data/04.Fibo/";
                    break;
                case 444:
                    task = new FiboGoldenRationTask();
                    path = $"data/04.Fibo/";
                    break;
                case 4444:
                    task = new FiboMatrixTask();
                    path = $"data/04.Fibo/";
                    break;
                case 5:
                    task = new IsPrimeIterativeTask();
                    path = $"data/05.Primes/";
                    break;
                case 55:
                    task = new IsPrimeIterativeV2Task();
                    path = $"data/05.Primes/";
                    break;
                case 555:
                    task = new IsPrimeIterativeV3Task();
                    path = $"data/05.Primes/";
                    break;
                case 5555:
                    task = new IsPrimeIterativeHashSetTask();
                    path = $"data/05.Primes/";
                    break;
                default:
                    return;
            }

            var t = new TestRunner(task, path);
            t.Run();
            Console.ReadLine();
        }

        private static int PrintMenu()
        {
            int rightPadding = 4;
            while (true)
            {
                Console.WriteLine("Выберите номер задания/решения:");
                Console.WriteLine("{0} - Вычислить длину строки", 0.ToString().PadRight(rightPadding));

                Console.WriteLine("{0} - Вычислить кол-во счастливых билетов", 1.ToString().PadRight(rightPadding));

                Console.WriteLine("{0} - Вычислить НОД - Алгоритм Евклида через вычитание", 2.ToString().PadRight(rightPadding));
                Console.WriteLine("{0} - Вычислить НОД - Алгоритм Евклида через остаток", 22.ToString().PadRight(rightPadding));
                Console.WriteLine("{0} - Вычислить НОД - Алгоритм Стейнца через битовые операции", 222.ToString().PadRight(rightPadding));

                Console.WriteLine("{0} - Вычислить A^N - Через обычные итерации", 3.ToString().PadRight(rightPadding));
                Console.WriteLine("{0} - Вычислить A^N - Через двоичное разложение показателя степени", 33.ToString().PadRight(rightPadding));

                Console.WriteLine("{0} - Найти точное значение N-ого числа Фибоначчи. - Через рекурсию", 4.ToString().PadRight(rightPadding));
                Console.WriteLine("{0} - Найти точное значение N-ого числа Фибоначчи. - Через итерацию", 44.ToString().PadRight(rightPadding));
                Console.WriteLine("{0} - Найти точное значение N-ого числа Фибоначчи. - Через формулу золотого сечения", 444.ToString().PadRight(rightPadding));
                Console.WriteLine("{0} - Найти точное значение N-ого числа Фибоначчи. - Через возведение матрицы в степень", 4444.ToString().PadRight(rightPadding));

                Console.WriteLine("{0} - Найти количество простых чисел от 1 до N. - Через перебор делителей", 5.ToString().PadRight(rightPadding));
                Console.WriteLine("{0} - Найти количество простых чисел от 1 до N. - Через перебор делителей. Оптимизация №1", 55.ToString().PadRight(rightPadding));
                Console.WriteLine("{0} - Найти количество простых чисел от 1 до N. - Через перебор делителей. Оптимизация №2", 555.ToString().PadRight(rightPadding));
                Console.WriteLine("{0} - Найти количество простых чисел от 1 до N. - Через перебор делителей. Оптимизация №3. Использование хеш таблицы", 5555.ToString().PadRight(rightPadding));

                int result;
                var answer = Console.ReadLine();
                if (int.TryParse(answer, out result))
                {
                    return result;
                }
            }
        }
    }
}
