using System;
using System.Collections.Generic;
using System.Text;

namespace Palindrome
{
    public class PalindromeBacktrackPartitioner: IPalindromeSolution
    {
        public IList<IList<string>> GetPalindromes(string inputValue)
        {
            var output = new List<IList<string>>();
            Partition(inputValue, 0, new List<string>(), output);
            return output;
        }

        private void Partition(string input, int startIndex, List<string> current, List<IList<string>> output)
        {
            if (startIndex >= input.Length)
                output.Add(new List<string>(current));
            else
                for (var endIndex = startIndex + 1; endIndex <= input.Length; endIndex++)
                {
                    var potentialPalindrome = input.Substring(startIndex, endIndex - startIndex);
                    if (potentialPalindrome.CheckIfPalindrome())
                    {
                        current.Add(potentialPalindrome);
                        Partition(input, endIndex, current, output);
                        current.RemoveAt(current.Count - 1);
                    }
                }
        }
    }
}
