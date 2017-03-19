using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

{
    public class LinkedList<T>
    {
        public LinkedListNode<T> Head { get; private set; }
        public LinkedListNode<T> Tail { get; private set; }
        public int Count { get; private set; }
        public void AddFirst(LinkedListNode<T> Node)
        {
            LinkedListNode<T> temp = Head;
            Head = Node;
            Head.Next = temp;
            Count++;
            if(Count == 1)
            {
                Tail = Node;
            }
        }
        public void AddLast(LinkedListNode<T> Node)
        {
            if (Count == 0)
            {
                Head = Node;
            }else
            {
                Tail.Next = Node;
            }
            Tail = Node;
            Count++;
        }
        public void RemoveLast()
        {
            if (Count != 0)
            {
                if (Count == 1)
                {
                    Head = Tail = null;
                }
                else
                {
                    LinkedListNode<T> Current = Head;
                    while (Current.Next != Tail)
                    {
                        Current = Current.Next;
                    }
                    Current.Next = null;
                    Tail = Current;
                }
                Count--;
            }
        }

        public void RemoveFirst()
        {
            if (Count != 0)
            {
                Head = Head.Next;
                Count--;
                if (Count == 0)
                {
                    Tail = null;
                }
            }
        }

        public bool Contains(T item)
        {
            LinkedListNode<T> Current = Head;
            while (Current != null)
            {
                if (Current.Value.Equals(item))
                {
                    return true;
                }
                Current = Current.Next;
            }
            return false;
        }

        public bool Remove(T item)
        {
            if (Count != 0)
            {
                LinkedListNode<T> Current = Head;
                LinkedListNode<T> Previous = null;
                while (Current != null)
                {
                    if (Current.Value.Equals(item))
                    {
                        if (Previous != null)
                        {
                            Previous.Next = Current.Next;
                            if (Current.Next == null)
                            {
                                Previous.Next = Tail;
                            }
                            Count--;
                        }else
                        {
                            RemoveFirst();
                        }
                        return true;
                    }
                    Previous = Current;
                    Current = Current.Next;
                }
            }
            return false;
        }

    }
}
