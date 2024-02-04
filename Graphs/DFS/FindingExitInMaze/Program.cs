using System;
using System.Collections.Generic;
using System.Linq;

namespace FindingExitInMaze
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine().Split(' ').Select(int.Parse).ToList();
            var graph = new Graph(input[0]);

            for (int i=0; i< input[1]; i++)
            {
                var edgeInformation = Console.ReadLine().Split(' ').Select(int.Parse).ToList();
                graph.AddEdge(edgeInformation[0], edgeInformation[1]);
            }

            var pathInformation = Console.ReadLine().Split(' ').Select(int.Parse).ToList();

            Console.WriteLine(graph.CheckConnectivity(pathInformation[0], pathInformation[1]) ? 1 : 0);
        }
    }

    internal class Graph
    {
        private Node[] _nodes;

        public Graph(int nodes)
        {
            _nodes = new Node[nodes];

            for(int i=0; i< _nodes.Length; i++)
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

        public bool CheckConnectivity(int from, int to)
        {
            var visited = new HashSet<Node>();
            var fromNode = _nodes[from - 1];
            var toNode = _nodes[to - 1];

            return Dfs(fromNode, toNode, visited);
        }

        private bool Dfs(Node currentNode, Node to, HashSet<Node> visited)
        {
            visited.Add(currentNode);

            if(currentNode == to)
            {
                return true;
            }

            foreach (var neighbour in currentNode.neighbours)
            {
                if (visited.Contains(neighbour))
                {
                    continue;
                }

                if (Dfs(neighbour, to, visited))
                {
                    return true;
                }
            }

            return false;
        }
    }

    internal class Node
    {
        public List<Node> neighbours = new List<Node>();
    }
}
