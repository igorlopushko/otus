using System;
using System.Collections.Generic;
using System.Linq;
using Otus.Tester.ConsoleApp.Base;

namespace Otus.Tester.ConsoleApp.Tasks
{
    public class SegmentSumTask : ITask
    {
        public string[] Run(string[] data)
        {
            if (data.Length == 0)
            {
                return new string[0];
            }

            var result = new List<int>();
            
            var values = data[0].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var arrayLength = int.Parse(values[0]);
            var operationNumber = int.Parse(values[1]);

            var array = new int[arrayLength];
            
            for (var i = 1; i <= operationNumber; i++)
            {
                var operationValues = data[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                var operationType = operationValues[0];

                if (operationType.Equals("A", StringComparison.InvariantCultureIgnoreCase))
                {
                    var index = int.Parse(operationValues[1]);
                    var value = int.Parse(operationValues[2]);
                    array[index] = value;
                } else if (operationType.Equals("Q", StringComparison.InvariantCultureIgnoreCase))
                {
                    var startIndex = int.Parse(operationValues[1]);
                    var endIndex = int.Parse(operationValues[2]);
                    var sum = CalculateSum(array, startIndex, endIndex);
                    result.Add(sum);
                }
            }

            return result.Select(x => x.ToString()).ToArray();
        }

        private int CalculateSum(int[] array, int leftIndex, int rightIndex)
        {
            var sum = 0;
            // length of the initial array
            var k = Convert.ToDouble(array.Length);
            // length of the initial array in power of 2
            var n = Convert.ToInt32(Math.Pow(2, Math.Ceiling(Math.Log(k, 2))));
            var internalArray = new int[2 * n];

            // copy existing array
            var initialArrayIndex = 0;
            for (var i = n; i < internalArray.Length; i++)
            {
                if (initialArrayIndex >= array.Length)
                {
                    break;
                }
                internalArray[i] = array[initialArrayIndex];
                initialArrayIndex++;
            }
            
            // calculate half sums
            for (var i = n - 1; i >= 1; i--)
            {
                internalArray[i] = internalArray[2 * i] + internalArray[2 * i + 1];
            }

            var leftIndexOffset = leftIndex + n;
            var rightIndexOffset = rightIndex + n;

            while (leftIndexOffset <= rightIndexOffset)
            {
                if (leftIndexOffset % 2 == 1)
                {
                    sum += internalArray[leftIndexOffset];
                }

                if (rightIndexOffset % 2 == 0)
                {
                    sum += internalArray[rightIndexOffset];
                }

                leftIndexOffset = (leftIndexOffset + 1) / 2;
                rightIndexOffset = (rightIndexOffset - 1) / 2;
            }
            
            return sum;
        }
    }
}