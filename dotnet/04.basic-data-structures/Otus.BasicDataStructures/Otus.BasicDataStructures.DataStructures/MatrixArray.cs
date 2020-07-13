using System;

namespace Otus.DataStructures
{
    public class MatrixArray<T> : IArray<T>
    {
        private int _size;
        private int _vector;
        private IArray<IArray<T>> _array;

        public int Size => _size;

        public MatrixArray()
        {
            _array = (IArray<IArray<T>>) new FactorArray<FactorArray<T>>();
            _vector = 100;
            _size = 0;
        }

        public void Add(T item)
        {
            if (_size == _array.Size * _vector)
            {
                _array.Add(new VectorArray<T>(_vector));
            }

            _array.Get(_size / _vector).Add(item);
            _size++;
        }

        public void Add(T item, int index)
        {
            throw new NotImplementedException();
        }

        public T Get(int index)
        {
            return _array.Get(index / _vector).Get(index % _vector);
        }

        public T Remove(int index)
        {
            throw new NotImplementedException();
        }
    }
}
