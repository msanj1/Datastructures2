using System;

namespace LongestCommonSubstring
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine().Split(' ');
            var s = input[0];
            var t = input[1];
            var answer = new Answer();
            for(int i= 0; i < input.Length; i++)
            {
                for(int j = 0; j < input.Length; j++)
                {
                    for(int len = 0; i + len <= s.Length && j + len <= t.Length; len++) // C => size of the smallest input
                    {
                        if(len > answer.Length && s.Substring(i, i + len).Equals(t.Substring(j, j + len))) //
                        {
                            answer.Length = len;
                            answer.I = i;
                            answer.J = j;
                        }
                    }
                }
            }

            Console.WriteLine($"{answer.I} {answer.J} {answer.Length}");
        }
    }
}
