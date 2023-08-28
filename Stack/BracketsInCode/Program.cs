using System;
using System.Collections.Generic;

namespace BracketsInCode
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var stack = new Stack<char>();
            bool valid = true;
            int currentCharacterIndex = -1;
            var characters = new Dictionary<char, char>();
            characters[')'] = '(';
            characters['}'] = '{';
            characters[']'] = '[';
            var allowedCharacterSet = new HashSet<char>()
            {
                '(', ')', '{', '}', '[', ']'
            };

            for (int i = 0; i < input.Length; i++)
            {
                var currentCharacter = input[i];

                if (characters.ContainsKey(currentCharacter))
                {
                    char previousCharacter;
                    if (stack.Count > 0)
                    {
                        previousCharacter = stack.Pop();
                        if (previousCharacter != characters[currentCharacter])
                        {
                            valid = false;
                            currentCharacterIndex = i;
                            break;
                        }
                    }
                    else
                    {
                        valid = false;
                        currentCharacterIndex = i;
                        break;
                    }
                }
                else
                {
                    if (allowedCharacterSet.Contains(currentCharacter))
                    {
                        if (stack.Count == 0)
                            currentCharacterIndex = i;

                        stack.Push(currentCharacter);
                    }
                }
            }

            currentCharacterIndex++;
            Console.WriteLine(stack.Count == 0 && valid ? "Success" : currentCharacterIndex.ToString());
        }
    }
}
