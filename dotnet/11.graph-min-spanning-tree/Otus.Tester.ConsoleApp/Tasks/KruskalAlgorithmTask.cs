using System;
using System.Collections.Generic;
using Otus.DataStructure;
using Otus.Tester.ConsoleApp.Base;

namespace Otus.Tester.ConsoleApp.Tasks
{
    public class KruskalAlgorithmTask : ITask
    {
        public string[] Run(string[] data)
        {
            int[][] adjacencyVector = null;
            var verticesCount = 0;
            var adjacencyVectorCount = 0;

            var g = new Graph();
            
            for (var i = 0; i < data.Length; i++)
            {
                var values = data[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if (i == 0)
                {
                    // get number of vertices
                    verticesCount = int.Parse(values[0]);
                    adjacencyVector = new int[verticesCount][];
                    continue;
                }

                if (i <= verticesCount && values.Length > 1)
                {
                    // build adjacency vector
                    var list = new List<int>();
                    for (int j = 1; j < values.Length; j++)
                    {
                        list.Add(int.Parse(values[j]) - 1);
                    }
                    
                    adjacencyVector[adjacencyVectorCount] = list.ToArray();
                    adjacencyVectorCount++;
                }

                if (i > verticesCount && values.Length == 3)
                {
                    g.AddEdge(int.Parse(values[0]) - 1,int.Parse(values[1]) - 1,int.Parse(values[2]));
                }
            }

            var result = g.GetMinSpanningTreeKruskal();
            
            var stringResult = new List<string>();
            foreach (var edge in result)
            {
                stringResult.Add($"{edge.Source + 1} {edge.Destination + 1}");
            }
            
            return stringResult.ToArray();
        }
    }
}