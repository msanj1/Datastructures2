using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelThreads
{
    internal class Worker : IComparable<Worker>
    {
        public Worker(int nextFreeTime, int id)
        {
            NextFreeTime = nextFreeTime;
            Id = id;
        }

        public int NextFreeTime { get; set; }
        public int Id { get; }

        public int CompareTo(Worker other)
        {
            if(NextFreeTime.CompareTo(other.NextFreeTime) == 0)
            {
                return Id.CompareTo(other.Id);
            }

            return NextFreeTime.CompareTo(other.NextFreeTime);
        }
    }
}
