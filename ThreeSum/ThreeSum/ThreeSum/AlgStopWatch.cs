using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeSum
{
    public class AlgStopWatch
    {
        private readonly Stopwatch _stopWatch;

        public AlgStopWatch()
        {
            _stopWatch = new Stopwatch();
            _stopWatch.Start();
        }

        public double ElapsedTime()
        {
            return _stopWatch.ElapsedMilliseconds / 1000.0;
        }
    }
}
