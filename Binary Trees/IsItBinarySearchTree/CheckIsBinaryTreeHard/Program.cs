using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckIsBinaryTreeEasy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var numberOfVertices = int.Parse(Console.ReadLine());
            var tree = new BinaryTree(numberOfVertices);
            for (int i = 0; i < numberOfVertices; i++)
            {
                var treeInputs = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToList();
                tree.Add(treeInputs[0], treeInputs[1], treeInputs[2]);
            }

            if (tree.IsBinarySearchTree)
            {
                Console.Write("CORRECT");
            }
            else
            {
                Console.Write("INCORRECT");
            }
        }
    }
}
