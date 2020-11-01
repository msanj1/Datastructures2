using System;
using System.Collections.Generic;
using System.Text;

namespace Palindrome
{
    public interface IPalindromeSolution
    {
        IList<IList<string>> GetPalindromes(string inputValue);
    }
}
