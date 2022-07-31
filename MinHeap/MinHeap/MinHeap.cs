using System;
using System.Collections.Generic;

namespace MinHeap
{
    public class MinHeap
    {
        private int[] _heap;
        private List<string> _swaps = new List<string>();
        
        public void BuildHeap(int[] inputArray)
        {
            var startingIndex = (int) Math.Floor(inputArray.Length / 2.0) - 1;
            _heap = inputArray;
            
            for (var i = startingIndex; i >= 0; i--)
            {
                 SiftDown(i);
            }
        }

        private void SiftDown(int index)
        {
            var leftChildIndex = GetLeftChild(index);
            var rightChildIndex = GetRightChild(index);
            int? leftChildValue = null;
            int? rightChildvalue = null;
            if (leftChildIndex < _heap.Length) leftChildValue = _heap[leftChildIndex];

            if (rightChildIndex < _heap.Length) rightChildvalue = _heap[rightChildIndex];

            if (leftChildValue.HasValue && rightChildvalue.HasValue)
            {
                if (_heap[index] > leftChildValue && _heap[index] <= rightChildvalue)
                {
                    //swap with the leftchildvalue
                    Swap(index, leftChildIndex);
                    SiftDown(leftChildIndex);
                }
                else if (_heap[index] > rightChildvalue && _heap[index] <= leftChildValue)
                {
                    //swap with the rightchildvalue
                    Swap(index, rightChildIndex);
                    SiftDown(rightChildIndex);
                }
                else if (_heap[index] > leftChildValue && _heap[index] > rightChildvalue)
                {
                    if (leftChildValue <= rightChildvalue)
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
            else if (leftChildValue.HasValue)
            {
                if (_heap[index] > leftChildValue)
                {
                    Swap(index, leftChildIndex);
                    SiftDown(leftChildIndex);
                    //swap with leftChildValue
                }
            }
            else if(rightChildvalue.HasValue)
            {
                if (_heap[index] > rightChildvalue)
                {
                    Swap(index, rightChildIndex);
                    SiftDown(rightChildIndex);
                    //swap with rightChildvalue
                }
            }
        }

        private void Swap(int indexA, int indexB)
        {
            _swaps.Add($"{indexA} {indexB}");
            var temp = _heap[indexA];
            _heap[indexA] = _heap[indexB];
            _heap[indexB] = temp;
        }

        public void PrintSwaps()
        {
            Console.WriteLine(_swaps.Count);
            foreach (var swap in _swaps)
            {
                Console.WriteLine(swap);
            }
        }

        private int GetParent(int index)
        {
            return (int) Math.Floor((index - 1) / 2.0);
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