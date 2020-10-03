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
            
            // adjust to start vertices from 1
            for (var i = 0; i < result.GetLength(0); i++)
            {
                for (var j = 0; j < result[i].Length; j++)
                {
                    result[i][j] += 1;
                }
            }
            
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
            var result = Calculate(adjacencyMatrix);

            return result;
        }

        private int[][] Calculate(int[,] adjacencyMatrix)
        {
            var previousLayerArray = new int[0];
            var previousSumArray = new int[adjacencyMatrix.GetLength(1)];
            var processedVertices = new HashSet<int>();
            var result = new int[0][];
            
            while (processedVertices.Count < adjacencyMatrix.GetLength(0))
            {
                var currentSumArray = new int[adjacencyMatrix.GetLength(0)];
                
                for (var col = 0; col < adjacencyMatrix.GetLength(1); col++)
                {
                    var layerIndex = 0;
                    for (var row = 0; row < adjacencyMatrix.GetLength(0); row++)
                    {
                        // if layers are present and layer in array skip
                        if (previousLayerArray.Length > 0 && previousLayerArray[layerIndex] != row) continue;
    
                        currentSumArray[col] += adjacencyMatrix[row, col];
    
                        // if layers are present and all layers are processed quit
                        if (previousLayerArray.Length > 0 && layerIndex + 1 >= previousLayerArray.Length) break;
    
                        layerIndex++;
                    }
                }
    
                // calculate = previous sum - current sum
                for (var i = 0; i < currentSumArray.Length; i++)
                {
                    currentSumArray[i] = Math.Abs(previousSumArray[i] - currentSumArray[i]);
                }
                
                // find zero sums to find vertices layers
                var layerArray = new List<int>();
                for (var i = 0; i < currentSumArray.Length; i++)
                {
                    if (processedVertices.Contains(i) || currentSumArray[i] != 0)
                    {
                        continue;
                    }

                    layerArray.Add(i);
                    processedVertices.Add(i);
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

                previousSumArray = currentSumArray;
                previousLayerArray = layerArray.ToArray();
            }
            
            return result;
        }
    }
}