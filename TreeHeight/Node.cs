using System;
using System.Collections.Generic;
using System.Text;

namespace TreeHeight
{
    public class Node
    {
        public List<Node> Children { get; set; } = new List<Node>();
        public int Height { get; set; } = 1;
    }
}
