using System;

namespace Otus.Tree.DataStructure
{
    public interface ITree<K, T> where K: IComparable<K>
    {
        void Insert(Node<K, T> node);
        void Remove(K key);
        Node<K, T> Find(K key);
    }
}