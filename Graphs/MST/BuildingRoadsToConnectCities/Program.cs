using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using static ShortestPathBellmanFord.Graph;

namespace ShortestPathBellmanFord
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine().Split(' ').Select(int.Parse).ToList();

            var points = new List<Point>();

            for (int i = 0; i < input[0]; i++)
            {
                var pointInformation = Console.ReadLine().Split(' ').Select(long.Parse).ToList();

                var point = new Point(pointInformation[0], pointInformation[1]);
                points.Add(point);
            }

            var graph = new Graph(points);
            graph.CreateConnections();

            //graph.RunPrimsAlgorithm();
            Console.WriteLine(graph.RunKruskalsAlgorithm(graph).ToString("N10"));
            //Console.WriteLine(graph.CalculateTotalDistance().ToString("F10"));
        }
    }

    public static class Utility
    {
        public static double Weight(long x1, long y1, long x2, long y2)
        {
            return Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
        }
    }

    public class Point
    {
        public long X { get; private set; }
        public long Y { get; private set; }
        public Point(long x, long y)
        {
            X = x;

            Y = y;

        }
    }

    public class Graph
    {
        private Node[] _nodes;
        private List<Edge> _edges;

        public Graph(List<Point> nodes)
        {
            _nodes = new Node[nodes.Count];

            for (int i = 0; i < _nodes.Length; i++)
            {
                _nodes[i] = new Node(nodes[i]);
            }

            _edges = new List<Edge>();
        }

        public List<Edge> Edges => _edges;

        public void AddEdge(int from, int to, decimal weight)
        {
            var fromNode = _nodes[from - 1];
            var toNode = _nodes[to - 1];

            var toEdge = new Edge(fromNode,toNode);
            toEdge.Weight = weight;
            fromNode.Edges.Add(toEdge);

            var fromEdge = new Edge(fromNode, toNode);
            fromEdge.Weight = weight;
            toNode.Edges.Add(fromEdge);

            _edges.Add(toEdge);
            _edges.Add(fromEdge);
        }

        public void CreateConnections()
        {
            for(int i=0; i < _nodes.Length; i++)
            {
                var currentNode = _nodes[i];

                for(int b=0; b < _nodes.Length; b++)
                {
                    if(i == b)
                    {
                        continue;
                    }

                    var nextNode = _nodes[b];
                    decimal weight = (decimal)Utility.Weight(currentNode.Value.X,currentNode.Value.Y, nextNode.Value.X, nextNode.Value.Y);
                    AddEdge(i+1, b+1, weight);
                }
            }
        }

        public void RunPrimsAlgorithm()
        {
            var firstNode = _nodes[0];
            firstNode.Distance = 0;

            var priorityQueue = new Heap<Node>();
            priorityQueue.BuildHeap(_nodes);

            while(priorityQueue.Count > 0)
            {
                var currentNode = priorityQueue.Extract();

                foreach(var edge in currentNode.Edges)
                {
                    if(priorityQueue.Contains(edge.ToNode) && edge.Weight < edge.ToNode.Distance)
                    {
                        edge.ToNode.Distance = edge.Weight;
                        edge.ToNode.PreviousNode = currentNode;
                        priorityQueue.ChangePriority(edge.ToNode);
                    }
                }
            }
        }

        public decimal RunKruskalsAlgorithm(Graph graph)
        {
            var disjointedSet = new DisjointedSet();
            disjointedSet.MakeSet(_nodes.ToList());

            var heap = new Heap<Edge>();
            heap.BuildHeap(graph.Edges.ToArray());

            decimal totalDistance = 0;

            while(heap.Count > 0)
            {
                var edge = heap.Extract();
                var fromNode = edge.FromNode;
                var toNode = edge.ToNode;

                var parentA = disjointedSet.Find(fromNode);
                var parentB = disjointedSet.Find(toNode);

                if (parentA != parentB)
                {
                    totalDistance += edge.Weight;
                    disjointedSet.Union(parentA, parentB);
                }
            }

            return totalDistance;
        }

        public decimal CalculateTotalDistance()
        {
            decimal totalDistance = 0;

            for(int i=0; i < _nodes.Length; i++)
            {
                var currentNode = _nodes[i];
                
                totalDistance += currentNode.Distance;
            }

            return totalDistance;
        }

        public class DisjointedSet
        {
            private List<Node> _nodes = new List<Node>();
            private Dictionary<Node, int> _map = new Dictionary<Node, int>();

            private int[] _parents;
            private int[] _ranks;

            public void MakeSet(List<Node> nodes)
            {
                _nodes = nodes;
                _parents = new int[_nodes.Count];
                _ranks = new int[_nodes.Count];

                for (int i = 0; i < _nodes.Count; i++)
                {
                    _parents[i] = i;
                    _ranks[i] = 0;
                    _map[_nodes[i]] = i;
                }
            }

            public int Find(int nodeIndex)
            {
                if (_parents[nodeIndex] != nodeIndex)
                {
                    _parents[nodeIndex] = Find(_parents[nodeIndex]);
                }

                return _parents[nodeIndex];
            }

            public int Find(Node node)
            {
                return Find(_map[node]);
            }

            public void Union(int nodeIndex1, int nodeIndex2)
            {
                var root1 = Find(nodeIndex1);
                var root2 = Find(nodeIndex2);

                if (root1 != root2)
                {
                    if (_ranks[root1] > _ranks[root2])
                    {
                        _parents[root2] = root1;
                    }
                    else if (_ranks[root1] < _ranks[root2])
                    {
                        _parents[root1] = root2;
                    }
                    else
                    {
                        _parents[root1] = root2;
                        _ranks[root2]++;
                    }
                }
            }
        }

        public class Node : IComparable<Node>
        {
            public Node(Point value)
            {
                Value = value;
            }
            public decimal Distance { get; set; } = decimal.MaxValue;
            public List<Edge> Edges = new List<Edge>();
            public Node PreviousNode { get; set; }
            public Point Value { get; private set; }

            public int CompareTo(Node other)
            {
                return Distance.CompareTo(other.Distance);
            }
        }

        public class Edge : IComparable<Edge>
        {
            public Edge(Node fromNode, Node toNode)
            {
                FromNode = fromNode;
                ToNode = toNode;
            }
            public decimal Weight { get; set; } = decimal.MaxValue;
            public Node FromNode { get; private set; }
            public Node ToNode { get; private set; }

            public int CompareTo(Edge other)
            {
                return Weight.CompareTo(other.Weight);
            }
        }

        public class Heap<T>
    where T : IComparable<T>
        {
            private List<T> _heap = new List<T>();
            private Dictionary<T, int> heapMap = new Dictionary<T, int>();

            public void BuildHeap(T[] inputArray)
            {
                var startingIndex = (int)Math.Floor(inputArray.Length / 2.0) - 1;
                _heap = new List<T>(inputArray);

                for (var i = 0; i < _heap.Count; i++)
                {
                    heapMap[_heap[i]] = i;
                }

                for (var i = startingIndex; i >= 0; i--)
                {
                    SiftDown(i);
                }
            }

            public int Count => _heap.Count;

            public T Extract()
            {
                var value = _heap[0];
                Swap(0, _heap.Count - 1);
                _heap.RemoveAt(_heap.Count - 1);
                heapMap.Remove(value);

                SiftDown(0);

                return value;
            }

            public bool Contains(T value)
            {
                return heapMap.ContainsKey(value);
            }

            public void ChangePriority(T node)
            {
                var index = heapMap[node];
                var parentIndex = GetParent(index);

                if (parentIndex >= 0 && _heap[parentIndex].CompareTo(_heap[index]) > 0) // Index 1, ParentIndex 2
                {
                    SiftUp(index);
                }
                else
                {
                    SiftDown(index);
                }
            }

            private void SiftUp(int index)
            {
                var parentIndex = GetParent(index);
                if (parentIndex >= 0)
                {
                    var parentValue = _heap[parentIndex];
                    if (_heap[index].CompareTo(parentValue) == -1) // _heap[index] < parentValue
                    {
                        Swap(index, parentIndex);
                        SiftUp(parentIndex);
                    }
                }
            }

            private void SiftDown(int index)
            {
                var leftChildIndex = GetLeftChild(index);
                var rightChildIndex = GetRightChild(index);
                T leftChildValue = default(T);
                T rightChildvalue = default(T);
                if (leftChildIndex < _heap.Count) leftChildValue = _heap[leftChildIndex];

                if (rightChildIndex < _heap.Count) rightChildvalue = _heap[rightChildIndex];

                if (leftChildValue != null && rightChildvalue != null)
                {
                    if (_heap[index].CompareTo(leftChildValue) == 1 && _heap[index].CompareTo(rightChildvalue) <= 0)
                    {
                        //swap with the leftchildvalue
                        Swap(index, leftChildIndex);
                        SiftDown(leftChildIndex);
                    }
                    else if (_heap[index].CompareTo(rightChildvalue) == 1 && _heap[index].CompareTo(leftChildValue) <= 0)
                    {
                        //swap with the rightchildvalue
                        Swap(index, rightChildIndex);
                        SiftDown(rightChildIndex);
                    }
                    else if (_heap[index].CompareTo(leftChildValue) == 1 && _heap[index].CompareTo(rightChildvalue) == 1)
                    {
                        if (leftChildValue.CompareTo(rightChildvalue) <= 0)
                        {
                            Swap(index, leftChildIndex);
                            SiftDown(leftChildIndex);
                            //swap with leftChildValue
                        }
                        else
                        {
                            Swap(index, rightChildIndex);
                            SiftDown(rightChildIndex);
                        }
                    }
                }
                else if (leftChildValue != null)
                {
                    if (_heap[index].CompareTo(leftChildValue) == 1)
                    {
                        Swap(index, leftChildIndex);
                        SiftDown(leftChildIndex);
                        //swap with leftChildValue
                    }
                }
                else if (rightChildvalue != null)
                {
                    if (_heap[index].CompareTo(rightChildvalue) == 1)
                    {
                        Swap(index, rightChildIndex);
                        SiftDown(rightChildIndex);
                        //swap with rightChildvalue
                    }
                }
            }

            private void Swap(int indexA, int indexB)
            {
                var temp = _heap[indexA];
                _heap[indexA] = _heap[indexB];
                _heap[indexB] = temp;

                heapMap[_heap[indexA]] = indexA;
                heapMap[_heap[indexB]] = indexB;
            }

            private int GetParent(int index)
            {
                return (int)Math.Floor((index - 1) / 2.0);
            }

            private int GetLeftChild(int index)
            {
                return 2 * index + 1;
            }

            private int GetRightChild(int index)
            {
                return 2 * index + 2;
            }
        }
    }
}
