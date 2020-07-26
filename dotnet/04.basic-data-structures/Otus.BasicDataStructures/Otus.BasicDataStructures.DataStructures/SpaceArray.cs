using System;
using System.Linq;

namespace Otus.DataStructures
{
    public class SpaceArray<T> : IArray<T>
    {
        //private int _size;
        private readonly int _vector;
        private IArray<T[]> _array;
        private int[] _actualSize;

        public int Size => _actualSize.Sum();
        
        public SpaceArray()
        {
            _array = new VectorArray<T[]>(1);;
            _vector = 100;
            _actualSize = new int[0];
        }
        
        public void Add(T item)
        {
            if (Size == _actualSize.Length * (_vector / 2))
            {
                Resize(_array);
            }

            var outerIndex = Size / (_vector / 2);
            var innerIndex = Size % (_vector / 2);
            
            _array.Get(outerIndex)[innerIndex] = item;
            _actualSize[_actualSize.Length - 1] += 1;
        }

        public void Add(T item, int index)
        {
            // index is out of the range
            if (index > Size - 1)
            {
                return;
            }

            var rowIndexToInsert = GetRowIndex(index);
            var columnIndexToInsert = GetColumnIndex(index);

            var newArray = new VectorArray<T[]>(1);
            var newActualSize = new int[_actualSize.Length];

            // copy all the rows before the row to insert new item
            for (var i = 0; i < rowIndexToInsert; i++)
            {
                newArray.Add(_array.Get(i));
                newActualSize[i] = _actualSize[i];
            }

            var isAdded = false;
            var processNextRow = false;
            
            // insert item in the middle of the array
            for (var i = rowIndexToInsert; i < _actualSize.Length; i++)
            {
                for (var j = 0; j < _vector; j++)
                {
                    processNextRow = false;
                    
                    // check size
                    if (i == newArray.Size)
                    {
                        newArray.Add(new T[_vector]);
                    }

                    // add new item
                    if (!isAdded && j == columnIndexToInsert)
                    {
                        newArray.Get(i)[j] = item;
                        isAdded = true;
                        newActualSize[i] += 1;
                    }

                    if (isAdded)
                    {
                        if (_array.Get(i)[j].Equals(default(T)))
                        {
                            // skip default elements which are not set
                            break;
                        }

                        if (j + 1 <= _vector - 1)
                        {
                            // copy from existing array with +1 shift
                            newArray.Get(i)[j + 1] = _array.Get(i)[j];
                            newActualSize[i] += 1;
                        }
                        else
                        {
                            // check if there is a capacity in the new array
                            if (i + 1 == newArray.Size)
                            {
                                newArray.Add(new T[_vector]);

                                if (i + 1 > newActualSize.Length)
                                {
                                    // add new row to the actual size counter
                                    var tempActualSize = new int[newActualSize.Length + 1];
                                    for (var k = 0; k < newActualSize.Length; k++)
                                    {
                                        tempActualSize[k] = newActualSize[k];
                                    }

                                    newActualSize = tempActualSize;
                                }
                            }
                            
                            // shift to the next row
                            newArray.Get(i + 1)[0] = _array.Get(i)[j];
                            newActualSize[i + 1] += 1;
                            processNextRow = true;
                        }
                    }
                    else
                    {
                        // copy from existing array
                        newArray.Get(i)[j] = _array.Get(i)[j];
                        newActualSize[i] += 1;
                    }
                }

                if (processNextRow)
                {
                    continue;
                }
                
                rowIndexToInsert = i;
                break;
            }
            
            // copy all the rows which were not changed
            for (var i = rowIndexToInsert + 1; i < _actualSize.Length; i++)
            {
                newArray.Add(_array.Get(i));
                newActualSize[i] = _actualSize[i];
            }
            
            _array = newArray;
            _actualSize = newActualSize;
        }

        public T Remove(int index)
        {
            throw new NotImplementedException();
        }

        public T Get(int index)
        {
            return _array.Get(GetRowIndex(index))[GetColumnIndex(index)];
        }

        private void Resize(IArray<T[]> array)
        {
            array.Add(new T[_vector]);
            
            if (_actualSize.Length == 0)
            {
                // inti actual size
                _actualSize = new int[1];
            }
            else
            {
                // add new row to the actual size counter
                var tempActualSize = new int[_actualSize.Length + 1];
                for (var i = 0; i < _actualSize.Length; i++)
                {
                    tempActualSize[i] = _actualSize[i];
                }
                _actualSize = tempActualSize;                
            }
        }
        
        private int GetRowIndex(int index)
        {
            var count = 0;
            for (var i = 0; i < _actualSize.Length; i++)
            {
                if (index >= count - 1 && index <= count - 1 + _actualSize[i])
                {
                    return i;
                }
                
                count += _actualSize[i];
            }

            return -1;
        }
        
        private int GetColumnIndex(int index)
        {
            var count = 0;
            var indexCountLeft = index;
            
            foreach (var rowCount in _actualSize)
            {
                if (index >= count - 1 && index <= count - 1 + rowCount)
                {
                    return indexCountLeft;
                }
                
                count += rowCount;
                indexCountLeft -= rowCount;
            }

            return -1;
        }
    }
}