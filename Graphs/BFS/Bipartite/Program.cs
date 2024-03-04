using System;
using System.Collections.Generic;
using System.Linq;

namespace Bipartite
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

            Console.Write(graph.CheckIsBipartite() ? 1 : 0);

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

            fromNode.Neighbours.Add(toNode);
            toNode.Neighbours.Add(fromNode);
        }

        public bool CheckIsBipartite()
        {
            var visited = new HashSet<Node>();
            var isBipartite = true;
            for(int i=0; i< _nodes.Length; i++)
            {
                var currentNode = _nodes[i];
                if (!visited.Contains(currentNode))
                {
                    isBipartite &= EvaluateIsBipartite(currentNode, visited);
                }
            }

            return isBipartite;
        }

        private bool EvaluateIsBipartite(Node node, HashSet<Node> visited)
        {
            visited.Add(node);
            node.Segment = 0;

            var queue = new Queue<Node>();
            queue.Enqueue(node);

            while(queue.Count > 0)
            {
                var queueCount = queue.Count;
                for(int i=0;i< queueCount; i++)
                {
                    var currentNode = queue.Dequeue();

                    foreach (Node neighbourNode in currentNode.Neighbours)
                    {
                        if (visited.Contains(neighbourNode))
                        {
                            if (neighbourNode.Segment == currentNode.Segment)
                            {
                                return false;
                            }
                            continue;
                        }

                        neighbourNode.Segment = currentNode.Segment == 0 ? 1 : 0;
                        queue.Enqueue(neighbourNode);
                        visited.Add(neighbourNode);
                    }
                }
            }

            return true;
        }
    }

    internal class Node
    {
        public int Segment { get; set; } = -1;

        public List<Node> Neighbours = new List<Node>();
    }
}
