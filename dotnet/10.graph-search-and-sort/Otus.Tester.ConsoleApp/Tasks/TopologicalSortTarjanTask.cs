using System;
using System.Collections.Generic;
using System.Text;
using Otus.Tester.ConsoleApp.Base;

namespace Otus.Tester.ConsoleApp.Tasks
{
    public class TopologicalSortTarjanTask : ITask
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

            var result = Tarjan(matrix);
            var stringResult = new List<string>();

            var str = new StringBuilder();

            while (result.Size > 0)
            {
                str.Append(result.Pop() + 1);
                if (result.Size > 0)
                {
                    str.Append(" ");
                }
            }
            stringResult.Add(str.ToString());
            
            return stringResult.ToArray();
        }

        private Otus.DataStructure.Stack<int> Tarjan(int[,] adjacencyMatrix)
        {
            var stack = new Otus.DataStructure.Stack<int>();
            var visited = new HashSet<int>();
            
            void dfs(int vertex)
            {
                if (visited.Contains(vertex)) return;

                // get child vertices
                var childVertices = new List<int>();
                for (var col = 0; col < adjacencyMatrix.GetLength(1); col++)
                {
                    if (adjacencyMatrix[vertex, col] == 1 && !visited.Contains(adjacencyMatrix[vertex, col]))
                    {
                        childVertices.Add(col);
                    }
                }

                // go down to children
                foreach (var child in childVertices)
                {
                    dfs(child);
                }
                // mark as visited and add to result
                visited.Add(vertex);
                stack.Push(vertex);
            }

            while (visited.Count < adjacencyMatrix.GetLength(0))
            {
                for (var vertex = 0; vertex < adjacencyMatrix.GetLength(0); vertex++)
                {
                    if (!visited.Contains(vertex))
                    {
                        dfs(vertex);
                    }
                }
            }
            
            return stack;
        }
    }
}