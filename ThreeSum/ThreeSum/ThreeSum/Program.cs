using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeSum
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> randomNumbers = new List<string>();

            if (!File.Exists("./1000.txt"))
            {
                int upperLimit = 1000;
                Random a = new Random();
                for (int i = 0; i < upperLimit; i++)
                {
                    randomNumbers.Add(a.Next(-1000,1000).ToString());
                }
                File.WriteAllLines("./1000.txt", randomNumbers);
            }

            var inputNumbers = File.ReadAllLines("./1000.txt").Select(int.Parse).ToArray();

            AlgStopWatch watch = new AlgStopWatch();
            var totalCount = ThreeSumCalculator.Count(inputNumbers);
            var notOfTicks = watch.ElapsedTime();

            Console.WriteLine($"Total count = {totalCount} and took {notOfTicks} seconds");
            Console.ReadLine();
        }
    }
}
