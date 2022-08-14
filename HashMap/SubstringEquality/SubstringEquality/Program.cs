using System;
using System.Collections.Generic;
using System.Linq;

namespace SubstringEquality
{
    internal class Program
    {
        static void Main(string[] args)
        {
            long p = (long)(1e9 + 7);
            long p2 = (long)(1e9 + 9);
            int x = 263;
            var input = Console.ReadLine();
            var numberOfQueries = int.Parse(Console.ReadLine());
            var powers = PreComputePowers(input, x, p);
            var powers2 = PreComputePowers(input, x, p2);
            var hashes = PreComputeHashes(input, x, p);
            var hashes2 = PreComputeHashes(input, x, p2);
            var output = new List<string>();

            for (int i = 0; i < numberOfQueries; i++)
            {
                var query = Console.ReadLine().Split(' ').Select(s => int.Parse(s)).ToList();
                var a = query[0];
                var b = query[1];
                var l = query[2];

                var hashA = CalculatePrefixHash(hashes, powers, a, l, x, p);
                var hashA2 = CalculatePrefixHash(hashes2, powers2, a, l, x, p2);

                var hashB = CalculatePrefixHash(hashes, powers, b, l, x, p);
                var hashB2 = CalculatePrefixHash(hashes2, powers2, b, l, x, p2); //𝐻(𝑠𝑎𝑠𝑎+1 · · · 𝑠𝑎+𝑙−1) = ℎ[𝑎 + 𝑙] − 𝑥𝑙ℎ[𝑎] world and l = 5 => h(rld) = h(world) - (x^l * h(wr))

                if (hashA == hashB && hashA2 == hashB2)
                {
                    output.Add("Yes");
                }
                else
                {
                    output.Add("No");
                }
                   
            }

            foreach (var o in output)
            {
                Console.WriteLine(o);
            }   
        }


        static long CalculatePrefixHash(long[] hashes, long[] powers, int startingIndex, int length, int x, long p)
        {
            var xPowerP = (powers[length]);
            long hash = (hashes[startingIndex + length] - xPowerP * hashes[startingIndex]) % p;

            return (hash + p) % p;
        }

        static long[] PreComputeHashes(string input, long x, long p)
        {
            long[] hashes = new long[input.Length + 1];
            for(int i = 1; i <= input.Length; i++)
            {
                hashes[i] = ((x * hashes[i - 1]) + input[i - 1]) % p;
            }

            return hashes;
        }

        static long[] PreComputePowers(string input, long x, long p)
        {
            long[] powers = new long[input.Length + 1];
            powers[0] = 1;

            for (int i = 1; i <= input.Length; i++)
            {
                powers[i] = x * powers[i - 1] % p;
            }

            return powers;
        }
    }
}
