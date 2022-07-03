using System;
using System.Linq;

namespace TreeHeight
{
    class Program
    {
        static void Main(string[] args)
        {
            var numberOfNodes = int.Parse(Console.ReadLine());

            var parentNodes = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            Tree tree = new Tree(numberOfNodes, parentNodes);
            Console.WriteLine(tree.GetHeight());
        }
    }
}
