using System;
using System.Collections.Generic;
using System.Linq;

namespace AddingExitsToMaze
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

            Console.WriteLine(graph.Dfs());
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
                _nodes[i] = new Node();
            }
        }

        public void AddEdge(int from, int to)
        {
            var fromNode = _nodes[from - 1];
            var toNode = _nodes[to - 1];

            fromNode.neighbours.Add(toNode);
            toNode.neighbours.Add(fromNode);
        }

        public int Dfs()
        {
            HashSet<Node> visited = new HashSet<Node>();
            int connectedComponents = 0;
            for (int i=0;i< _nodes.Length; i++)
            {
                var currentNode = _nodes[i];

                if (visited.Contains(currentNode))
                {
                    continue;
                }

                Explore(currentNode, visited);
                connectedComponents++;
            }

            return connectedComponents;
        }

        private void Explore(Node currentNode, HashSet<Node> visited)
        {
            visited.Add(currentNode);

            foreach (var neighbour in currentNode.neighbours)
            {
                if (visited.Contains(neighbour))
                {
                    continue;
                }

                Explore(neighbour, visited);
            }
        }
    }

    internal class Node
    {
        public List<Node> neighbours = new List<Node>();
    }
}
