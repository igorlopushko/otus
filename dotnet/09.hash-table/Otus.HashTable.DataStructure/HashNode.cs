using System;

namespace Otus.HashTable.DataStructure
{
    public class HashNode<K, T> where K: IComparable<K> where T: IComparable<T>
    {
        private K _key;
        private T _value;
        private HashNode<K, T> _next;

        public K Key => _key;
        public T Value => _value;

        public HashNode<K, T> Next
        {
            set => _next = value;
            get => _next;
        }
        
        public HashNode(K key, T value)
        {
            _key = key;
            _value = value;
        }
    }
}