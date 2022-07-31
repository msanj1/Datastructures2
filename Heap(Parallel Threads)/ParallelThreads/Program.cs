using System;
using System.Collections.Generic;
using System.Linq;

namespace ParallelThreads
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            var jobs = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            var numberOfThreads = input[0];

            var jobQueue = new JobQueue(numberOfThreads, jobs);
            jobQueue.AssignJobs();
            jobQueue.WriteResponse();
        }
    }
}
