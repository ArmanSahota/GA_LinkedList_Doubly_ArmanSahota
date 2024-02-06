using System;

namespace GA_LinkedList_Doubly_ArmanSahota
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create a doubly linked list of integers
            DoublyLinkedList<int> linkedList = new DoublyLinkedList<int>();

            // Add elements to the linked list
            linkedList.Add(10);
            linkedList.Add(20);
            linkedList.Add(30);

            // Display elements in both forward and backward directions
            Console.WriteLine("Forward:");
            linkedList.DisplayForward();
            Console.WriteLine("Backward:");
            linkedList.DisplayBackward();

            // Remove an element with value 20
            if (linkedList.Remove(20))
                Console.WriteLine("20 removed");

            // Access element at index 1 and display it
            Console.WriteLine($"Element at index 1: {linkedList[1]}");

            // Insert elements at specific positions in the linked list
            linkedList.InsertAtIndex(1, 25); // Insert 25 at index 1
            linkedList.InsertAtFront(5);     // Insert 5 at the beginning
            linkedList.InsertAtEnd(35);      // Insert 35 at the end

            // Display elements in forward direction after insertion
            Console.WriteLine("Forward (after insertion):");
            linkedList.DisplayForward();

            // Remove elements at specific positions in the linked list
            linkedList.RemoveAtFront(); // Remove the first element
            linkedList.RemoveAtEnd();   // Remove the last element
            linkedList.RemoveAtIndex(2); // Remove the element at index 2

            // Display elements in forward direction after removal
            Console.WriteLine("Forward (after removal):");
            linkedList.DisplayForward();

            // Clear the linked list
            linkedList.Clear();

            // Display elements in forward direction after clearing
            Console.WriteLine("Forward (after clearing):");
            linkedList.DisplayForward();

            Console.ReadLine();
        }

        
        class DoublyLinkedList<T>
        {
            // Nested class representing elements in the doubly linked list
            class LinkedListNode<T>
            {
                public T Value { get; set; }                    // Data stored in the node.
                public LinkedListNode<T> Next { get; set; }     //  the next node address
                public LinkedListNode<T> Previous { get; set; } // previous node address

                // Constructor to initialize a node with a value
                public LinkedListNode(T value)
                {
                    Value = value;
                    Next = null;  // Since we don't know whats next it start null
                    Previous = null; // since we don't know whats before we start null
                }
            }

            
            private LinkedListNode<T> head; 
            private LinkedListNode<T> tail;
            private int count;

            
            public int Count // get count
            {
                get { return count; }
            }

            
            public DoublyLinkedList() // head and tail unknown so null
            {
                head = null;
                tail = null;
                count = 0;
            }

            // Add an element to the end of the linked list
            public void Add(T value)
            {
                LinkedListNode<T> newNode = new LinkedListNode<T>(value);

                if (head == null)
                {
                    // If the list is empty, set both head and tail to the new node
                    head = newNode;
                    tail = newNode;
                }
                else
                {
                    // If the list is not empty, add the new node to the end
                    newNode.Previous = tail;
                    tail.Next = newNode;
                    tail = newNode;
                }

                count++;
            }

            // Display elements from head to tail
            public void DisplayForward()
            {
                LinkedListNode<T> current = head;
                while (current != null)
                {
                    Console.Write($"{current.Value} -> ");
                    current = current.Next; // current vaule becomes the next value
                }
                Console.WriteLine("null");
            }

            // Display elements from tail to head
            public void DisplayBackward()
            {
                LinkedListNode<T> current = tail;
                while (current != null)
                {
                    Console.Write($"{current.Value} -> ");
                    current = current.Previous; // current value becomes previous value
                }
                Console.WriteLine("null");
            }

            // Remove an element by value
            public bool Remove(T value)
            {
                LinkedListNode<T> current = head;

                while (current != null)
                {
                    if (current.Value.Equals(value))
                    {
                        // Update references to remove the current node
                        if (current == head) head = head.Next;
                        if (current == tail) tail = tail.Previous;
                        if (current.Next != null) current.Next.Previous = current.Previous;
                        if (current.Previous != null) current.Previous.Next = current.Next;

                        count--;
                        return true;
                    }

                    current = current.Next;
                }

                return false;
            }

            // access at certian node
            public T this[int index]
            {
                get
                {
                    if (index < 0 || index >= count)
                        throw new IndexOutOfRangeException();

                    // Traverse the list to the specified index and return the value
                    LinkedListNode<T> current = head;
                    for (int i = 0; i < index; i++)
                        current = current.Next;

                    return current.Value;
                }
            }

            // Insert an element at a specific index
            public void InsertAtIndex(int index, T value)
            {
                if (index < 0 || index > count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
                }

                LinkedListNode<T> newNode = new LinkedListNode<T>(value);

                if (index == 0)
                {
                    // Insert at the beginning
                    newNode.Next = head;

                    if (head != null)
                    {
                        head.Previous = newNode;
                    }

                    head = newNode;

                    if (count == 0)
                    {
                        tail = newNode;
                    }
                }
                else if (index == count)
                {
                    // Insert at the end
                    newNode.Previous = tail;
                    tail.Next = newNode;
                    tail = newNode;
                }
                else
                {
                    // Insert at a specific index
                    LinkedListNode<T> current = head;
                    for (int i = 0; i < index - 1; i++)
                    {
                        current = current.Next;
                    }
                    newNode.Next = current.Next;
                    newNode.Previous = current;
                    current.Next.Previous = newNode;
                    current.Next = newNode;
                }

                count++;
            }

            // Insert an element at the beginning of the list
            public void InsertAtFront(T value)
            {
                InsertAtIndex(0, value);
            }

            // Insert an element at the end of the list
            public void InsertAtEnd(T value)
            {
                InsertAtIndex(count, value);
            }

            // Remove an element at a specific index
            public void RemoveAtIndex(int index)
            {
                if (index < 0 || index >= count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
                }

                LinkedListNode<T> current = head;

                if (index == 0)
                {
                    // Remove the first element by making the head the next element and setting the old node to null
                    head = current.Next;

                    if (head != null)
                    {
                        head.Previous = null;
                    }

                    if (count == 1)
                    {
                        tail = null;
                    }
                }
                else if (index == count - 1)
                {
                    // Remove the last element by swapping it with null
                    current = tail;
                    tail = current.Previous;
                    tail.Next = null;
                }
                else
                {
                    // Remove at a specific index
                    for (int i = 0; i < index; i++)
                    {
                        current = current.Next;
                    }
                    current.Previous.Next = current.Next;
                    current.Next.Previous = current.Previous;
                }

                count--;
            }

            // Remove the element at the beginning of the list
            public void RemoveAtFront()
            {
                RemoveAtIndex(0);
            }

            // Remove the element at the end of the list
            public void RemoveAtEnd()
            {
                RemoveAtIndex(count - 1);
            }

            // Clear the entire linked list, resetting it to an empty state
            public void Clear()
            {
                head = null;
                tail = null;
                count = 0;
            }
        }
    }
}
