using System.Collections.Generic;

namespace Otus.DataStructure
{
    public class MergeSort
    {
        public static Edge[] Sort(Edge[] array)
        {
            void merge(int left, int center, int right)
            {
                var result = new Edge[right - left + 1];
                var leftIndex = left;
                var rightIndex = center + 1;
                var resultIndex = 0;

                while (leftIndex <= center && rightIndex <= right)
                {
                    if (array[leftIndex].Rank < array[rightIndex].Rank)
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