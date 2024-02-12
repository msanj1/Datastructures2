using System;
using System.Collections.Generic;
using System.Linq;

namespace Cycle
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

            Console.WriteLine(graph.CheckForCycle() ? 1 : 0);
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
        }

        public bool CheckForCycle()
        {
            var visited = new HashSet<Node>();

            return Dfs(visited);
        }

        private bool Dfs(HashSet<Node> visited)
        {
            for (int i=0;i< _nodes.Length; i++)
            {
                var node = _nodes[i];
                var pathTrace = new HashSet<Node>();
                if (!visited.Contains(node))
                {
                    var cycleFound = Explore(node, visited, pathTrace);
                    if (cycleFound)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool Explore(Node currentNode, HashSet<Node> visited, HashSet<Node> pathTrace)
        {
            visited.Add(currentNode);
            pathTrace.Add(currentNode);

            foreach (var neighbour in currentNode.neighbours)
            {
                if (pathTrace.Contains(neighbour))
                {
                    return true;
                }

                if (visited.Contains(neighbour))
                {
                    continue;
                }

                if (Explore(neighbour, visited, pathTrace))
                {
                    return true;
                }
            }

            pathTrace.Remove(currentNode);

            return false;
        }
    }

    internal class Node
    {
        public List<Node> neighbours = new List<Node>();
    }
}
