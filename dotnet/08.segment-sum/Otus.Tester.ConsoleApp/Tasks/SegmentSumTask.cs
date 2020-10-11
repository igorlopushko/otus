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

            var ss = new SegmentSum(arrayLength);
            
            for (var i = 1; i <= operationNumber; i++)
            {
                var operationValues = data[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                var operationType = operationValues[0];

                if (operationType.Equals("A", StringComparison.InvariantCultureIgnoreCase))
                {
                    var index = int.Parse(operationValues[1]);
                    var value = int.Parse(operationValues[2]);
                    
                    ss.Add(index, value);
                } else if (operationType.Equals("Q", StringComparison.InvariantCultureIgnoreCase))
                {
                    var startIndex = int.Parse(operationValues[1]);
                    var endIndex = int.Parse(operationValues[2]);
                    
                    var sum = ss.Calculate(startIndex, endIndex);
                    
                    result.Add(sum);
                }
            }

            return result.Select(x => x.ToString()).ToArray();
        }

        private class SegmentSum
        {
            private readonly int[] _halfSumArray;

            public SegmentSum(int arrayLength)
            {
                // length of the initial array in power of 2 => length of the internal (half sum) array 
                var n = Convert.ToInt32(Math.Pow(2, Math.Ceiling(Math.Log(arrayLength, 2))));

                _halfSumArray = new int[2 * n];
            }

            public void Add(int index, int value)
            {
                var internalIndex = index + _halfSumArray.Length / 2;
                _halfSumArray[internalIndex] = value;

                // init indexes to calculate half sums up
                var leftIndex = internalIndex % 2 == 0 ? internalIndex : internalIndex - 1;
                var rightIndex = internalIndex % 2 == 1 ? internalIndex : internalIndex + 1;
                var parentIndex = leftIndex / 2;

                // recalculate half sums up
                while (parentIndex > 0)
                {
                    _halfSumArray[parentIndex] = _halfSumArray[leftIndex] + _halfSumArray[rightIndex];

                    leftIndex = leftIndex / 2 % 2 == 0 ? leftIndex / 2 : leftIndex / 2 - 1;
                    rightIndex = rightIndex / 2 % 2 == 1 ? rightIndex / 2 : rightIndex / 2 + 1;
                    parentIndex = leftIndex / 2;
                }
            }

            public int Calculate(int leftIndex, int rightIndex)
            {
                var sum = 0;

                var leftIndexOffset = leftIndex + _halfSumArray.Length / 2;
                var rightIndexOffset = rightIndex + _halfSumArray.Length / 2;

                while (leftIndexOffset <= rightIndexOffset)
                {
                    if (leftIndexOffset % 2 == 1)
                    {
                        sum += _halfSumArray[leftIndexOffset];
                    }

                    if (rightIndexOffset % 2 == 0)
                    {
                        sum += _halfSumArray[rightIndexOffset];
                    }

                    leftIndexOffset = (leftIndexOffset + 1) / 2;
                    rightIndexOffset = (rightIndexOffset - 1) / 2;
                }

                return sum;
            }
        }
    }
}