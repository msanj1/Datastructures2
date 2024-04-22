using System;
using System.Collections.Generic;

namespace ConstructTrie
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var inputLength = int.Parse( Console.ReadLine());
            var trie = new Trie();
            for(int i=0; i < inputLength; i++)
            {
                var input = Console.ReadLine();
                trie.Insert(input);
            }

            trie.Print();
        }
    }

    internal class Trie
    {
        private TrieNode _root;
        private static int counter = 0;

        public Trie()
        {
            _root = new TrieNode(0);
        }

        public void Insert(string input)
        {
            var currentNode = _root;
            for(int i=0; i < input.Length; i++)
            {
                var characterInput = input[i];
                if (!currentNode.Children.ContainsKey(characterInput))
                {
                    currentNode.Children[characterInput] = new TrieNode(++Trie.counter);
                }

                currentNode = currentNode.Children[characterInput];
            }

            currentNode.IsEndOfWord = true;
        }

        public void Print()
        {
            //use bfs to print the nodes
            var queue = new Queue<TrieNode>();
            queue.Enqueue(_root);

            while(queue.Count > 0)
            {
                var queueCount = queue.Count;
                for(int i=0; i< queueCount; i++)
                {
                    var node = queue.Dequeue();

                    if (node.Children.Count > 0)
                    {
                        foreach (var child in node.Children)
                        {
                            Console.WriteLine(node.Value + "->" + child.Value.Value + ":" + child.Key);
                            queue.Enqueue(child.Value);
                        }
                    }
                }
            }
        }

    }

    internal class TrieNode
    {
        public int Value { get; private set; }
        public Dictionary<char, TrieNode> Children { get; private set; }
        public bool IsEndOfWord { get; set; }

        public TrieNode(int value)
        {
            Value = value;
            Children = new Dictionary<char, TrieNode>();
        }
    }
}
