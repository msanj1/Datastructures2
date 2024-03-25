using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace MinimumCostOfFlight
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

            var edgeInformationFromTo = Console.ReadLine().Split(' ').Select(int.Parse).ToList();

            Console.Write(graph.ShortestPath(edgeInformationFromTo[0], edgeInformationFromTo[1]));
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

        public void AddEdge(int from, int to, int weight)
        {
            var fromNode = _nodes[from - 1];
            var toNode = _nodes[to - 1];

            var toEdge = new Edge(toNode);
            toEdge.Weight = weight;

            fromNode.Edges.Add(toEdge);
        }

        public int ShortestPath(int from, int to)
        {
            var fromNode = _nodes[from - 1];
            var toNode = _nodes[to - 1];

            fromNode.Distance = 0;

            var heap = new Heap<Node>();
            heap.BuildHeap(_nodes);

            while(heap.Count > 0)
            {
               var currentNode = heap.Extract();
                if (currentNode.Distance == int.MaxValue)
                    continue;
                foreach(var edge in currentNode.Edges)
                {
                    var newWeight = currentNode.Distance + edge.Weight;

                    if (edge.Node.Distance > newWeight)
                    {
                        edge.Node.Distance = newWeight;
                        heap.ChangePriority(edge.Node);
                    }
                }
            }

            return toNode.Distance == int.MaxValue ? -1 : toNode.Distance;
        }
    }

    internal class Node: IComparable<Node>
    {
        public int Distance { get; set; } = int.MaxValue;
        public List<Edge> Edges = new List<Edge>();

        public int CompareTo([AllowNull] Node other)
        {
            if(this.Distance == other.Distance)
            {
                return 0;
            }
            else if (this.Distance > other.Distance)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
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

        public void ChangePriority(T node)
        {
            var index = heapMap[node];
            var parentIndex = GetParent(index);

            if(parentIndex >= 0 && _heap[parentIndex].CompareTo(_heap[index]) > 0) // Index 1, ParentIndex 2
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
