namespace LinkedLists;
public class LinkedList
{
    //     Linear data structure that stores values in nodes. The list maintains a reference to the first node, also called head. Each node points to the next node in the list

    // Attributes:
    //     head: The head node of the list
    public Node? Head { get; set; }
    public int Count { get; set; }

    public LinkedList()
    {
        this.Head = null;
        this.Count = 0;
    }

    // Determines if the linked list is empty
    // Takes O(1) time
    public bool IsEmpty() => this.Head is null;

    public int Length() => this.Count;

    public int Size()
    {
        var current = this.Head;
        var count = 0;

        while (current != null)
        {
            count++;
            current = current.NextNode;
        }

        return count;
    }

    // Adds new Node containing data to head of the list
    // Also called prepend
    // Takes O(1) time
    public void Add(int data)
    {
        var newHead = new Node(data, this.Head);
        this.Head = newHead;
        this.Count++;
    }

    // Search for the first node containing data that matches the key
    // Returns the node or `None` if not found
    // Takes O(n) time
    public Node? Search(int key)
    {
        var current = this.Head;

        while (current != null)
        {
            if (current.Data == key)
                return current;
            else
                current = current.NextNode;
        }

        return null;
    }

    public void Insert(int data, int index)
    {
        // Inserts a new Node containing data at index position
        // Insertion takes O(1) time but finding node at insertion point takes
        // O(n) time.
        // Takes overall O(n) time.

        if (index >= this.Count) throw new IndexOutOfRangeException();

        if (index == 0)
        {
            this.Add(data);
            return;
        }

        if (index > 0)
        {
            var newNode = new Node(data);
            var position = index;
            var current = this.Head;

            while (position > 1)
            {
                current = current?.NextNode;
                position--;
            }

            var previousNode = current;
            var nextNode = current?.NextNode;

            previousNode.NextNode = newNode;
            newNode.NextNode = nextNode;
        }

        this.Count++;
    }

    public Node? NodeAtIndex(int index)
    {
        // Returns the Node at specified index
        // Takes O(n) time

        if (index == 0) return this.Head;

        var current = this.Head;
        var position = 0;

        while (position < index)
        {
            current = current?.NextNode;
            position++;
        }

        return current;
    }

    public Node? Remove(int key)
    {
        // Removes Node containing data that matches the key
        // Returns the node or `None` if key doesn't exist
        // Takes O(n) time

        var current = this.Head;
        var previous = current;
        var found = false;

        while (current != null && !found)
        {
            if (current.Data == key && current == this.Head)
            {
                found = true;
                this.Head = current.NextNode;
                this.Count--;
                return current;
            }
            else if (current.Data == key)
            {
                found = true;
                previous.NextNode = current.NextNode;
                this.Count--;
                return current;
            }
            else
            {
                previous = current;
                current = current.NextNode;
            }
        }

        return null;

    }

    public Node? RemoveAtIndex(int index)
    {
        // Removes Node at specified index
        // Takes O(n) time
        if (index >= this.Count) throw new IndexOutOfRangeException();

        var current = this.Head;

        if (index == 0)
        {
            this.Head = current?.NextNode;
            this.Count--;
            return current;
        }

        var position = index;

        while (position > 1)
        {
            current = current?.NextNode;
            position--;
        }

        var previousNode = current;
        current = current?.NextNode;
        var nextNode = current?.NextNode;

        previousNode.NextNode = nextNode;
        this.Count--;

        return current;

    }

    public string ListToString()
    {
        // Return a string representation of the list.
        // Takes O(n) time.

        var nodesString = "";
        var current = this.Head;

        while (current != null)
        {
            if (current == this.Head) nodesString += $"[Head {current.Data}]-> ";
            else if (current.NextNode == null) nodesString += $"[Tail {current.Data}]";
            else nodesString += $"[{current.Data}]-> ";

            current = current.NextNode;
        }

        return nodesString;

    }

    public LinkedList MergeSort(LinkedList linkedList)
    {
        /*Sorts a linked list in ascending order
        - Recursively divide the linked list into sublists containing a single node
        - Repeatedly merge the sublists to produce sorted sublists until one remains

        Returns a sorted linked list

        Takes O(n log n) time
        Takes O(n) space*/
        if (linkedList.Size() == 1) return linkedList;
        else if (linkedList.Head == null) return linkedList;

        var (left_half, right_half) = Split(linkedList);
        var left = MergeSort(left_half);
        var right = MergeSort(right_half);

        return Merge(left, right);
    }

    private (LinkedList, LinkedList) Split(LinkedList linkedList)
    {
        /*Divide the unsorted list at midpoint into sublists
        Takes O(log n) time*/
        if (linkedList == null || linkedList.Head == null)
        {
            var leftHalf = linkedList;
            LinkedList rightHalf = null;

            return (linkedList, rightHalf);
        }
        else
        {
            var size = linkedList.Size();
            var mid = size / 2;
            var midNode = linkedList.NodeAtIndex(mid - 1);

            var leftHalf = linkedList;
            var rightHalf = new LinkedList();
            rightHalf.Head = midNode.NextNode;
            midNode.NextNode = null;

            return (leftHalf, rightHalf);
        }

    }


    public LinkedList Merge(LinkedList left, LinkedList right)
    {
        /*
        Merges two linked lists, sorting by data in nodes
        Returns a new merged list
        Takes O(n) space
        Runs in O(n) time*/

        // Create a new linked list that contains nodes from merging left and right
        var merged = new LinkedList();
        // Add a fake head that is discarded later.
        merged.Add(0);
        // Set current to the head of the linked list
        var current = merged.Head;

        // Obtain head nodes for left and right linked lists
        var left_head = left.Head;
        var right_head = right.Head;

        // Iterate over left and right as long until the tail node of both
        // left and right
        while (left_head != null || right_head != null)
        {
            // If the head node of left is None, we're at the tail
            // Add the tail node from right to the merged linked list
            if (left_head == null)
            {
                current.NextNode = right_head;
                // Call next on right to set loop condition to False
                right_head = right_head.NextNode;
            }
            // If the head node of right is None, we're at the tail
            // Add the tail node from left to the merged linked list
            else if (right_head == null)
            {
                current.NextNode = left_head;
                // Call next on left to set loop condition to False
                left_head = left_head.NextNode;
            }
            else
            {
                // Not at either tail node
                // Obtain node data to perform comparison operations
                var left_data = left_head.Data;
                var right_data = right_head.Data;

                // If data on left is lesser than right set current to left node
                // Move left head to next node
                if (left_data < right_data)
                {
                    current.NextNode = left_head;
                    left_head = left_head.NextNode;
                }
                // If data on left is greater than right set current to right node
                // Move right head to next node
                else
                {
                    current.NextNode = right_head;
                    right_head = right_head.NextNode;
                }
            }
            // Move current to next node
            current = current.NextNode;
        }
        // Discard fake head and set first merged node as head
        var head = merged.Head.NextNode;
        merged.Head = head;

        return merged;

    }
}