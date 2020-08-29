using System.Linq;
using Otus.Tester.ConsoleApp.Base;

namespace Otus.Tester.ConsoleApp.Tasks
{
    public class QuickSortTask : ITask
    {
        public string[] Run(string[] data)
        {
            var inputString = data[1].Split(" ");
            var inputArray = new int[inputString.Length];

            for (int i = 0; i < inputString.Length; i++)
            {
                inputArray[i] = int.Parse(inputString[i]);
            }

            var result = inputArray;

            if (inputArray.Length > 1)
            {
                result = Sort(inputArray, 0, inputArray.Length - 1);
            }

            return new[]
            {
                string.Join(" ", result.Select(x => x.ToString()).ToArray())
            };
        }

        private int[] Sort(int[] array, int left, int right)
        {
            if (left >= right)
            {
                return array;
            }

            int leftStartIndex = Partition(array, left, right);
            array = Sort(array, left, leftStartIndex - 1);
            array = Sort(array, leftStartIndex + 1, right);
            return array;
        }
        
        private int Partition(int[] array, int left, int right)
        {
            var pivot = array[right];
            var leftStartIndex = left - 1;
            for (var currentIndex = left; currentIndex <= right; currentIndex++)
            {
                if (array[currentIndex] <= pivot)
                {
                    leftStartIndex++;
                    array = Swap(array, leftStartIndex, currentIndex);
                }
            }

            return leftStartIndex;
        }

        private static int[] Swap(int[] array, int a, int b)
        {
            var temp = array[a];
            array[a] = array[b];
            array[b] = temp;

            return array;
        }
    }
}