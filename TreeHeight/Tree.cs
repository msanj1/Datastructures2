using System;
using System.Collections.Generic;

namespace TreeHeight
{
    public class Tree
    {
        public List<Node> Nodes { get; set; }
        private Node _rootNode;

        public Tree(int numberOfNodes, int[] nodeRelationships)
        {
            Nodes = new List<Node>(numberOfNodes);
            for (var index = 0; index < numberOfNodes; index++)
            {
                Nodes.Add(new Node(){});
            }

            for (var index = 0; index < nodeRelationships.Length; index++)
            {
                var childNode = Nodes[index];
                
                var parentNodeIndex = nodeRelationships[index];
             
                if (parentNodeIndex == -1)
                {
                    _rootNode = childNode;
                }
                else
                {
                    var parentNode = Nodes[parentNodeIndex];
                    parentNode.Children.Add(childNode);
                }
            }
        }

        public int GetHeight()
        {
           return CalculateHeight(_rootNode);
        }

        public int CalculateHeight(Node node, int currentHeight = -1)
        {
            if (node == null)
            {
                return -1;
            }

            for (var index = 0; index < node.Children.Count; index++)
            {
                var childNode = node.Children[index];
                childNode.Height = node.Height + 1;
                currentHeight = CalculateHeight(childNode, currentHeight);
            }

            currentHeight = Math.Max(node.Height, currentHeight);

            return currentHeight;

        }
    }
}