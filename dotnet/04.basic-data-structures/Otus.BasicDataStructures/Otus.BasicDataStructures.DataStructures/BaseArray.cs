using System;

namespace Otus.DataStructures
{
    public abstract class BaseArray<T> : IArray<T>
    {
        protected T[] _array;
        protected int _size;

        public virtual int Size => _size;

        public virtual void Add(T item)
        {
            throw new NotImplementedException();
        }

        public virtual void Add(T item, int index)
        {
            throw new NotImplementedException();
        }

        public virtual T Get(int index)
        {
            return _array[index];
        }

        public virtual T Remove(int index)
        {
            T[] newArray = new T[_array.Length - 1];
            T item = _array[index];

            for (int i = 0; i < index; i++)
            {
                newArray[i] = _array[i];
            }
            for (int i = index; i < _array.Length - 1; i++)
            {
                newArray[i] = _array[i + 1];
            }

            _array = newArray;
            _size = _array.Length;

            return item;
        }
    }
}
