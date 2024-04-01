using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using static BellmanFordCurrencyExchange.Graph;

namespace BellmanFordCurrencyExchange
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

            Console.WriteLine(graph.EvaluateIfNegativeCyclesExist() ? 1 : 0);
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

        public bool EvaluateIfNegativeCyclesExist()
        {
            for(int i=0; i < _nodes.Length; i++)
            {
                var affectedNodes = RunBellManFord();
                if(i == _nodes.Length - 1)
                {
                    if(affectedNodes.Count > 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private List<Node> RunBellManFord()
        {
            var nodesUpdates = new List<Node>();
            for (int i = 0; i < _nodes.Length; i++)
            {
                var currentNode = _nodes[i];
                if(currentNode.Distance == int.MaxValue)
                {
                    currentNode.Distance = 0;
                    nodesUpdates.Add(currentNode);
                }

                foreach (var edge in currentNode.Edges)
                {
                    var newWeight = currentNode.Distance + edge.Weight;

                    if (newWeight < edge.Node.Distance)
                    {
                        edge.Node.Distance = newWeight;
                        edge.Node.PreviousNode = currentNode;
                        nodesUpdates.Add(edge.Node);
                    }
                }
            }

            return nodesUpdates;
        }

        internal class Node
        {
            public Node(int value)
            {
                Value = value;
            }
            public int Distance { get; set; } = int.MaxValue;
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
            public int Weight { get; set; } = int.MaxValue;
            public Node Node { get; private set; }
        }
    }
}
