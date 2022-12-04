using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaxSlidingWindow
{
    internal class Program
    {
        static Stack<int> headStack = new Stack<int>();
        static Stack<int> headStackMax = new Stack<int>();
        static Stack<int> tailStack = new Stack<int>();
        static Stack<int> tailStackMax = new Stack<int>();

        static void Main(string[] args)
        {
            var output = new StringBuilder();
            var numberOfItems = int.Parse(Console.ReadLine());

            var valueArray = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToList();
            var windowsSize = int.Parse(Console.ReadLine());

            for (int i=0; i <= numberOfItems - windowsSize; i++)
            {
                //move through the windows
                for(int b = i + headStack.Count + tailStack.Count; b < i + windowsSize; b++)
                {
                    //move through the items in the window
                    Push(valueArray[b], tailStack, tailStackMax);
                }

                var maxValue = Max();
                if(output.Length > 0)
                    output.Append(" ");
                output.Append(maxValue);

                Pop();
            }

            Console.WriteLine(output.ToString());
        }

        static int Pop()
        {
            if(headStack.Count == 0 && tailStack.Count > 0)
            {
                while (tailStack.Count != 0)
                {
                    var tailValue = Pop(tailStack, tailStackMax);
                    Push(tailValue, headStack, headStackMax);
                }
            }

            return Pop(headStack, headStackMax);
        }

        static int Max()
        {
            var max1 = headStackMax.Count > 0 ? headStackMax.Peek() : -1;
            var max2 = tailStackMax.Count > 0 ? tailStackMax.Peek() : -1;
            return Math.Max(max1, max2);
        }

        static int Pop(Stack<int> stack, Stack<int> maxStack)
        {
            var value = stack.Pop();
            if (maxStack.Count > 0)
            {
                maxStack.Pop();
            }

            return value;
        }

        static void Push(int value, Stack<int> stack, Stack<int> maxStack)
        {
            stack.Push(value);
            if (maxStack.Count > 0)
            {
                var currentMax = maxStack.Peek();
                if (currentMax < value)
                {
                    maxStack.Push(value);
                }
                else
                {
                    maxStack.Push(currentMax);
                }
            }
            else
            {
                maxStack.Push(value);
            }
        }
    }
}
