using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiplePatternMatching
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var trie = new Trie();
            var input = Console.ReadLine();
            var numberOfOperations = int.Parse(Console.ReadLine());
            
            List<int> output = new List<int>();

            for (int i=0; i< numberOfOperations; i++)
            {
                var pattern = Console.ReadLine();
                trie.Insert(pattern);
            }

            output = trie.Search(input);

            Console.WriteLine(string.Join(" ", output.OrderBy(x => x)));
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
            for (int i = 0; i < input.Length; i++)
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

        private TrieNode SearchNode(TrieNode startingNode, char input)
        {
            if (!startingNode.Children.ContainsKey(input))
            {
                return null;
            }

            return startingNode.Children[input];
        }

        public List<int> Search(string input)
        {
            var result = new List<int>();
            for (int i = 0; i < input.Length; i++)
            {
                var currentNode = _root;
                for (int b = i; b < input.Length; b++)
                {
                    var newNode = SearchNode(currentNode, input[b]);

                    if (newNode == null)
                    {
                        break;
                    }

                    if (newNode.IsEndOfWord)
                    {
                        result.Add(i);
                        break;
                    }

                    currentNode = newNode;
                }
            }

            return result;
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
