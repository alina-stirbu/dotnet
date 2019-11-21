using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp4
{
    class LinkedList
    {
        Node head;
        public void AddNode(Node n)
        {
            if(head == null)
            {
                head = n;
            }
            else
            {
                Node temp = head;
                //go to the end of the list and put the n there
                while(temp.next != null)
                {
                    temp = temp.next;
                }
                temp.next = n;
            }
        }

        public void ReverseList()
        {
            Node curr = head;
            Node next = null;
            Node prev = null;

            while(curr != null)
            {
                next = curr.next;
                curr.next = prev;
                prev = curr;
                curr = next;
            }
            head = prev;
        }

        public void DisplayList()
        {
            Node first = head;
            while(first != null)
            {
                Console.Write((char)first.content);
                first = first.next;
            }
        }
    }
}
