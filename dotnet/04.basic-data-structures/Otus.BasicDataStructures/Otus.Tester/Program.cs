using System;
using System.Diagnostics;
using Otus.DataStructures;

namespace Otus.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            IArray<int> single = new SingleArray<int>();
            IArray<int> vector = new VectorArray<int>(1000);
            IArray<int> factor = new FactorArray<int>();
            IArray<int> matrix = new MatrixArray<int>();
            TestPut(single, 100);
            TestPut(vector, 100);
            TestPut(factor, 100);
            TestPut(matrix, 100);

            single.Remove(10);
            vector.Remove(10);
            factor.Remove(10);
            matrix.Add(999, 10);
            matrix.Add(999, 99);
            matrix.Remove(101);
            matrix.Remove(100);
            matrix.Remove(10);

            Console.ReadLine();
        }

        private static void TestPut(IArray<int> array, int total)
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();

            for (int i = 0; i < total; i++)
            {
                array.Add(i);
            }

            sw.Stop();

            Console.WriteLine(array + " Test Put: " + sw.ElapsedMilliseconds + " (ms)");
        }
    }
}
