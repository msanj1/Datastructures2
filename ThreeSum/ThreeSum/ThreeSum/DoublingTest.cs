using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeSum
{
    public class DoublingTest
    {
        public static double TimeTrial(int noOfInput)
        {
            int max = 1000000;
            int[] input = new int[noOfInput];

            Random ran = new Random();
            for (int i = 0; i < noOfInput; i++)
            {
                input[i] = ran.Next(-max, max);
            }

            AlgStopWatch timer = new AlgStopWatch();
            ThreeSumCalculator.Count(input);
            return timer.ElapsedTime();
        }
    }
}
