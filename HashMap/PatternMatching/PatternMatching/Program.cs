using System;
using System.Collections.Generic;

namespace LongestCommonSubstring
{
    internal class Answer
    {
        public int MistmatchCount { get; set; }
        public List<int> Indexes { get; set; } = new List<int>();
    }
    internal class Program
    {
        const int x = 263;
        static void Main(string[] args)
        {
            const long p = (long)(1e9 + 7);
            List<Answer> answers = new List<Answer>();
            while (true)
            {
                var currentInput = Console.ReadLine();
                if (string.IsNullOrEmpty(currentInput))
                    break;
                int numberOfMismatchesAllowed = int.Parse(currentInput.Split(' ')[0]);
                string input = currentInput.Split(' ')[1];
                string pattern = currentInput.Split(' ')[2];
                var answer = new Answer();
                for (int i=0; i <= input.Length - pattern.Length; i++)
                {
                    var currentTerm = input.Substring(i, pattern.Length);
                    if(IsEqual(currentTerm, pattern) <= numberOfMismatchesAllowed)
                    {
                        answer.Indexes.Add(i);
                        answer.MistmatchCount++;
                    }
                }
                answers.Add(answer);
            }

            foreach (var answer in answers)
            {
                Console.WriteLine($"{answer.MistmatchCount} {string.Join(" ",answer.Indexes)}");
            }
        }

        static int IsEqual(string input1, string input2)
        {
            int numberOfMismatches = 0;
            for (int j = 0; j < input1.Length; j++)
            {
                if (input1[j] != input2[j])
                {
                    numberOfMismatches++;
                }
            }

            return numberOfMismatches;
        }

        static long[] PreComputeHashes(string input, long x, long p)
        {
            long[] hashes = new long[input.Length + 1];
            for (int i = 1; i <= input.Length; i++)
            {
                hashes[i] = ((x * hashes[i - 1]) + input[i - 1]) % p;
            }

            return hashes;
        }

        static long CalculatePrefixHash(long[] hashes, long[] powers, int startingIndex, int length, int x, long p)
        {
            var xPowerP = (powers[length]);
            long hash = (hashes[startingIndex + length] - xPowerP * hashes[startingIndex]) % p;

            return (hash + p) % p;
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
