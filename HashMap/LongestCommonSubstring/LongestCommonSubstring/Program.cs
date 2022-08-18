using System;
using System.Collections.Generic;

namespace LongestCommonSubstring
{
    internal class Program
    {
        const int x = 263;
        const long p = (long)(1e9 + 7);
        static void Main(string[] args)
        {
            var input = Console.ReadLine().Split(' ');

            var s = input[0];
            var t = input[1];

            var answer = new Answer();

            int l = 1;
            int r = Math.Min(s.Length, t.Length);
            string input1 = s;
            string input2 = t;

            var sHashes = PreComputeHashes(input1);
            var sPowers = PreComputePowers(input1);
            var tHashes = PreComputeHashes(input2);
            var tPowers = PreComputePowers(input2);
            while (l < r)
            {
                int mid = (l + r)/ 2;
                Dictionary<long, int> hashes = new Dictionary<long, int>();
                for (int i = 0; i <= input1.Length - mid; i++)
                {
                    var m = input1.Substring(i, mid);
                    var hash = CalculatePrefixHash(sHashes, sPowers, i, mid);
                    if(!hashes.ContainsKey(hash))
                        hashes.Add(hash, i);
                }
                bool found = false;
                for (int j = 0; j <= input2.Length - mid; j++)
                {
                    var m = input2.Substring(j, mid);

                    var hash = CalculatePrefixHash(tHashes, tPowers, j, mid);
                    if (hashes.ContainsKey(hash))
                    {
                        found = true;
                        var i = hashes[hash];
                        l = mid + 1;
                        answer.Length = mid;
                        answer.I = i;
                        answer.J = j;
                        break;
                    }
                }
                if (!found)
                {
                    r = mid - 1;
                }
                
            }

            Console.WriteLine($"{answer.I} {answer.J} {answer.Length}");
        }

        static long[] PreComputeHashes(string input)
        {
            long[] hashes = new long[input.Length + 1];
            for (int i = 1; i <= input.Length; i++)
            {
                hashes[i] = ((x * hashes[i - 1]) + input[i - 1]) % p;
            }

            return hashes;
        }

          static long[] PreComputePowers(string input)
        {
            long[] powers = new long[input.Length + 1];
            powers[0] = 1;

            for (int i = 1; i <= input.Length; i++)
            {
                powers[i] = x * powers[i - 1] % p;
            }

            return powers;
        }

        static long CalculatePrefixHash(long[] hashes, long[] powers, int startingIndex, int length)
        {
            var xPowerP = (powers[length]);
            long hash = (hashes[startingIndex + length] - xPowerP * hashes[startingIndex]) % p;

            return (hash + p) % p;
        }
    }
}
