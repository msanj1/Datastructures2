using System;
using System.Collections.Generic;
using System.Linq;

namespace MinimumNumberOfFlightSegments
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

            var edgeInformationFromTo = Console.ReadLine().Split(' ').Select(int.Parse).ToList();

            Console.Write(graph.CalculateShortestPath(edgeInformationFromTo[0], edgeInformationFromTo[1]));
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

        public void AddEdge(int from, int to)
        {
            var fromNode = _nodes[from - 1];
            var toNode = _nodes[to - 1];

            fromNode.Neighbours.Add(toNode);
            toNode.Neighbours.Add(fromNode);
        }

        public int CalculateShortestPath(int fromNode, int toNode)
        {
            var distances = new int[_nodes.Length];

            for(int i=0;i < distances.Length; i++)
            {
                distances[i] = int.MaxValue;
            }

            var from = _nodes[fromNode - 1];
            var to = _nodes[toNode - 1];

            var queue = new Queue<Node>();
            queue.Enqueue(from);
            distances[from.Value - 1] = 0;
            var level = -1;
            while(queue.Count > 0)
            {
                int currentQueueCount = queue.Count;
                level++;

                for (int i=0; i< currentQueueCount; i++)
                {
                    var node = queue.Dequeue();

                    if(node == to)
                    {
                        return level;
                    }

                    foreach (var neighbour in node.Neighbours)
                    {
                        if (distances[neighbour.Value - 1] == int.MaxValue)
                        {
                            queue.Enqueue(neighbour);
                            distances[neighbour.Value - 1] = level;
                        }
                    }
                }
            }

            return -1;
        }
    }

    internal class Node
    {
        public Node(int value)
        {
            Value = value;
        }

        public int Value { get; set; }
        public List<Node> Neighbours = new List<Node>();
    }
}
