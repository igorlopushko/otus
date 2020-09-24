using System;
using System.Collections.Generic;
using System.Text;
using Otus.Tester.ConsoleApp.Base;

namespace Otus.Tester.ConsoleApp.Tasks
{
    public class TopologicalSortDemukronTask : ITask
    {
        public string[] Run(string[] data)
        {
            var matrix = new int[data.Length, data.Length];

            for (var i = 0; i < data.Length; i++)
            {
                var values = data[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                var valuesCount = values.Length > 1 ? 1 : 0;

                for (var j = 0; j < data.Length; j++)
                    if (values.Length > 1 &&
                        valuesCount < values.Length &&
                        int.Parse(values[valuesCount]) - 1 == j)
                    {
                        matrix[i, j] = 1;
                        valuesCount++;
                    }
                    else
                    {
                        matrix[i, j] = 0;
                    }
            }

            var result = Demukron(matrix);
            var stringResult = new List<string>();

            for (var i = 0; i < result.GetLength(0); i++)
            {
                var str = new StringBuilder();
                for (var j = 0; j < result[i].Length; j++)
                {
                    str.Append(result[i][j]);
                    if (j < result[i].Length - 1)
                    {
                        str.Append(" ");
                    }
                }
                stringResult.Add(str.ToString());
            }
            
            return stringResult.ToArray();
        }

        private int[][] Demukron(int[,] adjacencyMatrix)
        {
            var result = Calculate(
                adjacencyMatrix, 
                new int[0], 
                new int[adjacencyMatrix.GetLength(1)], 
                new int[0][],
                new HashSet<int>());

            for (var i = 0; i < result.GetLength(0); i++)
            {
                for (var j = 0; j < result[i].Length; j++)
                {
                    result[i][j] += 1;
                }
            }
            
            return result;
        }

        private int[][] Calculate(
            int[,] adjacencyMatrix, 
            int[] previousLayerArray, 
            int[] previousSumArray, 
            int[][] result,
            HashSet<int> vertices)
        {
            var sumArray = new int[adjacencyMatrix.GetLength(0)];

            for (var col = 0; col < adjacencyMatrix.GetLength(1); col++)
            {
                var layerIndex = 0;
                for (var row = 0; row < adjacencyMatrix.GetLength(0); row++)
                {
                    // if layers are present and layer in array skip
                    if (previousLayerArray.Length > 0 && previousLayerArray[layerIndex] != row) continue;

                    sumArray[col] += adjacencyMatrix[row, col];

                    // if layers are present and all layers are processed quit
                    if (previousLayerArray.Length > 0 && layerIndex + 1 >= previousLayerArray.Length) break;

                    layerIndex++;
                }
            }

            // calculate = previous sum - current sum
            for (var i = 0; i < sumArray.Length; i++)
            {
                sumArray[i] = Math.Abs(previousSumArray[i] - sumArray[i]);
            }
            
            // find zero sums to find vertices layers
            var layerArray = new List<int>();
            for (var i = 0; i < sumArray.Length; i++)
            {
                if(vertices.Contains(i)) continue;
                if(sumArray[i] == 0) layerArray.Add(i);
            }

            // build result array
            if (result.GetLength(0) == 0)
            {
                //create new
                result = new int[1][];
                result[0] = layerArray.ToArray();
            }
            else
            {
                // copy
                var temp = new int[result.GetLength(0) + 1][];
                for (var i = 0; i < result.GetLength(0); i++)
                {
                    temp[i] = result[i];
                }

                temp[result.GetLength(0)] = layerArray.ToArray();

                result = temp;
            }
            
            // add layer vertices to the list of processed
            foreach (var vertix in layerArray)
            {
                vertices.Add(vertix);
            }

            // if all vertices are processed return result
            if (vertices.Count == adjacencyMatrix.GetLength(0))
            {
                return result;
            }

            return Calculate(adjacencyMatrix, layerArray.ToArray(), sumArray, result, vertices);
        }
    }
}