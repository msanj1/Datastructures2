using System;
using System.Collections.Generic;
using System.Linq;

namespace SubstringEquality
{
    internal class Program
    {
        static void Main(string[] args)
        {
            long p = 1000000007;
            int x = 263;
            var input = Console.ReadLine();
            var numberOfQueries = int.Parse(Console.ReadLine());
            var hashes = PreComputeHashes(input, x, p);
            var output = new List<string>();

            for (int i = 0; i < numberOfQueries; i++)
            {
                var query = Console.ReadLine().Split(" ").Select(x => int.Parse(x)).ToList();
                var a = query[0];
                var b = query[1];
                var l = query[2];

                var hashA = hashes[a + l] - ((Math.Pow(x, l) * hashes[a]) % p);
                var hashB = hashes[b + l] - ((Math.Pow(x, l) * hashes[b]) % p);
                if (hashA == hashB)
                    output.Add("Yes");
                else
                    output.Add("No");
            }

            foreach (var o in output)
            {
                Console.WriteLine(o);
            }
        }

        static long[] PreComputeHashes(string input, int x, long p)
        {
            long[] hashes = new long[input.Length + 1];
            for(int i= 1; i <= input.Length; i++)
            {
                hashes[i] = ((x * hashes[i - 1]) + input[i - 1]) % p;
            }

            return hashes;
        }

        static long PolyHash(string input, int x, long p)
        {
            long hash = 0;
            for (int i = input.Length - 1; i >= 0; i--)
            {
                hash = (hash * x + input[i]) % p;
            }

            return hash;
        }
    }
}
