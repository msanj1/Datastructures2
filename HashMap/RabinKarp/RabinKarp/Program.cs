using System;
using System.Collections.Generic;

namespace RabinKarp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            long p = 1000000007;
            int x = 263;
            var pattern = Console.ReadLine();
            var input = Console.ReadLine();
            var matchedIndex = new List<string>();
            var patternHash = PolyHash(pattern, x, p);
            var preComputedHash = PreComputeHash(input, pattern, x, p);

            for (int i = 0; i <= input.Length - pattern.Length; i++) // O(input.length - pattern.length + 1)
            {
                if (patternHash != preComputedHash[i]) // O(input.length - pattern.length + 1) * O(pattern.Length)
                {
                    continue;
                }

                var inputValue = input.Substring(i, pattern.Length); // O(pattern.Length)

                if (AreEqual(inputValue, pattern)) // O(pattern.Length)
                {
                    matchedIndex.Add(i.ToString());
                }
            }

            Console.WriteLine(String.Join(" ",matchedIndex));
        }

        static long[] PreComputeHash(string input, string pattern, int x, long p)
        {
            var hashes = new long[input.Length - pattern.Length + 1]; //world wo
            var lastPattern = input.Substring(input.Length - pattern.Length, pattern.Length);
            hashes[input.Length - pattern.Length] = PolyHash(lastPattern, x, p);
            long y = 1;
            for (int i = 1; i <= pattern.Length; i++)
            {
                y = (y * x) % p;
            }

            for(int i = input.Length - pattern.Length - 1; i >= 0; i--)
            {
                hashes[i] = (((x * hashes[i + 1] + input[i] - (y * input[i + pattern.Length])) % p) + p) % p;
            }

            return hashes;
        }

        static bool AreEqual(string input1, string input2)
        {
            if (input1.Length != input1.Length)
                return false;

            for(int i=0; i< input1.Length; i++)
            {
                if (input1[i] != input2[i])
                    return false;
            }

            return true;
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
