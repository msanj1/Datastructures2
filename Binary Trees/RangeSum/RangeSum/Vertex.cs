using System;
using System.Collections.Generic;
using System.Text;

namespace RangeSum
{
    public class Vertex
    {
        public int key;
        // Sum of all the keys in the subtree - remember to update
        // it after each operation that changes the tree.
        public long sum;
        public Vertex left;
        public Vertex right;
        public Vertex parent;

        public Vertex(int key, long sum, Vertex left, Vertex right, Vertex parent)
        {
            this.key = key;
            this.sum = sum;
            this.left = left;
            this.right = right;
            this.parent = parent;
        }
    }
}
