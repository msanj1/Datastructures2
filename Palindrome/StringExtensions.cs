using System;
using System.Collections.Generic;
using System.Text;

namespace Palindrome
{
    public static class StringExtensions
    {
        public static bool CheckIfPalindrome(this string input)
        {
            var startIndex = 0;
            var endIndex = input.Length - 1;
            while (startIndex < endIndex)
            {
                if (input[startIndex] != input[endIndex]) return false;
                startIndex++;
                endIndex--;
            }

            return true;
        }
    }
}
