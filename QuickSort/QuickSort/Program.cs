using System;

namespace QuickSort
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var items = new DataStructure<int>
            {
                2, 8, 7, 1, 3, 5, 6, 4
            };

            items.QuickSort(0, items.Count - 1);

            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }
    }
}