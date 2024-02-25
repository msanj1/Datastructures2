using System;
using System.Collections.Generic;
using System.Linq;

namespace StronglyConnectedComponents
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

            Console.WriteLine(graph.GetStronglyConnectedComponents().Count);
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

            fromNode.Neighbours.Add(toNode);
            toNode.NeighboursInReverse.Add(fromNode);
        }

        public List<List<Node>> GetStronglyConnectedComponents()
        {
            var output = new List<List<Node>>();

            var visited = new HashSet<Node>();
            int counter = 0;
            for (int i = 0; i < _nodes.Length; i++)
            {
                var currentNode = _nodes[i];
                if (visited.Contains(currentNode))
                {
                    continue;
                }

                counter = ExploreInReverse(currentNode, counter, visited);
            }

            var nodesOrdered = _nodes.OrderByDescending(x => x.PostOrderCounter).ToArray();
            visited.Clear();

            for (int i = 0; i < nodesOrdered.Length; i++)
            {
                var nodesInpath = new List<Node>();
                var currentNode = nodesOrdered[i];
                if (visited.Contains(currentNode))
                {
                    continue;
                }

                Explore(currentNode, visited, nodesInpath);
                output.Add(nodesInpath);
            }

            return output;
        }

        private void Explore(Node node, HashSet<Node> visited, List<Node> nodesInPath)
        {
            visited.Add(node);
            nodesInPath.Add(node);

            for(int i=0; i< node.Neighbours.Count; i++)
            {
                var currentNode = node.Neighbours[i];
                if (visited.Contains(currentNode))
                {
                    continue;
                }

                Explore(currentNode, visited, nodesInPath);
            }
        }

        private int ExploreInReverse(Node node, int counter, HashSet<Node> visited)
        {
            visited.Add(node);

            node.PreOrderCounter = counter++;

            for (int i = 0; i < node.NeighboursInReverse.Count; i++)
            {
                var neighbour = node.NeighboursInReverse[i];
                if (visited.Contains(neighbour))
                {
                    continue;
                }

                counter = ExploreInReverse(neighbour, counter, visited);
            }
            
            node.PostOrderCounter = counter++;

            return counter;
        }

        private void Explore(Node node, HashSet<Node> visited)
        {
            visited.Add(node);

            for (int i = 0; i < node.Neighbours.Count; i++)
            {
                var neighbour = node.Neighbours[i];
                if (visited.Contains(neighbour) || neighbour.Removed)
                {
                    continue;
                }

                Explore(neighbour, visited);
            }
        }
    }

    internal class Node
    {
        public Node(int value)
        {
            Value = value;
        }

        public int Value { get; set; }
        public int PreOrderCounter { get; set; }
        public int PostOrderCounter { get; set; }
        public List<Node> Neighbours = new List<Node>();
        public List<Node> NeighboursInReverse = new List<Node>();
        public bool Removed { get; set; }
    }
}
