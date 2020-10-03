using System;

namespace Otus.HashTable.DataStructure
{
    public class HashTable<K, T> where K: IComparable<K> where T: IComparable<T>
    {
        private const double DefaultLoadFactor = 0.75;
        private const int DefaultCapacity = 100;

        private readonly double _loadFactor;
        private int _threshold;
        private int _count;
        private HashNode<K, T>[] _buckets;

        public int Count => _count;
        
        public HashTable() : this(DefaultCapacity, DefaultLoadFactor) { }

        public HashTable(int capacity) : this(capacity, DefaultLoadFactor) { }

        public HashTable(int capacity, double loadFactor)
        {
            if (capacity < 0) throw new ArgumentException("Capacity has to be greater than or equal to 0.");
            if (loadFactor <= 0) throw new ArgumentException("Load factor has to be greater than 0.");
            if (capacity == 0)
            {
                capacity = 1;
            }
            
            _loadFactor = loadFactor;
            _buckets = new HashNode<K, T>[capacity];
            _threshold = Convert.ToInt32(capacity * loadFactor);
        }

        public bool ContainsKey(K key)
        {
            var index = Hash(key);
            var item = _buckets[index];

            if (item == null) return false;
            
            while (item != null)
            {
                if (item.Key.CompareTo(key) == 0)
                {
                    return true;
                }

                item = item.Next;
            }

            return false;
        }

        public bool ContainsValue(T value)
        {
            foreach (var bucket in _buckets)
            {
                if (bucket == null) continue;
                
                var item = bucket;
                while (item != null)
                {
                    if (item.Value.CompareTo(value) == 0)
                    {
                        return true;
                    }

                    item = item.Next;
                }
            }

            return false;
        }
        
        public T Find(K key)
        {
            var index = Hash(key);
            var item = _buckets[index];

            if (item == null) return default;
            
            while (item != null)
            {
                if (item.Key.CompareTo(key) == 0)
                {
                    return item.Value;
                }

                item = item.Next;
            }

            return default;
        }
        
        public void Add(K key, T value)
        {
            var index = Hash(key);
            var bucket = _buckets[index];

            // check that node is not already added.
            while (bucket != null)
            {
                if (key.CompareTo(bucket.Key) == 0)
                {
                    // rewrite value and break if already exist
                    bucket.Value = value;
                    return;
                }

                bucket = bucket.Next;
            }

            // check if rehash and extension is needed.
            if (_count + 1 > _threshold)
            {
                Rehash();
                index = Hash(key);
            }

            // add new node.
            var item = new HashNode<K, T>(key, value)
            {
                Next = _buckets[index]
            };
            _buckets[index] = item;
            _count++;
        }

        public void Remove(K key)
        {
            var index = Hash(key);
            var bucket = _buckets[index];
            HashNode<K, T> lastNode = null;

            while (bucket != null)
            {
                if (bucket.Key.CompareTo(key) == 0)
                {
                    _count--;
                    if (lastNode == null)
                    {
                        _buckets[index] = bucket.Next;
                    }
                    else
                    {
                        lastNode.Next = bucket.Next;
                    }
                }

                lastNode = bucket;
                bucket = bucket.Next;
            }
        }

        private int Hash(K key)
        {
            return key == null ? 0 : Math.Abs(key.GetHashCode() % _buckets.Length);
        }

        private void Rehash()
        {
            var oldBuckets = _buckets;

            var newCapacity = _buckets.Length * 2 + 1;
            _threshold = Convert.ToInt32(newCapacity * _loadFactor);
            _buckets = new HashNode<K, T>[newCapacity];

            for (var i = oldBuckets.Length - 1; i > 0; i--)
            {
                var currentNode = oldBuckets[i];
                while (currentNode != null)
                {
                    var index = Hash(currentNode.Key);
                    var node = new HashNode<K, T>(currentNode.Key, currentNode.Value)
                    {
                        Next = _buckets[index]
                    };
                    _buckets[index] = node;
                    currentNode = currentNode.Next;
                }
            }
        }
    }
}