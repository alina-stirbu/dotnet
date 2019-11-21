using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp4
{
    public class Node
    {
        public int content;
        public Node next;
        public Node(char c)
        {
            content = c;
            next = null;
        }
    }
}
