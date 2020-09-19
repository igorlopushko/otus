using System;

namespace Otus.HashTable.DataStructure
{
    public class HashTableWithOpenAddressing<K, T> where K: IComparable<K> where T: IComparable<T>
    {
        private const int c1 = 55;
        private const int c2 = 97;
        private const double DefaultLoadFactor = 0.75;
        private const int DefaultCapacity = 100;
        
        private readonly double _loadFactor;
        private int _threshold;
        private int _count;
        private HashNode<K, T>[] _buckets;

        public int Count => _count;
        
        public HashTableWithOpenAddressing() : this(DefaultCapacity, DefaultLoadFactor) { }

        public HashTableWithOpenAddressing(int capacity) : this(capacity, DefaultLoadFactor) { }

        public HashTableWithOpenAddressing(int capacity, double loadFactor)
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
            var attempt = 0;
            var index = Hash(key, attempt);
            var foundBucket = _buckets[index];

            while (foundBucket != null)
            {
                if (key.CompareTo(foundBucket.Key) == 0 && !foundBucket.IsDeleted)
                {
                    return true;
                }

                attempt++;
                index = Hash(key, attempt);
                foundBucket = _buckets[index];
            }
            
            return default;
        }

        public bool ContainsValue(T value)
        {
            foreach (var node in _buckets)
            {
                if (node != null && node.Value.CompareTo(value) == 0)
                {
                    return true;
                }
            }

            return false;
        }
        
        public T Find(K key)
        {
            var i = 0;
            var index = Hash(key, i);
            var foundBucket = _buckets[index];

            while (foundBucket != null)
            {
                if (key.CompareTo(foundBucket.Key) == 0 && !foundBucket.IsDeleted)
                {
                    return foundBucket.Value;
                }

                i++;
                index = Hash(key, i);
                foundBucket = _buckets[index];
            }
            
            return default;
        }
        
        public void Add(K key, T value)
        {
            var attempt = 0;
            var index = Hash(key, attempt);
            var foundBucket = _buckets[index];
            var deletedNodeIndex = -1;

            // check if rehash and extension is needed.
            if (_count + 1 > _threshold)
            {
                Rehash();
            }

            if (foundBucket != null)
            {
                // find appropriate bucket
                while (!(foundBucket == null || foundBucket.IsDeleted))
                {
                    if (key.CompareTo(foundBucket.Key) == 0 && !foundBucket.IsDeleted)
                    {
                        throw new ArgumentException("Item with the same key already exists.");
                    }
                    
                    if (deletedNodeIndex == -1 && foundBucket.IsDeleted)
                    {
                        deletedNodeIndex = index;
                    }

                    attempt++;
                    index = Hash(key, attempt);
                    foundBucket = _buckets[index];
                }
            }
            
            _buckets[index] = new HashNode<K, T>(key, value);
            _count++;

            // swap with deleted node
            if (deletedNodeIndex != -1)
            {
                var temp = _buckets[index];
                _buckets[index] = _buckets[deletedNodeIndex];
                _buckets[deletedNodeIndex] = temp;
            }
        }

        public void Remove(K key)
        {
            var attempt = 0;
            var index = Hash(key, attempt);
            var bucket = _buckets[index];

            while (bucket != null)
            {
                if (key.CompareTo(bucket.Key) == 0)
                {
                    bucket.IsDeleted = true;
                    _count--;
                    return;
                }

                attempt++;
                index = Hash(key, attempt);
                bucket = _buckets[index];
            }
        }

        private int Hash(K key, int i)
        {
            return key == null ? 0 : Math.Abs((key.GetHashCode() + c1 * i + c2 * i * i) % _buckets.Length);
        }

        private void Rehash()
        {
            var oldBuckets = _buckets;

            var newCapacity = _buckets.Length * 2 + 1;
            _threshold = Convert.ToInt32(newCapacity * _loadFactor);
            _buckets = new HashNode<K, T>[newCapacity];

            int i = 0;
            foreach (var node in oldBuckets)
            {
                if (node == null) continue;
                
                var index = Hash(node.Key, i);
                var foundBucket = _buckets[index];

                if (foundBucket != null)
                {
                    // find appropriate bucket
                    while (foundBucket != null)
                    {
                        i++;
                        index = Hash(node.Key, i);
                        foundBucket = _buckets[index];
                    }
                }
            
                _buckets[index] = node;
            }
        }
    }
}