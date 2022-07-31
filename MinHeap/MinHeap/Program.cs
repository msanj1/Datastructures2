using System;
using System.Linq;

namespace MinHeap
{
    class Program
    {
        static void Main(string[] args)
        {
            var numberOfIntegers = int.Parse(Console.ReadLine());
            var inputs = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            MinHeap heap = new MinHeap();
            heap.BuildHeap(inputs);
            heap.PrintSwaps();
        }
    }

}
