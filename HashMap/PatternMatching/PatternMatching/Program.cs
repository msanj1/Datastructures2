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
        const long p = (long)(1e9 + 7);
        static void Main(string[] args)
        {
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
                var inputHashes = PreComputeHashes(input);
                var inputPowers = PreComputePowers(input);

                var patternHashes = PreComputeHashes(pattern);
                var patternPowers = PreComputePowers(pattern);
                for (int i = 0; i <= input.Length - pattern.Length; i++)
                {
                    var numberOfMismatches = IsEqual(i, 0, pattern.Length - 1, 0, inputHashes, inputPowers, patternHashes, patternPowers);
                    if (numberOfMismatches <= numberOfMismatchesAllowed)
                    {
                        answer.MistmatchCount++;
                        answer.Indexes.Add(i);
                    }
                }
                    answers.Add(answer);
            }

            foreach (var answer in answers)
            {
                Console.WriteLine($"{answer.MistmatchCount} {string.Join(" ",answer.Indexes)}");
            }
        }

        static int IsEqual(int i, int l, int r, int mismatchCount, long[] inputHashes, long[] inputPowers, long[] patternHashes, long[] patternPowers)
        {
            if (l <= r)
            {
                int mid = l + (r - l) / 2;

                if (CalculatePrefixHash(inputHashes, inputPowers, mid + i, 1) != CalculatePrefixHash(patternHashes, patternPowers, mid, 1))
                {
                    mismatchCount++;
                }

                if (l <= mid -1 && CalculatePrefixHash(inputHashes, inputPowers, l + i, mid - l) != CalculatePrefixHash(patternHashes, patternPowers, l, mid - l))
                {
                    mismatchCount = IsEqual(i,l, mid - 1, mismatchCount, inputHashes, inputPowers, patternHashes, patternPowers);
                }

                if (mid + 1 <= r && CalculatePrefixHash(inputHashes, inputPowers, mid + i + 1, r - mid) != CalculatePrefixHash(patternHashes, patternPowers, mid + 1, r - mid))
                {
                    mismatchCount = IsEqual(i, mid + 1, r, mismatchCount, inputHashes, inputPowers, patternHashes, patternPowers);
                }
            }

            return mismatchCount;
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

        static long CalculatePrefixHash(long[] hashes, long[] powers, int startingIndex, int length)
        {
            var xPowerP = (powers[length]);
            long hash = (hashes[startingIndex + length] - xPowerP * hashes[startingIndex]) % p;

            return (hash + p) % p;
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
    }
}
