using System;
using System.Collections.Generic;
using System.Text;

namespace RangeSum
{
    public class SplayTree
    {
        public SplayTree()
        {

        }
        private void update(Vertex v)
        {
            if (v == null) return;
            v.sum = v.key + (v.left != null ? v.left.sum : 0) + (v.right != null ? v.right.sum : 0);
            if (v.left != null)
            {
                v.left.parent = v;
            }
            if (v.right != null)
            {
                v.right.parent = v;
            }
        }

        private void smallRotation(Vertex v)
        {
            Vertex parent = v.parent;
            if (parent == null)
            {
                return;
            }
            Vertex grandparent = v.parent.parent;
            if (parent.left == v)
            {
                Vertex m = v.right;
                v.right = parent;
                parent.left = m;
            }
            else
            {
                Vertex m = v.left;
                v.left = parent;
                parent.right = m;
            }
            update(parent);
            update(v);
            v.parent = grandparent;
            if (grandparent != null)
            {
                if (grandparent.left == parent)
                {
                    grandparent.left = v;
                }
                else
                {
                    grandparent.right = v;
                }
            }
        }

        private void bigRotation(Vertex v)
        {
            if (v.parent.left == v && v.parent.parent.left == v.parent)
            {
                // Zig-zig
                smallRotation(v.parent);
                smallRotation(v);
            }
            else if (v.parent.right == v && v.parent.parent.right == v.parent)
            {
                // Zig-zig
                smallRotation(v.parent);
                smallRotation(v);
            }
            else
            {
                // Zig-zag
                smallRotation(v);
                smallRotation(v);
            }
        }

        // Makes splay of the given vertex and returns the new root.
        private Vertex splay(Vertex v)
        {
            if (v == null) return null;
            while (v.parent != null)
            {
                if (v.parent.parent == null)
                {
                    smallRotation(v); //zig
                    break;
                }
                bigRotation(v);
            }
            return v;
        }

        // Searches for the given key in the tree with the given root
        // and calls splay for the deepest visited node after that.
        // Returns pair of the result and the new root.
        // If found, result is a pointer to the node with the given key.
        // Otherwise, result is a pointer to the node with the smallest
        // bigger key (next value in the order).
        // If the key is bigger than all keys in the tree,
        // then result is null.
        private VertexPair find(ref Vertex root, int key)
        {
            Vertex v = root;
            Vertex last = root;
            Vertex next = null;
            while (v != null)
            {
                if (v.key >= key && (next == null || v.key < next.key))
                {
                    next = v;
                }
                last = v;
                if (v.key == key)
                {
                    break;
                }
                if (v.key < key)
                {
                    v = v.right;
                }
                else
                {
                    v = v.left;
                }
            }

            root = splay(last);

            return new VertexPair(next, root);
        }

        private VertexPair split(ref Vertex root, int key)
        {
            VertexPair result = new VertexPair();
            VertexPair findAndRoot = find(ref root, key);
            root = findAndRoot.right;
            result.right = findAndRoot.left;
            if (result.right == null)
            {
                result.left = root;
                return result;
            }
            result.right = splay(result.right);
            result.left = result.right.left;
            result.right.left = null;
            if (result.left != null)
            {
                result.left.parent = null;
            }
            update(result.left);
            update(result.right);
            return result;
        }

        private Vertex merge(Vertex left, ref Vertex right)
        {
            if (left == null) return right;
            if (right == null) return left;
            while (right.left != null)
            {
                right = right.left;
            }
            right = splay(right);
            right.left = left;
            update(right);
            return right;
        }

        // Code that uses splay tree to solve the problem

        Vertex root = null;
        public void insert(int x)
        {
            Vertex left = null;
            Vertex right = null;
            Vertex new_vertex = null;
            VertexPair leftRight = split(ref root, x);
            left = leftRight.left;
            right = leftRight.right;
            if (right == null || right.key != x)
            {
                new_vertex = new Vertex(x, x, null, null, null);
            }
            root = merge(merge(left, ref new_vertex), ref right);
        }

        public void erase(int x)
        {
            // Implement erase yourself
            var foundVertex = find(x);
            if (foundVertex)
            {
                var currentVertex = root;
                var nextVertex = Next(root);
                if (nextVertex != null)
                {
                    root = splay(nextVertex);
                    root = splay(currentVertex);
                    var left = currentVertex.left;
                    var right = currentVertex.right;
                    right.left = left;
                    if (left != null)
                        left.parent = right;
                    root = right;
                    right.parent = null;
                }
                else
                {
                    //has no next element
                    if (currentVertex.left != null)
                    {
                        root = currentVertex.left;
                        currentVertex.left.parent = null;
                        currentVertex.left = null;
                    }
                    else
                    {
                        root = null;
                    }
                }
            }
        }

        private Vertex Next(Vertex node)
        {
            if (node.right != null)
                return LeftDescendant(node.right);
            else
                return RightAncestor(node);
        }

        private Vertex LeftDescendant(Vertex node)
        {
            if (node.left == null)
                return node;
            else
                return LeftDescendant(node.left);
        }

        private Vertex RightAncestor(Vertex node)
        {
            var parent = node.parent;
            if (parent == null)
                return null;
            if (node.key < parent.key)
            {
                return node.parent;
            }
            else
                return RightAncestor(node.parent);
        }

        public bool find(int x)
        {
            // Implement find yourself
            var result = find(ref root, x);
            if (result.right != null && result.right.key == x)
                return true;
            return false;
        }

        public long sum(int from, int to)
        {
            VertexPair leftMiddle = split(ref root, from);
            Vertex left = leftMiddle.left;
            Vertex middle = leftMiddle.right;
            VertexPair middleRight = split(ref middle, to + 1);
            middle = middleRight.left;
            Vertex right = middleRight.right;
            long ans = 0;
            if (leftMiddle.right != null && middleRight.left != null)
            {
                ans = middleRight.left.sum;
            }

            root = merge(middleRight.left, ref middleRight.right);
            root = merge(left, ref root);
            // Complete the implementation of sum

            return ans;
        }

    }
}
