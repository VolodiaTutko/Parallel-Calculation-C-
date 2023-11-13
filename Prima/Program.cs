using System;
using System.Collections.Generic;
using System.Diagnostics;

class AdjListNode
{
    public int dest;     
    public int weight;   
    public AdjListNode next; 
}


class AdjList
{
    public AdjListNode head;
}


class Graph
{
    public int V;         
    public AdjList[] array; 
}

class Program
{

    static Random random = new Random();
    static void FillGraphWithRandomWeights(Graph graph, int maxWeight)
    {
        int V = graph.V;
        Random random = new Random();

        for (int src = 0; src < V; src++)
        {
            for (int dest = src + 1; dest < V; dest++)
            {
                int weight = random.Next(1, maxWeight + 1); 
                addEdge(graph, src, dest, weight);
            }
        }
    }

   
    static AdjListNode newAdjListNode(int dest, int weight)
    {
        AdjListNode newNode = new AdjListNode();
        newNode.dest = dest;
        newNode.weight = weight;
        newNode.next = null;
        return newNode;
    }

    
    static Graph createGraph(int V)
    {
        Graph graph = new Graph();
        graph.V = V;
        graph.array = new AdjList[V];

        for (int i = 0; i < V; ++i)
            graph.array[i] = new AdjList();

        return graph;
    }

    
    static void addEdge(Graph graph, int src, int dest, int weight)
    {
        AdjListNode newNode = newAdjListNode(dest, weight);
        newNode.next = graph.array[src].head;
        graph.array[src].head = newNode;

        newNode = newAdjListNode(src, weight);
        newNode.next = graph.array[dest].head;
        graph.array[dest].head = newNode;
    }

  
    class MinHeapNode
    {
        public int v;   
        public int key;  
    }

    
    class MinHeap
    {
        public int size;     
        public int capacity;
        public int[] pos;    
        public MinHeapNode[] array; 
    }

    
    static MinHeapNode newMinHeapNode(int v, int key)
    {
        MinHeapNode minHeapNode = new MinHeapNode();
        minHeapNode.v = v;
        minHeapNode.key = key;
        return minHeapNode;
    }

   
    static MinHeap createMinHeap(int capacity)
    {
        MinHeap minHeap = new MinHeap();
        minHeap.pos = new int[capacity];
        minHeap.size = 0;
        minHeap.capacity = capacity;
        minHeap.array = new MinHeapNode[capacity];
        return minHeap;
    }

    static void swapMinHeapNode(ref MinHeapNode a, ref MinHeapNode b)
    {
        MinHeapNode t = a;
        a = b;
        b = t;
    }

    
    static void minHeapify(MinHeap minHeap, int idx)
    {
        int smallest, left, right;
        smallest = idx;
        left = 2 * idx + 1;
        right = 2 * idx + 2;

        if (left < minHeap.size && minHeap.array[left].key < minHeap.array[smallest].key)
            smallest = left;

        if (right < minHeap.size && minHeap.array[right].key < minHeap.array[smallest].key)
            smallest = right;

        if (smallest != idx)
        {
            MinHeapNode smallestNode = minHeap.array[smallest];
            MinHeapNode idxNode = minHeap.array[idx];

            minHeap.pos[smallestNode.v] = idx;
            minHeap.pos[idxNode.v] = smallest;

            swapMinHeapNode(ref minHeap.array[smallest], ref minHeap.array[idx]);

            minHeapify(minHeap, smallest);
        }
    }


    static bool isEmpty(MinHeap minHeap)
    {
        return minHeap.size == 0;
    }

    
    static MinHeapNode extractMin(MinHeap minHeap)
    {
        if (isEmpty(minHeap))
            return null;

        MinHeapNode root = minHeap.array[0];

        MinHeapNode lastNode = minHeap.array[minHeap.size - 1];
        minHeap.array[0] = lastNode;

        minHeap.pos[root.v] = minHeap.size - 1;
        minHeap.pos[lastNode.v] = 0;

        --minHeap.size;
        minHeapify(minHeap, 0);

        return root;
    }

    static void decreaseKey(MinHeap minHeap, int v, int key)
    {
        int i = minHeap.pos[v];

        minHeap.array[i].key = key;

        while (i > 0 && minHeap.array[i].key < minHeap.array[(i - 1) / 2].key)
        {
            minHeap.pos[minHeap.array[i].v] = (i - 1) / 2;
            minHeap.pos[minHeap.array[(i - 1) / 2].v] = i;
            swapMinHeapNode(ref minHeap.array[i], ref minHeap.array[(i - 1) / 2]);

            i = (i - 1) / 2;
        }
    }

    
    static bool isInMinHeap(MinHeap minHeap, int v)
    {
        return minHeap.pos[v] < minHeap.size;
    }

   
    static void printArr(int[] arr, int n)
    {
        for (int i = 1; i < n; ++i)
            Console.WriteLine(arr[i] + " - " + i);
    }

    
    static void PrimMST(Graph graph)
    {
        int V = graph.V;
        int[] parent = new int[V];
        int[] key = new int[V];

        MinHeap minHeap = createMinHeap(V);

        for (int v = 1; v < V; ++v)
        {
            parent[v] = -1;
            key[v] = int.MaxValue;
            minHeap.array[v] = newMinHeapNode(v, key[v]);
            minHeap.pos[v] = v;
        }

        key[0] = 0;
        minHeap.array[0] = newMinHeapNode(0, key[0]);
        minHeap.pos[0] = 0;

        minHeap.size = V;

        while (!isEmpty(minHeap))
        {
            MinHeapNode minHeapNode = extractMin(minHeap);
            int u = minHeapNode.v;

            AdjListNode pCrawl = graph.array[u].head;
            while (pCrawl != null)
            {
                int v = pCrawl.dest;

                if (isInMinHeap(minHeap, v) && pCrawl.weight < key[v])
                {
                    key[v] = pCrawl.weight;
                    parent[v] = u;
                    decreaseKey(minHeap, v, key[v]);
                }
                pCrawl = pCrawl.next;
            }
        }

       // printArr(parent, V);
       
    }

    static void ParallelPrimMST(Graph graph, int numThreads)
    {
        int V = graph.V;
        int[] parent = new int[V];
        int[] key = new int[V];
        MinHeap minHeap = createMinHeap(V);

        for (int v = 1; v < V; ++v)
        {
            parent[v] = -1;
            key[v] = int.MaxValue;
            minHeap.array[v] = newMinHeapNode(v, key[v]);
            minHeap.pos[v] = v;
        }

        key[0] = 0;
        minHeap.array[0] = newMinHeapNode(0, key[0]);
        minHeap.pos[0] = 0;
        minHeap.size = V;

        
        Parallel.For(0, V, new ParallelOptions { MaxDegreeOfParallelism = numThreads }, u =>
        {
            while (!isEmpty(minHeap))
            {
                MinHeapNode minHeapNode = extractMin(minHeap);
                int uV = minHeapNode.v;

                AdjListNode pCrawl = graph.array[uV].head;
                while (pCrawl != null)
                {
                    int v = pCrawl.dest;

                    if (isInMinHeap(minHeap, v) && pCrawl.weight < key[v])
                    {
                        key[v] = pCrawl.weight;
                        parent[v] = uV;
                        decreaseKey(minHeap, v, key[v]);
                    }
                    pCrawl = pCrawl.next;
                }
            }
        });

        printArr(parent, V);
    }

    static void Main(string[] args)
    {
        int V = 5;
        Graph graph = createGraph(V);
        int threads = 100;

        //addEdge(graph, 0, 1, 4);
        //addEdge(graph, 0, 7, 8);
        //addEdge(graph, 1, 2, 8);
        //addEdge(graph, 1, 7, 11);
        //addEdge(graph, 2, 3, 7);
        //addEdge(graph, 2, 8, 2);
        //addEdge(graph, 2, 5, 4);
        //addEdge(graph, 3, 4, 9);
        //addEdge(graph, 3, 5, 14);
        //addEdge(graph, 4, 5, 10);
        //addEdge(graph, 5, 6, 2);
        //addEdge(graph, 6, 7, 1);
        //addEdge(graph, 6, 8, 6);
        //addEdge(graph, 7, 8, 7);

        
        FillGraphWithRandomWeights(graph, 10);


        Stopwatch timer1 = new Stopwatch();
        timer1.Start();
        PrimMST(graph);
        timer1.Stop();

        var time1 = timer1.ElapsedMilliseconds;

        

        Stopwatch timer2 = new Stopwatch();
        timer2.Start();
        ParallelPrimMST(graph, threads);
        timer2.Stop();

        var time2 = timer2.ElapsedMilliseconds;
        Console.WriteLine($"Algorithm Prima with {V} vertices -: {time1} ms");
        Console.WriteLine($" Parallel Algorithm Prima with {V} vertices and {threads} threads -: {time2} ms");

        double speedUp = (double)time1 / (double)time2;
        double efficiency = speedUp / threads;
        Console.WriteLine($"SpeedUp = {speedUp}");
        Console.WriteLine($"efficiency = {efficiency}");




    }
}
