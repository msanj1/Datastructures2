using System;
using System.Collections.Generic;
using System.Text;

namespace ParallelThreads
{
    public class JobQueue
    {
        private int numWorkers;
        private int[] jobs;

        private int[] assignedWorker;
        private long[] startTime;

        public JobQueue(int numberOfWorkers, int[] jobs)
        {
            numWorkers = numberOfWorkers;
            this.jobs = jobs;
        }

        public void AssignJobs()
        {
            assignedWorker = new int[jobs.Length];
            startTime = new long[jobs.Length];
            var heap = new MinHeap<Worker>();
            var workers = new Worker[numWorkers];

            for (int i = 0; i < numWorkers; i++)
            {
                workers[i] = new Worker(0, i);
            }

            heap.BuildHeap(workers);

            for (int i = 0; i < jobs.Length; i++)
            {
                int duration = jobs[i];
                Worker bestWorker = heap.ExtractMin();
                assignedWorker[i] = bestWorker.Id;
                startTime[i] = bestWorker.NextFreeTime;
                bestWorker.NextFreeTime += duration;
                heap.Add(bestWorker);
            }
        }

        public void WriteResponse()
        {
            for (int i = 0; i < jobs.Length; ++i)
            {
                Console.WriteLine(assignedWorker[i] + " " + startTime[i]);
            }
        }
    }
}
