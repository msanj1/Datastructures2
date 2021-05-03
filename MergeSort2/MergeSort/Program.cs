using System;
using System.Runtime.InteropServices;

namespace MergeSort
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] input = new int[] { 4, 1, 3, 9, 7};
            QuickSort(input, 0,input.Length - 1);
        }

        static void QuickSort(int[] arr, int low, int high)
        {
            if (low < high)
            {
                var pivot = Partition(arr, low, high);
                QuickSort(arr, low,pivot - 1);
                QuickSort(arr, pivot + 1, high);
            }
        }

        static int Partition(int[] arr, int low, int high)
        {
            var pivot = high;

            var i = low - 1; //anything up until this index is considered smaller than pivot

            for (var j = 0; j < high; j++)  //j - anything after i up until J is bigger or equal to the pivot
            {
                var currentValue = arr[j];
                if (currentValue < arr[pivot])
                {
                    i++;
                    var temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }

            var temp2 = arr[i + 1];
            arr[i + 1] = arr[pivot];
            arr[pivot] = temp2;
            return i + 1;
        }
    }
}
