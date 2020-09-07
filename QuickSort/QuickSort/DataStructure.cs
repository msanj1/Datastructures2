using System;
using System.Collections.Generic;

namespace QuickSort
{
    public class DataStructure<T> : List<T>
        where T : IComparable, new()
    {
        public void QuickSort(int p, int r)
        {
            if (p < r)
            {
                var q = Partition(p, r);
                QuickSort(p, q - 1);
                QuickSort(q + 1, r);
            }
        }

        private int Partition(int p, int r)
        {
            var pivot = this[r]; //this will be the pivot
            var i = p - 1;
            T temp;
            for (var j = p;
                j <= r - 1;
                j++) //loop through all the items at the left side of the pivot. Exclude the pivot.
                if (this[j].CompareTo(pivot) <= 0)
                {
                    i++;
                    temp = this[i];
                    this[i] = this[j];
                    this[j] = temp;
                }

            temp = this[i + 1];
            this[i + 1] = this[r];
            this[r] = temp;
            return i + 1; //new location of the pivot
        }
    }
}