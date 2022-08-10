using System;
using System.Collections.Generic;
using System.Text;

namespace HashingChains
{
    public class HashTable
    {
        public LinkedList<string>[] items;
        private int _cardinality = 1;
        private long p = 1000000007;
        private int x = 263;
        private int numberOfKeys = 0;

        public HashTable(int cardinality)
        {
            _cardinality = cardinality;
            items = InitialiseWords(cardinality);
        }

        public void Add(string input)
        {
            //if (numberOfKeys / (double)items.Length > 0.9)
            //{
            //    ReHash();
            //}

            var existingWord = Find(input);
            if (existingWord != null)
            {
                return;
            }

            var newHash = BinaryHash(input);
            var chain = items[newHash];
            chain.AddFirst(input);
            numberOfKeys++;
        }

        public void Delete(string input)
        {
            var contact = Find(input);
            if (contact == null)
                return;

            var hash = BinaryHash(input);

            var chain = items[hash];
            chain.Remove(contact);
            numberOfKeys--;
        }

        public string Check(int index)
        {
            StringBuilder output = new StringBuilder();
            var chain = items[index];
            var currentNode = chain.First;
            while (currentNode != null)
            {
                output.Append(currentNode.Value);
                currentNode = currentNode.Next;
                if(currentNode != null)
                {
                    output.Append(" ");
                }
            }

            return output.ToString();
        }

        private void ReHash()
        {
            _cardinality = items.Length * 2;
            var newItems = InitialiseWords(_cardinality);

            foreach (var chain in items)
            {
                var currentItem = chain.First;
                while (currentItem != null)
                {
                    var hash = BinaryHash(currentItem.Value);

                    var newChain = newItems[hash];
                    newChain.AddFirst(currentItem.Value);

                    currentItem = currentItem.Next;
                }
            }

            items = newItems;
        }

        public string Find(string input)
        {
            var hash = BinaryHash(input);

            var chain = items[hash];

            var currentNode = chain.First;
            while (currentNode != null)
            {
                if (currentNode.Value == input)
                {
                    break;
                }
                currentNode = currentNode.Next;
            }

            return currentNode?.Value;
        }

        private LinkedList<string>[] InitialiseWords(int cardinality)
        {
            var words = new LinkedList<string>[cardinality];
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = new LinkedList<string>();
            }

            return words;
        }

        private long BinaryHash(string input)
        {
            long hash = 0;
            for(int i = input.Length - 1; i >= 0; i--)
            {
                var leftTerm = (hash * x + input[i]);
                hash = leftTerm % p;
            }

            return hash % _cardinality;
        }
    }
}
