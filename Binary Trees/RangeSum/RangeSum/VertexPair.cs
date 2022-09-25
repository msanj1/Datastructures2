using System;
using System.Collections.Generic;
using System.Text;

namespace RangeSum
{
    public class VertexPair
    {
        public Vertex left;
        public Vertex right;
        public VertexPair()
        {
        }
        public VertexPair(Vertex left, Vertex right)
        {
            this.left = left;
            this.right = right;
        }
    }
}
