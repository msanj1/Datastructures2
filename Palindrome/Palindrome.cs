using System;
using System.Runtime.InteropServices;

namespace Palindrome
{
    internal class Palindrome
    {
        private static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine(RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                    ? "Usage: Palindrome.exe [options] [settings]"
                    : "Usage: Palindrome [options] [settings]");

                Console.WriteLine();
                Console.WriteLine("Options:");
                Console.WriteLine("  -w\tInput Word ");
            }
            else
            {
                if (args[0].ToLower() == "-w")
                {
                    var input = args[1];
                    IPalindromeSolution solution = new PalindromeBacktrackPartitioner();
                    var results = solution.GetPalindromes(input);

                    foreach (var result in results) Console.WriteLine(string.Join(",", result));
                }
            }
        }
    }
}