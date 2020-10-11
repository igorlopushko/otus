using System;
using System.Collections.Generic;
using System.Linq;

namespace Otus.DataStructure
{
    public class Graph
    {
        private List<Edge> _edges;
        private HashSet<int> _vertices;
        private int[,] _adjacencyMatrix;

        public Edge[] Edges => _edges.ToArray();
        public int[] Vertices => _vertices.ToArray();

        public Graph()
        {
            _edges = new List<Edge>();
            _vertices = new HashSet<int>();
        }

        public Graph(int[,] adjacencyMatrix)
        {
            _adjacencyMatrix = adjacencyMatrix;
            
            _edges = new List<Edge>();
            _vertices = new HashSet<int>();
            
            // convert to edges
            for (var i = 0; i < adjacencyMatrix.GetLongLength(0); i++)
            {
                _vertices.Add(i);
                
                for (var j = i; j < adjacencyMatrix.GetLongLength(1); j++)
                {
                    if (adjacencyMatrix[i, j] != 0)
                    {
                        _edges.Add(new Edge(i, j, adjacencyMatrix[i, j]));                        
                    }
                }
            }
        }
        
        public Graph(Edge[] edges)
        {
            _edges = new List<Edge>();
            _vertices = new HashSet<int>();
            
            foreach (var edge in edges)
            {
                if (!_vertices.Contains(edge.Source))
                {
                    _vertices.Add(edge.Source);
                }
                
                if (!_vertices.Contains(edge.Destination))
                {
                    _vertices.Add(edge.Destination);
                }
                
                _edges.Add(edge);    
            }
        }

        public void AddEdge(int source, int destination, int rank)
        {
            if (!_vertices.Contains(source))
            {
                _vertices.Add(source);
            }
                
            if (!_vertices.Contains(destination))
            {
                _vertices.Add(destination);
            }
            
            _edges.Add(new Edge(source, destination, rank));
        }
        
        public void AddEdge(Edge edge)
        {
            if (!_vertices.Contains(edge.Source))
            {
                _vertices.Add(edge.Source);
            }
                
            if (!_vertices.Contains(edge.Destination))
            {
                _vertices.Add(edge.Destination);
            }
            
            _edges.Add(edge);
        }

        private int GetMinDistance(int[] distance, bool[] shortestPathSet)
        {
            var minValue = int.MaxValue;
            var minIndex = -1;

            for (var vertex = 0; vertex < _vertices.Count; vertex++)
            {
                if (shortestPathSet[vertex] == false && distance[vertex] <= minValue)
                {
                    minValue = distance[vertex];
                    minIndex = vertex;
                }
            }

            return minIndex;
        }
        
        public IEnumerable<int> GetShortestPathDijkstra(int source)
        {
            // the output array. distance[i] will hold the shortest distance from source to i
            int[] distance = new int[_vertices.Count];

            // shortestPathSet[i] will true if vertex i is included in shortest path 
            // tree or shortest distance from src to i is finalized 
            bool[] shortestPathSet = new bool[_vertices.Count]; 
  
            // initialize all distances as INFINITE and shortestPathSet[] as false 
            for (var i = 0; i < _vertices.Count; i++) { 
                distance[i] = int.MaxValue; 
                shortestPathSet[i] = false; 
            } 
  
            // distance of source vertex from itself is always 0 
            distance[source] = 0;
  
            // find shortest path for all vertices
            for (var count = 0; count < _vertices.Count - 1; count++) 
            { 
                // pick the minimum distance vertex from the set of vertices not yet processed.
                // minDistanceVertex is always equal to src in first iteration. 
                var minDistanceVertex = GetMinDistance(distance, shortestPathSet); 
  
                // Mark the picked vertex as processed 
                shortestPathSet[minDistanceVertex] = true; 
  
                // update dist value of the adjacent vertices of the picked vertex. 
                for (var vertex = 0; vertex < _vertices.Count; vertex++)
                {

                    // update distance[vertex] only if is not in shortestPathSet,
                    // there is an edge from minDistanceVertex to vertex,
                    // and total weight of path from source to v through u is smaller than current value of distance[v] 
                    if (!shortestPathSet[vertex] && 
                        _adjacencyMatrix[minDistanceVertex, vertex] != 0 &&
                        distance[minDistanceVertex] != int.MaxValue &&
                        distance[minDistanceVertex] + _adjacencyMatrix[minDistanceVertex, vertex] < distance[vertex])
                    {
                        distance[vertex] = distance[minDistanceVertex] + _adjacencyMatrix[minDistanceVertex, vertex];
                    }
                }
            }

            return distance;
        }
        
        public int[,] GetShortestPathFloydWarshall()
        {
            int[,] distance = new int[_vertices.Count, _vertices.Count];
  
            // initialize the result matrix same as input graph matrix
            for (var i = 0; i < _vertices.Count; i++) 
            { 
                for (var j = 0; j < _vertices.Count; j++) 
                { 
                    distance[i, j] = _adjacencyMatrix[i, j]; 
                } 
            }
            
            for (var k = 0; k < _vertices.Count; k++) 
            { 
                // pick all vertices as source one by one 
                for (var i = 0; i < _vertices.Count; i++) 
                { 
                    // pick all vertices as destination for the above picked source 
                    for (var j = 0; j < _vertices.Count; j++) 
                    { 
                        // if vertex k is on the shortest path from i to j
                        // then update the value of distance[i][j] 
                        if (distance[i, k] + distance[k, j] < distance[i, j])  
                        { 
                            distance[i, j] = distance[i, k] + distance[k, j]; 
                        }
                    }
                }
            }

            return distance;
        }
        
        public IEnumerable<int> GetShortestPathBellmanFord(int source)
        {
            var distance = new int[_vertices.Count];

            for (var i = 0; i < _vertices.Count; i++)
            {
                distance[i] = int.MaxValue;
            }
 
            distance[source] = 0;
 
            for (var i = 1; i <= _vertices.Count - 1; ++i)
            {
                foreach (var edge in _edges)
                {
                    int u = edge.Source;
                    int v = edge.Destination;
                    int rank = edge.Rank;

                    if (distance[u] != int.MaxValue && distance[u] + rank < distance[v])
                    {
                        distance[v] = distance[u] + rank;
                    }
                }
            }
 
            foreach (var edge in _edges)
            {
                var u = edge.Source;
                var v = edge.Destination;
                var rank = edge.Rank;

                if (distance[u] != int.MaxValue && distance[u] + rank < distance[v])
                {
                    return null;
                }
            }

            return distance;
        }
    }
}