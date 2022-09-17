using System;
using System.Collections.Generic;
using System.Text;

namespace CheckIsBinaryTreeEasy
{
    internal class Node
    {
        public Node Parent { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public long Key { get; set; }
        public long LowerLimit { get; set; } = long.MinValue;
        public long UpperLimit { get; set; } = long.MaxValue;
    }
}
