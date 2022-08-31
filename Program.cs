using LinkedLists;

var linkedList = new LinkedList();

linkedList.Add(18);
linkedList.Add(28);
linkedList.Add(13);
linkedList.Add(4);
linkedList.Add(11);
linkedList.Add(88);
linkedList.Add(99);
linkedList.Add(14);
linkedList.Add(318);
linkedList.Add(4);
linkedList.Add(3);
linkedList.Add(219);




Console.WriteLine(linkedList.ListToString());
Console.WriteLine(linkedList.MergeSort(linkedList).ListToString());