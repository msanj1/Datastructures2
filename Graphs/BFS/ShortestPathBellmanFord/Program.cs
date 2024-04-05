using System;
using System.Collections.Generic;
using System.Linq;

namespace ShortestPathBellmanFord
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
                graph.AddEdge(edgeInformation[0], edgeInformation[1], edgeInformation[2]);
            }

            var startingNode = int.Parse(Console.ReadLine());
            graph.Print(startingNode);
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
                _nodes[i] = new Node(i + 1);
            }
        }

        public void AddEdge(int from, int to, int weight)
        {
            var fromNode = _nodes[from - 1];
            var toNode = _nodes[to - 1];

            var toEdge = new Edge(toNode);
            toEdge.Weight = weight;

            fromNode.Edges.Add(toEdge);
        }

        public void Print(int startingNodeIndex)
        {
            var allNodesLastUpdate = RunBellmanFord(startingNodeIndex);

            var allNodes = new HashSet<Node>();

            var visisted = new HashSet<Node>();
            foreach (var node in allNodesLastUpdate)
            {
                if (visisted.Contains(node))
                {
                    continue;
                }

                foreach (var item in Bfs(node, visisted))
                {
                    allNodes.Add(item);
                }
            }

            for(int i=0; i< _nodes.Length; i++)
            {
                var node = _nodes[i];
                if (allNodes.Contains(node))
                {
                    Console.WriteLine("-");
                }else if (node.Distance == long.MaxValue)
                {
                    Console.WriteLine("*");
                }
                else
                {
                    Console.WriteLine(node.Distance);
                }
            }
        }

        public List<Node> Bfs(Node node, HashSet<Node> visited)
        {
            var result = new List<Node>();
            var queue = new Queue<Node>();
            queue.Enqueue(node);
            visited.Add(node);

            while (queue.Count > 0)
            {
                var currentNode = queue.Dequeue();
                result.Add(currentNode);

                foreach (var edge in currentNode.Edges)
                {
                    if (visited.Contains(edge.Node))
                    {
                        continue;
                    }

                    visited.Add(edge.Node);
                    queue.Enqueue(edge.Node);
                }
            }

            return result;
        }

        public List<Node> RunBellmanFord(int startingNodeIndex)
        {
            var startingNode = _nodes[startingNodeIndex - 1];
            startingNode.Distance = 0;
            for (int i = 0; i < _nodes.Length; i++)
            {
                var affectedNode = RelaxAllEdges(startingNode);
                if (i == _nodes.Length - 1)
                {
                    if (affectedNode != null)
                    {
                        return affectedNode;
                    }
                }
            }

            return null;
        }

        private List<Node> RelaxAllEdges(Node startingNode)
        {
            List<Node> lastUpdateNode = new List<Node>();
            for (int i = 0; i < _nodes.Length; i++)
            {
                var currentNode = _nodes[i];

                if(currentNode.Distance == long.MaxValue)
                {
                    continue;
                }

                foreach (var edge in currentNode.Edges)
                {
                    var newWeight = currentNode.Distance + edge.Weight;

                    if (newWeight < edge.Node.Distance)
                    {
                        edge.Node.Distance = newWeight;
                        edge.Node.PreviousNode = currentNode;
                        lastUpdateNode.Add(edge.Node);
                    }
                }
            }

            return lastUpdateNode;
        }

        internal class Node
        {
            public Node(int value)
            {
                Value = value;
            }
            public long Distance { get; set; } = long.MaxValue;
            public List<Edge> Edges = new List<Edge>();
            public Node PreviousNode { get; set; }
            public int Value { get; private set; }
        }

        internal class Edge
        {
            public Edge(Node node)
            {
                Node = node;
            }
            public long Weight { get; set; } = long.MaxValue;
            public Node Node { get; private set; }
        }
    }
}
