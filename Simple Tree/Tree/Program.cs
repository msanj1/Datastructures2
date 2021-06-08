using System;
using System.Collections.Generic;
using System.Linq;

namespace Tree
{
    class Program
    {
        static void Main(string[] args)
        {
            var numberOfNodes = int.Parse(Console.ReadLine());

            var parentNodes = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            var nodes = new int[numberOfNodes];
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i] = i;
            }

            var tree = new Tree(nodes, parentNodes);
            Console.WriteLine(tree.CalculateHeight());
        }

        public class Node
        {
            public Node()
            {
                Children = new List<Node>();
            }

            public Node Parent { get; set; }
            public List<Node> Children { get; set; }
            public int Height { get; set; }
        }

        public class Tree
        {
            private readonly Node[] _nodes;
            private readonly Node _root;

            public Tree(int[] nodes, int[] parents)
            {
                _nodes = new Node[nodes.Length];

                for (var index = 0; index < nodes.Length; index++)
                {
                    var newNode = new Node();

                    _nodes[index] = newNode;
                }

                for (var index = 0; index < parents.Length; index++)
                {
                    var parent = parents[index];
                    if (parent == -1)
                    {
                        _root = _nodes[index];
                    }
                    else
                    {
                        var currentNode = _nodes[index];
                        var parentNode = _nodes[parent];
                        parentNode.Children.Add(currentNode);
                        currentNode.Parent = parentNode;
                    }
                }
            }

            public int CalculateHeight()
            {
                var queue = new Queue<Node>();
                queue.Enqueue(_root);
                var height = 0;
                while (queue.Count > 0)
                {
                    int size = queue.Count;
                    height++;
                    while (size > 0)
                    {
                        var node = queue.Dequeue();
                        foreach (var child in node.Children)
                        {
                            queue.Enqueue(child);
                        }

                        size--;
                    }
                }

                return height;

                //bfs 2nd approach
                //var queue = new Queue<Node>();
                //queue.Enqueue(_root);
                //var height = 1;
                //_root.Height = 1;

                //while (queue.Count > 0)
                //{
                //    var node = queue.Dequeue();

                //    foreach (var child in node.Children)
                //    {
                //        child.Height = node.Height + 1;
                //        height = Math.Max(child.Height, height);
                //        queue.Enqueue(child);
                //    }
                //}

                //return height;
            }
        }
    }
}
