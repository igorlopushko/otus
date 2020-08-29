using System.Linq;
using Otus.Tester.ConsoleApp.Base;

namespace Otus.Tester.ConsoleApp.Tasks
{
    public class MergeSortTask : ITask
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
                result = RunSort(inputArray);
            }

            return new[]
            {
                string.Join(" ", result.Select(x => x.ToString()).ToArray())
            };
        }

        private int[] RunSort(int[] array)
        {
            void merge(int left, int center, int right)
            {
                var result = new int[right - left + 1];
                var leftIndex = left;
                var rightIndex = center + 1;
                var resultIndex = 0;

                while (leftIndex <= center && rightIndex <= right)
                {
                    if (array[leftIndex] < array[rightIndex])
                    {
                        result[resultIndex++] = array[leftIndex++];
                    }
                    else
                    {
                        result[resultIndex++] = array[rightIndex++];
                    }
                }

                while (leftIndex <= center)
                {
                    result[resultIndex++] = array[leftIndex++];
                }

                while (rightIndex <= right)
                {
                    result[resultIndex++] = array[rightIndex++];
                }

                for (var i = left; i <= right; i++)
                {
                    array[i] = result[i - left];
                }
            }
            
            void sort(int left, int right)
            {
                if (left >= right) return;
                var center = (left + right) / 2;
                sort(left, center);
                sort(center + 1, right);
                merge(left, center, right);
            }
            
            sort(0, array.Length - 1);

            return array;
        }
    }
}