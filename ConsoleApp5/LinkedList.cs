using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp5
{
    class LinkedList
    {
        Node head;
        public void AddNode(Node n)
        {
            if (head == null)
            {
                head = n;
            }
            else
            {
                Node temp = head;
                //go to the end of the list and put the n there
                while (temp.next != null)
                {
                    temp = temp.next;
                }
                temp.next = n;
            }
        }

        public void RemoveDuplicatesFromList()
        {
            Node start = head;

            while (start != null)
            {
                Node curr = start;
                while(curr.next != null)
                {
                    if (curr.next.content == start.content)
                    {
                        curr.next = curr.next.next;
                    }
                    else
                        curr = curr.next;
                }
                start = start.next;
            }
        }

        public void DisplayList()
        {
            Node first = head;
            while (first != null)
            {
                Console.Write((char)first.content);
                first = first.next;
            }
        }
    }
}
