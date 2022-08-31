namespace LinkedLists;
public class Node
{
    /* An object for storing a single node in a linked list

    Attributes:
    Data: Data stored in node
    NextNode: Reference to next node in linked list*/
    public int Data { get; set; }
    public Node? NextNode { get; set; }

    public Node(int data, Node? node = null)
    {
        this.Data = data;
        this.NextNode = node;
    }

    public string Print()
    {
        return "Node data:" + this.Data.ToString();
    }
};