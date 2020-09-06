using System;

namespace Otus.BinarySearchTree.DataStructure
{
    public class Node<K, T> where K: IComparable<K>
    {
        public T Value { get; set; }
        public K Key { get; set; }
        public Node<K, T> Left { get; set; }
        public Node<K, T> Right { get; set; }
    }
}