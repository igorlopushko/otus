using System;

namespace Otus.DataStructures
{
    public class MatrixArray<T> : IArray<T>
    {
        private int _size;
        private readonly int _vector;
        private IArray<T[]> _array;

        public int Size => _size;

        public MatrixArray()
        {
            _array = new VectorArray<T[]>(1);
            _vector = 100;
            _size = 0;
        }

        public void Add(T item)
        {
            if (_size == _array.Size * _vector)
            {
                _array.Add(new T[_vector]);
            }
            
            _array.Get(_size / _vector)[_size % _vector] = item;
            _size++;
        }

        public void Add(T item, int index)
        {
            // index is out of the range
            if (index > _size - 1)
            {
                throw new IndexOutOfRangeException();
            }

            var newArray = new VectorArray<T[]>(1);
            var newSize = 0;
            var isAdded = false;

            var outerIndex = index / _vector;
            var innerIndex = index % _vector;

            var matrixRowCount = _size % _vector == 0 ? _size / _vector : (_size / _vector) + 1;

            // insert item in the middle of the array
            for (int i = 0; i < matrixRowCount; i++)
            {
                for (int j = 0; j < _vector; j++)
                {
                    // check for resize
                    if (newSize == newArray.Size * _vector)
                    {
                        newArray.Add(new T[_vector]);
                    }

                    // add new item
                    if (i == outerIndex && j == innerIndex)
                    {
                        newArray.Get(i)[j] = item;
                        isAdded = true;
                        newSize++;
                    }

                    if (isAdded)
                    {
                        if (_array.Get(i)[j].Equals(default(T)))
                        {
                            // skip default elements which are not set
                            continue;
                        }

                        if (j + 1 <= _vector - 1)
                        {
                            // copy from existing array with +1 shift
                            newArray.Get(i)[j + 1] = _array.Get(i)[j];
                        }
                        else
                        {
                            if (newSize == newArray.Size * _vector)
                            {
                                newArray.Add(new T[_vector]);
                            }
                            
                            newArray.Get(i + 1)[0] = _array.Get(i)[j];
                        }

                    }
                    else
                    {
                        // copy from existing array
                        newArray.Get(i)[j] = _array.Get(i)[j];
                    }

                    newSize++;
                }
            }

            _array = newArray;
            _size = newSize;
        }

        public T Get(int index)
        {
            return _array.Get(index / _vector)[index % _vector];
        }

        public T Remove(int index)
        {
            // index is out of the range
            if (index > _size - 1)
            {
                throw new IndexOutOfRangeException();
            }
            
            var newArray = new VectorArray<T[]>(1);
            var newSize = 0;
            var isRemoved = false;

            var outerIndex = index / _vector;
            var innerIndex = index % _vector;
            var removedItem = _array.Get(outerIndex)[innerIndex];

            var matrixRowCount = _size % _vector == 0 ? _size / _vector : (_size / _vector) + 1;

            for (var i = 0; i < matrixRowCount; i++)
            {
                for (var j = 0; j < _vector; j++)
                {
                    // check for resize
                    if (newSize == newArray.Size * _vector)
                    {
                        newArray.Add(new T[_vector]);
                    }

                    if (i == outerIndex && j == innerIndex)
                    {
                        isRemoved = true;
                        break;
                    }

                    // copy from existing array
                    newArray.Get(i)[j] = _array.Get(i)[j];

                    newSize++;
                }

                if (isRemoved)
                {
                    break;
                }
            }

            // check if not the last item
            if (newSize < _size - 1)
            {
                var continueRowIndex = (newSize + 1) / _vector;
                var continueCellIndex = (newSize + 1) % _vector;
                for (var i = continueRowIndex; i < matrixRowCount; i++)
                {
                    for (var j = continueCellIndex; j < _vector; j++)
                    {
                        // check for resize
                        if (newSize == newArray.Size * _vector)
                        {
                            newArray.Add(new T[_vector]);
                        }

                        if (j - 1 > -1 && j < _vector)
                        {
                            newArray.Get(i)[j - 1] = _array.Get(i)[j];
                        }
                        else if (j == 0)
                        {
                            newArray.Get(i - 1)[_vector - 1] = _array.Get(i)[j];
                        }

                        newSize++;
                    }

                    continueCellIndex = 0;
                }
            }

            // remove unused matrix row
            if (newSize % _vector == 0)
            {
                newArray.Remove(newSize / _vector);
            }

            _size = newSize;
            _array = newArray;
            return removedItem;
        }
    }
}
