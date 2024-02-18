using System;
using System.Collections.Generic;
using System.Linq;

namespace TopologicalSort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine().Split(' ').Select(int.Parse).ToList();
            var graph = new Graph(input[0]);

            for (int i = 0; i < input[1]; i++)
            {
                var edgeInformation = Console.ReadLine().Split(' ').Select(int.Parse).ToList();
                graph.AddEdge(edgeInformation[0], edgeInformation[1]);
            }

            var output = graph.Dfs();

            Console.Write(string.Join(" ", output));
        }
    }

    internal class Graph
    {
        private Node[] _nodes;

        public Graph(int nodes)
        {
            _nodes = new Node[nodes];

            for (int i = 0; i < _nodes.Length; i++)
            {
                _nodes[i] = new Node(i+1);
            }
        }

        public void AddEdge(int from, int to)
        {
            var fromNode = _nodes[from - 1];
            var toNode = _nodes[to - 1];

            fromNode.neighbours.Add(toNode);
        }

        //create unit tests for AddEdge method

        public Stack<int> Dfs()
        {
            var output = new Stack<int>();
            var removed = new HashSet<Node>();
            for(int i=0; i< _nodes.Length; i++)
            {
                var currentNode = _nodes[i];
                if (removed.Contains(currentNode))
                {
                    continue;
                }

                Explore(currentNode, output, removed);
            }

            return output;
        }

        private void Explore(Node node, Stack<int> output, HashSet<Node> removed)
        {
            for(int i=0;i< node.neighbours.Count; i++)
            {
                var neighbour = node.neighbours[i];
                if (removed.Contains(neighbour))
                {
                    continue;
                }

                Explore(neighbour, output, removed);
            }

            if (node.neighbours.All(n => removed.Contains(n)) || node.neighbours.Count == 0)
            {
                output.Push(node.Value);
                removed.Add(node);
            }
        }

    }

    internal class Node
    {
        private readonly int _value;
        public int Value => _value;
        public Node(int value)
        {
            _value = value;
        }

        public List<Node> neighbours = new List<Node>();


    }
}
