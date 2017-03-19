﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList
{
    public class LinkedListNode<T>
    {
        public LinkedListNode<T> Next { get; set; }
        public T Value { get; set; }
    }
}
