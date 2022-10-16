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
            var isSuccessful = true;
            int index = -1;
            for(int i=0; i < input.Length; i++)
            {
                var character = input[i];

                if(character == ']' || character == '}' || character == ')')
                {
                    if (stack.Count > 0)
                    {
                        var lastCharacter = stack.Pop();
                        if (character == ']' && lastCharacter != '[')
                        {
                            index = i + 1;
                            isSuccessful = false;
                            break;
                        }else
                        if (character == '}' && lastCharacter != '{')
                        {
                            index = i + 1;
                            isSuccessful = false;
                            break;
                        }else
                        if (character == ')' && lastCharacter != '(')
                        {
                            index = i + 1;
                            isSuccessful = false;
                            break;
                        }
                    }
                    else
                    {
                        index = i + 1;
                        isSuccessful = false;
                        break;
                    }
                }
                if(character == '[' || character == '{' || character == '(')
                {
                    if (stack.Count > 0)
                    {
                        var previousCharacter = stack.Peek();
                        if (!(previousCharacter == '[' || previousCharacter == '{' || previousCharacter == '('))
                        {
                            index = i + 1;
                        }
                    }
                    else
                    {
                        index = i + 1;
                    }
                    stack.Push(character);
                }
            }

            if(isSuccessful && stack.Count == 0)
            {
                Console.WriteLine("Success");
            }
            else
            {
                Console.WriteLine(index);
            }
        }
    }
}
