using System;
using System.Collections.Generic;
using Otus.Tester.ConsoleApp.Base;

namespace Otus.Tester.ConsoleApp.Tasks
{
    public class GraphFindBridgesTask : ITask
    {
        private int _time = 0;
        
        public string[] Run(string[] data)
        {
            var adjacencyVector = new int[data.Length][];

            for (var i = 0; i < data.Length; i++)
            {
                var values = data[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);

                if (values.Length > 1)
                {
                    var list = new List<int>();
                    for (int j = 1; j < values.Length; j++)
                    {
                        list.Add(int.Parse(values[j]) - 1);
                    }
                    
                    adjacencyVector[i] = list.ToArray();
                }
            }

            var result = FindBridges(adjacencyVector);
            var stringResult = new List<string>();

            foreach (var bridge in result)
            {
                stringResult.Add($"{bridge.Item1 + 1} {bridge.Item2 + 1}");
            }
            
            return stringResult.ToArray();
        }

        private Tuple<int, int>[] FindBridges(int[][] adjacencyVector)
        {
            var visited = new HashSet<int>();
            var disc = new int[adjacencyVector.GetLength(0)];
            var low = new int[adjacencyVector.GetLength(0)];
            var parent = new int[adjacencyVector.GetLength(0)];
            var result = new List<Tuple<int, int>>();

            void dfs(int vertex)
            {
                // mark current vertex as visited
                visited.Add(vertex);

                // initialize discovery time and low value  
                disc[vertex] = low[vertex] = ++_time;

                foreach (var child in adjacencyVector[vertex])
                {
                    // if CHILD is not visited yet, then make it a child of current VERTEX n DFS tree and recur for it.
                    if (!visited.Contains(child))
                    {
                        parent[child] = vertex;
                        dfs(child); 
                    
                        // check if the subtree rooted with CHILD has a connection to one of the ancestors of VERTEX.
                        low[vertex] = Math.Min(low[vertex], low[child]);
                    
                        // if the lowest vertex reachable from subtree under VERTEX is below CHILD in DFS tree,
                        // then VERTEX-CHILD is a bridge.
                        if (low[child] > disc[vertex])
                        {
                            result.Add(new Tuple<int, int>(vertex, child));
                        }
                    }
                    // update low value of VERTEX for parent function calls.  
                    else if (child != parent[vertex])
                    {
                        low[vertex] = Math.Min(low[vertex], low[child]);
                    }
                }
            }
            
            // init all parents with -1
            for (var i = 0; i < parent.Length; i++)
            {
                parent[i] = -1;
            }

            for (var i = 0; i < adjacencyVector.GetLength(0); i++)
            {
                if (!visited.Contains(i))
                {
                    dfs(i);
                }
            }
            
            return result.ToArray();
        }
    }
}