using System.Collections.Generic;
using System.Linq;
using Otus.Tester.ConsoleApp.Base;

namespace Otus.Tester.ConsoleApp.Tasks
{
    public class BucketSortTask : ITask
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
                result = Sort(inputArray);
            }

            return new[]
            {
                string.Join(" ", result.Select(x => x.ToString()).ToArray())
            };
        }
        
        private int[] Sort(int[] array)
        {
            var minValue = array[0];
            var maxValue = array[0];

            for (var i = 1; i < array.Length; i++)
            {
                if (array[i] > maxValue)
                {
                    maxValue = array[i];
                }

                if (array[i] < minValue)
                {
                    minValue = array[i];
                }
            }

            var bucketSet = new List<int>[maxValue - minValue + 1];

            for (var i = 0; i < bucketSet.Length; i++)
            {
                bucketSet[i] = new List<int>();
            }

            foreach (var item in array)
            {
                bucketSet[item - minValue].Add(item);
            }
	
            var count = 0;
            foreach (var bucket in bucketSet)
            {
                if (bucket.Count > 0)
                {
                    foreach (var item in bucket)
                    {
                        array[count] = item;
                        count++;
                    }
                }
            }

            return array;
        }
    }
}