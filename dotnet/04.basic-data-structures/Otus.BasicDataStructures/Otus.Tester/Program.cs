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
            IArray<int> space = new SpaceArray<int>();;
            
            TestPut(single, 1000000);
            TestPut(vector, 1000000);
            TestPut(factor, 1000000);
            TestPut(matrix, 1000000);
            TestPut(space,  1000000);

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
