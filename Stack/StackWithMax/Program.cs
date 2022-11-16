using System;
using System.Collections.Generic;
using System.Text;

namespace StackWithMax
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var output = new StringBuilder();
            var numberOfOperations = int.Parse(Console.ReadLine());
            var stack = new Stack<int>();
            var maxStack = new Stack<int>();
            for(int i=0; i< numberOfOperations; i++)
            {
                var operations = Console.ReadLine().Split(' ');
                var operation = operations[0];

                switch (operation)
                {
                    case "push":
                        {
                            var valueAsString = operations[1];
                            var value = int.Parse(valueAsString);
                            stack.Push(value);
                            if(maxStack.Count > 0)
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
                        break;
                    case "max":
                        {
                            if(maxStack.Count > 0)
                            {
                                var currentMax = maxStack.Peek();
                                output.AppendLine(currentMax.ToString());
                            }
                            else
                            {
                                output.AppendLine("");
                            }
                        }
                        break;
                    case "pop":
                        {
                            var currentValue = stack.Pop();
                            maxStack.Pop();
                        }
                        break;
                    default:
                        break;
                }
            }

            Console.Write(output.ToString());
        }
    }
}
