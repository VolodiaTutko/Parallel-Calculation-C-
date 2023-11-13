using System;
using System.Diagnostics;

class GFG
{

    static int V;


    static void printSolution(int[] dist, int n)
    {
        Console.Write($"Vertex	 Distance from {n}\n");
        for (int i = 0; i < V; i++)
            Console.Write(i + " \t\t " + dist[i] + "\n");
    }

    static int minDistance(int[] dist, bool[] sptSet)
    {

        int min = int.MaxValue, min_index = -1;

        for (int v = 0; v < V; v++)
            if (sptSet[v] == false && dist[v] <= min)
            {
                min = dist[v];
                min_index = v;
            }

        return min_index;
    }





    static int[] dijkstra(int[,] graph, int src)
    {
        int[] dist = new int[V];
        bool[] sptSet = new bool[V];

        for (int i = 0; i < V; i++)
        {
            dist[i] = int.MaxValue;
            sptSet[i] = false;
        }

        dist[src] = 0;

        for (int count = 0; count < V - 1; count++)
        {
            int u = minDistance(dist, sptSet);

            sptSet[u] = true;

            for (int v = 0; v < V; v++)
            {
                if (!sptSet[v] && graph[u, v] != 0 &&
                    dist[u] != int.MaxValue && dist[u] + graph[u, v] < dist[v])
                {
                    dist[v] = dist[u] + graph[u, v];
                }
            }
        }

        return dist;
    }
    private static object lockObject = new object();

    public static int[] DijkstraThreaded(int[,] graph, int src, int numThreads)
    {
        int[] dist = new int[V];
        bool[] sptSet = new bool[V];

        for (int i = 0; i < V; i++)
        {
            dist[i] = int.MaxValue;
            sptSet[i] = false;
        }

        dist[src] = 0;

        Thread[] threads = new Thread[numThreads];
        int batchSize = V / numThreads;

        for (int threadNum = 0; threadNum < numThreads; threadNum++)
        {
            int startIndex = threadNum * batchSize;
            int endIndex = (threadNum == numThreads - 1) ? V : (threadNum + 1) * batchSize;

            threads[threadNum] = new Thread(() =>
            {
                for (int count = 0; count < V - 1; count++)
                {
                    int u = -1;
                    int minDist = int.MaxValue;

                    for (int v = startIndex; v < endIndex; v++)
                    {
                        if (!sptSet[v] && dist[v] <= minDist)
                        {
                            u = v;
                            minDist = dist[v];
                        }
                    }

                    lock (lockObject)
                    {
                        if (u == -1)
                        {
                            continue;
                        }

                        sptSet[u] = true;

                        for (int v = 0; v < V; v++)
                        {
                            if (!sptSet[v] && graph[u, v] != 0 &&
                                dist[u] != int.MaxValue && dist[u] + graph[u, v] < dist[v])
                            {
                                dist[v] = dist[u] + graph[u, v];
                            }
                        }
                    }
                }
            });

            threads[threadNum].Start();
        }

        foreach (Thread thread in threads)
        {
            thread.Join();
        }

        return dist;
    }




public static int[,] GenerateRandomMatrix(int numberOfVertices, int maxWeight)
    {
        Random random = new Random();
        int[,] graph = new int[numberOfVertices, numberOfVertices];

        for (int i = 0; i < numberOfVertices; i++)
        {
            for (int j = 0; j < numberOfVertices; j++)
            {
                if (i == j)
                {
                    graph[i, j] = 0; 
                }
                else
                {
                    graph[i, j] = random.Next(0, maxWeight + 1); 
                }
            }
        }

        return graph;
    }



    public static void Main()
    {
        Console.Write("Enter a number of vertices: ");

        string input = Console.ReadLine();
        if (!int.TryParse(input, out int Vert))
        {
            Console.WriteLine("The entered expression should be a real number.");
        }
        V = Vert;
        int maxWeight = 100;

        Console.Write("Enter a source vertice: ");

        string inputS = Console.ReadLine();
        if (!int.TryParse(inputS, out int source_vertice))
        {
            Console.WriteLine("The entered expression should be a real number.");
        }
        Console.Write("Enter a number of thread: ");

        string inputTH = Console.ReadLine();
        if (!int.TryParse(inputTH, out int numThreads))
        {
            Console.WriteLine("The entered expression should be a real number.");
        }


        var graph = GenerateRandomMatrix(V, maxWeight);
        //int[,] graph = new int[,] { { 0, 4, 0, 0, 0, 0, 0, 8, 0 },
        //                            { 4, 0, 8, 0, 0, 0, 0, 11, 0 },
        //                            { 0, 8, 0, 7, 0, 4, 0, 0, 2 },
        //                            { 0, 0, 7, 0, 9, 14, 0, 0, 0 },
        //                            { 0, 0, 0, 9, 0, 10, 0, 0, 0 },
        //                            { 0, 0, 4, 14, 10, 0, 2, 0, 0 },
        //                            { 0, 0, 0, 0, 0, 2, 0, 1, 6 },
        //                            { 8, 11, 0, 0, 0, 0, 1, 0, 7 },
        //                            { 0, 0, 2, 0, 0, 0, 6, 7, 0 } };






        var stopwatch = new Stopwatch();
        stopwatch.Start();
        dijkstra(graph, source_vertice);
        stopwatch.Stop();
        var time = stopwatch.ElapsedMilliseconds;

        Console.WriteLine($"Standart Time : {time} ms");


        var stopwatch1 = new Stopwatch();
        stopwatch1.Start();
        DijkstraThreaded(graph, source_vertice, numThreads);
        stopwatch1.Stop();
        var time1 = stopwatch1.ElapsedMilliseconds;

        Console.WriteLine($"Parallel Time : {time1} ms");


        double speedUp = (double)time / (double)time1;
        double efficiency = speedUp / numThreads;

        //printSolution(dijkstra(graph, source_vertice), source_vertice);

        Console.WriteLine($"Parallel speedUp : {speedUp}");
        Console.WriteLine($"Parallel efficiency : {efficiency}");
    }
}


