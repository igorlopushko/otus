using System;

namespace Otus.DataStructure
{
    public class Node<T> where T: IComparable<T> 
    {
        public Node<T> Next { get; set; }
        public T Value { get; set; }
    }
}