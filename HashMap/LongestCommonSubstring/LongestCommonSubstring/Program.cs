using System;
using System.Collections.Generic;

namespace LongestCommonSubstring
{
    internal class Answer
    {
        public int Length { get; set; }
        public int I { get; set; }
        public int J { get; set; }
    }
    internal class Program
    {
        const int x = 263;
        static void Main(string[] args)
        {
            const long p = (long)(1e9 + 7);
            const long p2 = (long)(1e9 + 9);
            List<Answer> answers = new List<Answer>();
            while(true)
            {
                var currentInput =  Console.ReadLine();
                if (string.IsNullOrEmpty(currentInput))
                    break;
                var input = currentInput.Split(' ');
                var s = input[0];
                var t = input[1];

                int l = 1;
                int r = Math.Min(s.Length, t.Length);
                string input1 = s;
                string input2 = t;

                var answer = new Answer();
                while (l <= r)
                {
                    int mid = (int)Math.Floor((l + r) / 2.0);
                    Dictionary<long, Hash> hashes = new Dictionary<long, Hash>();

                    var input1WindowHashes = PreComputeHash(input1, mid, p);
                    var input1WindowHashes2 = PreComputeHash(input1, mid, p2);
                    for (int i = 0; i < input1WindowHashes.Length; i++)
                    {
                        if (!hashes.ContainsKey(input1WindowHashes[i]))
                            hashes.Add(input1WindowHashes[i], new Hash { Hash1 = input1WindowHashes[i], Hash2 = input1WindowHashes2[i], Index = i });
                    }
                    bool found = false;
                    var input2WindowHashes = PreComputeHash(input2, mid, p);
                    var input2WindowHashes2 = PreComputeHash(input2, mid, p2);

                    for (int j = 0; j < input2WindowHashes.Length; j++)
                    {
                        long input2Hash = input2WindowHashes[j];
                        if (hashes.ContainsKey(input2Hash))
                        {
                            var hashesFromInput1 = hashes[input2Hash];
                            if (hashesFromInput1.Hash1 == input2WindowHashes[j] && hashesFromInput1.Hash2 == input2WindowHashes2[j]) //to reduce collision
                            {
                                found = true;
                                l = mid + 1;
                                answer.Length = mid;
                                answer.I = hashesFromInput1.Index;
                                answer.J = j;
                            }
                        }
                    }
                    if (found)
                    {
                        l = mid + 1;
                    }
                    else
                    {
                        r = mid - 1;
                    }
                }
                answers.Add(answer);
            }

            foreach (var answer in answers)
            {
                Console.WriteLine($"{answer.I} {answer.J} {answer.Length}");
            }
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

        static long[] PreComputeHash(string input, int patternLength, long p)
        {
            var hashes = new long[input.Length - patternLength + 1]; 
            var lastPattern = input.Substring(input.Length - patternLength, patternLength);
            hashes[input.Length - patternLength] = PolyHash(lastPattern, x, p);
            long y = 1;
            for (int i = 1; i <= patternLength; i++)
            {
                y = (y * x) % p;
            }

            for (int i = input.Length - patternLength - 1; i >= 0; i--)
            {
                var hash = (x * hashes[i + 1] + input[i] - (y * input[i + patternLength])); //rolling hash
                hashes[i] = ((hash % p) + p) % p; 
            }

            return hashes;
        }
    }

    public class Hash
    {
        public long Hash1 { get; set; }
        public long Hash2 { get; set; }
        public int Index { get; set; }
    }
}
