using System;

namespace Otus.DataStructure
{
    public class Edge : IComparable<Edge>
    {
        private int _source;
        private int _destination;
        private int _rank;

        public int Source => _source;
        public int Destination => _destination;
        public int Rank => _rank;

        public Edge(int source, int destination, int rank)
        {
            _source = source;
            _destination = destination;
            _rank = rank;
        }
        
        public int CompareTo(Edge otherEdge)
        {
            return _rank - otherEdge.Rank;
        }
    }
}