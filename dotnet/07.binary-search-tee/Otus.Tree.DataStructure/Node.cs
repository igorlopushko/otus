using System;

namespace Otus.Tree.DataStructure
{
    public class Node<K, T> where K: IComparable<K>
    {
        public T Value { get; private set; }
        public K Key { get; private set; }
        public Node<K, T> Left { get; set; }
        public Node<K, T> Right { get; set; }
        public int Height { get; set; }
        
        public Node(K key, T value)
        {
            Key = key;
            Value = value;
            Height = 1;
        }
    }
}