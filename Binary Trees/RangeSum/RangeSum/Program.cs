using System;
using System.Linq;
using System.Text;

namespace RangeSum
{
    internal class Program
    {
        static int Modulo = 1000000001;
        static void Main(string[] args)
        {
            var tree = new SplayTree();
            var numberOfOperations = int.Parse(Console.ReadLine());
            int last_sum_result = 0;
            var consoleOutput = new StringBuilder();
            for (int i=0;i< numberOfOperations; i++)
            {
                var treeInputs = Console.ReadLine().Split(' ');
                var operation = treeInputs[0];
                var input1 = int.Parse(treeInputs[1]);
                switch (operation)
                {
                    case "?":
                        var found = tree.find((input1 + last_sum_result) % Modulo);
                        if (found)
                        {
                            consoleOutput.AppendLine("Found");
                        }
                        else
                        {
                            consoleOutput.AppendLine("Not Found");
                        }
                        break;
                    case "+":
                        tree.insert((input1 + last_sum_result) % Modulo);
                        break;
                    case "s":
                        var input2 = int.Parse(treeInputs[2]);
                        long res = tree.sum((input1 + last_sum_result) % Modulo, (input2 + last_sum_result) % Modulo);
                        last_sum_result = (int)(res % Modulo);
                        consoleOutput.AppendLine(res.ToString());
                        break;
                    case "-":
                        tree.erase((input1 + last_sum_result) % Modulo);
                        break;
                    default:
                        break;
                }
            }

            Console.WriteLine(consoleOutput);
        }
    }
}
