using System;
using System.Collections.Generic;

namespace HashingChains
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var numberOfBuckets = int.Parse(Console.ReadLine());
            var numberOfQueries = int.Parse(Console.ReadLine());
            HashTable table = new HashTable(numberOfBuckets);
            var messages = new List<string>();

            for (int i = 0; i < numberOfQueries; i++)
            {
                var input = Console.ReadLine().Split(' ');
                var query = input[0];
                if (query == "add")
                {
                    table.Add(input[1]);
                }
                if (query == "del")
                {
                    table.Delete(input[1]);
                }
                if (query == "find")
                {
                    var word = table.Find(input[1]);
                    if (string.IsNullOrEmpty(word))
                    {
                        messages.Add("no");
                    }
                    else
                    {
                        messages.Add("yes");
                    }
                }
                if (query == "check")
                {
                    var items = table.Check(int.Parse(input[1]));
                    messages.Add(items);
                }
            }

            for (int i = 0; i < messages.Count; i++)
            {
                Console.WriteLine(messages[i]);
            }
        }
    }
}
