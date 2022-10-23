using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NetworkPackets
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var inputs = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToList();
            var bufferSize = inputs[0];
            var incomingPackets = inputs[1];

            Queue<Job> buffer = new Queue<Job>();
            List<int> responses = new List<int>();
            int currentFinishTime = 0;

            for(int i=0; i< incomingPackets; i++)
            {
                var packet = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToList();
                var arrivalTime = packet[0];
                var processingTime = packet[1];

                while (buffer.Count > 0)
                {
                    var currentTime = arrivalTime;
                    var currentJob = buffer.Peek();
                    if (currentJob.FinishTime <= currentTime)
                    {
                        buffer.Dequeue();
                    }
                    else
                    {
                        break;
                    }
                }

                if (buffer.Count >= bufferSize)
                    responses.Add(-1);
                else
                {
                    if (arrivalTime < currentFinishTime)
                        arrivalTime = currentFinishTime;

                    buffer.Enqueue(new Job()
                    {
                        StartTime = arrivalTime,
                        FinishTime = arrivalTime + processingTime 
                    });
                    currentFinishTime = arrivalTime + processingTime;
                    responses.Add(arrivalTime);
                }
            }

            foreach (var response in responses)
            {
                Console.WriteLine(response);
            }
        }

        public class Job
        {
            public int StartTime { get; set; }
            public int FinishTime { get; set; }
        }
    }
}
