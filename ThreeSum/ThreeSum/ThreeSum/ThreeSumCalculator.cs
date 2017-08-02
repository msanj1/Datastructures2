using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeSum
{
    public class ThreeSumCalculator
    {
        public static int Count(int[] input)
        {
            int count = 0;
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = i+1; j < input.Length; j++)
                {
                    for (int k = j+1; k < input.Length; k++)
                    {
                        if (input[i] + input[j] + input[k] == 0)
                        {
                            count++;
                        }
                    }
                }
            }
            return count;
        }
    }
}
