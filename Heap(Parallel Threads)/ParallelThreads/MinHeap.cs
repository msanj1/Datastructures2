using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelThreads
{
    public class MinHeap<T>
        where T: IComparable<T>
    {
        private List<T> _heap = new List<T>();

        public void BuildHeap(T[] inputArray)
        {
            var startingIndex = (int)Math.Floor(inputArray.Length / 2.0) - 1;
            _heap = new List<T>(inputArray);

            for (var i = startingIndex; i >= 0; i--)
            {
                SiftDown(i);
            }
        }

        public T ExtractMin()
        {
            var value = _heap[0];
            Swap(0, _heap.Count - 1);
            _heap.RemoveAt(_heap.Count - 1);

            SiftDown(0);
           
            return value;
        }

        public void Add(T value)
        {
            _heap.Add(value);
            SiftUp(_heap.Count - 1);
        }

        private void SiftUp(int index)
        {
            var parentIndex = GetParent(index);
            if(parentIndex >= 0)
            {
                var parentValue = _heap[parentIndex];
                if (_heap[index].CompareTo(parentValue) == -1) // _heap[index] < parentValue
                {
                    Swap(index, parentIndex);
                    SiftUp(parentIndex);
                }
            }
        }

        private void SiftDown(int index)
        {
            var leftChildIndex = GetLeftChild(index);
            var rightChildIndex = GetRightChild(index);
            T leftChildValue = default(T);
            T rightChildvalue = default(T);
            if (leftChildIndex < _heap.Count) leftChildValue = _heap[leftChildIndex];

            if (rightChildIndex < _heap.Count) rightChildvalue = _heap[rightChildIndex];

            if (leftChildValue != null && rightChildvalue != null)
            {
                if (_heap[index].CompareTo(leftChildValue) == 1 && _heap[index].CompareTo(rightChildvalue) <= 0)
                {
                    //swap with the leftchildvalue
                    Swap(index, leftChildIndex);
                    SiftDown(leftChildIndex);
                }
                else if (_heap[index].CompareTo(rightChildvalue) == 1 && _heap[index].CompareTo(leftChildValue) <= 0)
                {
                    //swap with the rightchildvalue
                    Swap(index, rightChildIndex);
                    SiftDown(rightChildIndex);
                }
                else if (_heap[index].CompareTo(leftChildValue) == 1 && _heap[index].CompareTo(rightChildvalue) == 1)
                {
                    if (leftChildValue.CompareTo(rightChildvalue) <= 0)
                    {
                        Swap(index, leftChildIndex);
                        SiftDown(leftChildIndex);
                        //swap with leftChildValue
                    }
                    else
                    {
                        Swap(index, rightChildIndex);
                        SiftDown(rightChildIndex);
                    }
                }
            }
            else if (leftChildValue != null)
            {
                if (_heap[index].CompareTo(leftChildValue) == 1)
                {
                    Swap(index, leftChildIndex);
                    SiftDown(leftChildIndex);
                    //swap with leftChildValue
                }
            }
            else if (rightChildvalue != null)
            {
                if (_heap[index].CompareTo(rightChildvalue) == 1)
                {
                    Swap(index, rightChildIndex);
                    SiftDown(rightChildIndex);
                    //swap with rightChildvalue
                }
            }
        }

        private void Swap(int indexA, int indexB)
        {
            var temp = _heap[indexA];
            _heap[indexA] = _heap[indexB];
            _heap[indexB] = temp;
        }

        private int GetParent(int index)
        {
            return (int)Math.Floor((index - 1) / 2.0);
        }

        private int GetLeftChild(int index)
        {
            return 2 * index + 1;
        }

        private int GetRightChild(int index)
        {
            return 2 * index + 2;
        }
    }

}
