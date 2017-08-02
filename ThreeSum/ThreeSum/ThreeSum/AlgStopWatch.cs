using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeSum
{
    public class AlgStopWatch
    {
        private readonly long _startTime;

        public AlgStopWatch()
        {
            _startTime = DateTime.Now.Millisecond;
        }

        public double ElapsedTime()
        {
            return (DateTime.Now.Millisecond - _startTime) / 1000.0;
        }
    }
}
