using System;
using System.Collections.Generic;
using System.Text;

namespace Traversals
{
    internal class BinaryTree
    {
        private readonly List<Node> _nodes;
        private int _currentIndex;
        public BinaryTree(int numberOfNodes)
        {
            _nodes = new List<Node>(numberOfNodes);
            for(int i=0; i< numberOfNodes; i++)
            {
                _nodes.Add(new Node());
            }
        }

        public void Add(int key, int leftIndex, int rightIndex)
        {
            var node = _nodes[_currentIndex];
            node.Key = key;

            if (leftIndex != -1)
            {
                var leftNode = _nodes[leftIndex];
                node.Left = leftNode;
            }

            if (rightIndex != -1)
            {
                var rightNode = _nodes[rightIndex];
                node.Right = rightNode;
            }

            _currentIndex++;
        }


        public List<Node> TraverseInOrder()
        {
            var nodes = new List<Node>();
            TraverseInOrder(_nodes[0], nodes);
            return nodes;
        }

        public List<Node> TraversePreOrder()
        {
            var nodes = new List<Node>();
            TraversePreOrder(_nodes[0], nodes);
            return nodes;
        }

        public List<Node> TraversePostOrder()
        {
            var nodes = new List<Node>();
            TraversePostOrder(_nodes[0], nodes);
            return nodes;
        }

        private void TraverseInOrder(Node node, List<Node> output)
        {
            if (node == null)
                return;

            TraverseInOrder(node.Left, output);

            output.Add(node);

            TraverseInOrder(node.Right, output);
        }

        private void TraversePreOrder(Node node, List<Node> output)
        {
            if (node == null)
                return;

            output.Add(node);

            TraversePreOrder(node.Left, output);

            TraversePreOrder(node.Right, output);
        }

        private void TraversePostOrder(Node node, List<Node> output)
        {
            if (node == null)
                return;

            TraversePostOrder(node.Left, output);

            TraversePostOrder(node.Right, output);

            output.Add(node);
        }
    }
}
