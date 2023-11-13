using System;
using System.Diagnostics;


public class FloydFinding
{
    public const int INF = 999999999;
    public static Random random = new Random();
    public static int maxValue = 100;

    public static int[,] GenerateRandomMatrix(int verticesCount, int maxValue, int infinityValue)
    {
        int[,] matrix = new int[verticesCount, verticesCount];

        for (int i = 0; i < verticesCount; i++)
        {
            for (int j = 0; j < verticesCount; j++)
            {
                if (i == j)
                {
                    matrix[i, j] = 0;
                }
                else
                {

                    matrix[i, j] = random.Next(1, maxValue);
                }
            }
        }


        for (int i = 0; i < verticesCount; i++)
        {
            for (int j = 0; j < verticesCount; j++)
            {
                if (matrix[i, j] > maxValue / 2)
                {
                    matrix[i, j] = infinityValue;
                }
            }
        }

        return matrix;
    }

    private static void Print(int[,] distance, int verticesCount)
    {
        Console.WriteLine("Shortest distances between every pair of vertices:");

        for (int i = 0; i < verticesCount; ++i)
        {
            for (int j = 0; j < verticesCount; ++j)
            {
                if (distance[i, j] == INF)
                    Console.Write("INF".PadLeft(9));
                else
                    Console.Write(distance[i, j].ToString().PadLeft(9));
            }

            Console.WriteLine();
        }
    }



    public static int[,] FloydStandart(int[,] graph, int verticesCount)
    {
        int[,] distance = new int[verticesCount, verticesCount];
        Array.Copy(graph, distance, graph.Length);

        for (int k = 0; k < verticesCount; ++k)
        {
            for (int i = 0; i < verticesCount; ++i)
            {
                for (int j = 0; j < verticesCount; ++j)
                {
                    if (distance[i, k] + distance[k, j] < distance[i, j])
                        distance[i, j] = distance[i, k] + distance[k, j];
                }
            }
        }

        return distance;
    }




    public static int[,] ImprovedFW(int N, int[,] A)
    {
        int[][] outgoing = new int[N][];
        int[][] incoming = new int[N][];

        for (int i = 0; i < N; i++)
        {
            outgoing[i] = new int[N];
            incoming[i] = new int[N];

            for (int j = 0; j < N; j++)
            {
                if (i != j && A[i, j] != INF)
                {
                    outgoing[i][j] = j;
                }
            }
        }

        for (int k = 0; k < N; k++)
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (A[i, j] > A[i, k] + A[k, j])
                    {
                        if (A[i, j] == INF)
                        {
                            outgoing[i][j] = outgoing[i][j] | outgoing[i][k];
                            incoming[j][i] = incoming[j][i] | incoming[k][i];
                        }
                        A[i, j] = A[i, k] + A[k, j];
                    }
                }
            }
        }

        return A;
    }

    public static void CalculateShortestPaths(int k, int n, int[,] graph, int[,] distance)
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (distance[i, k] + distance[k, j] < distance[i, j])
                {
                    distance[i, j] = distance[i, k] + distance[k, j];
                }
            }
        }
    }
    public static int[,] ManualParallelFloyd(int[,] graph, int verticesCount)
    {
        int[,] distance = new int[verticesCount, verticesCount];
        Array.Copy(graph, distance, graph.Length);
        CountdownEvent countdown = new CountdownEvent(verticesCount);

        for (int i = 0; i < verticesCount; i++)
        {
            int localI = i;
            ThreadPool.QueueUserWorkItem(state =>
            {
                CalculateShortestPaths(localI, verticesCount, graph, distance);
                countdown.Signal();
            }, null);
        }

        countdown.Wait();

        return distance;
    }

   



    public static int[,] ParallelFloyd(int[,] graph, int verticesCount, int maxDegreeOfParallelism)
    {
        int[,] distance = new int[verticesCount, verticesCount];

        ParallelOptions options = new ParallelOptions
        {
            MaxDegreeOfParallelism = maxDegreeOfParallelism
        };

        for (int i = 0; i < verticesCount; i++)
        {
            for (int j = 0; j < verticesCount; j++)
            {
                distance[i, j] = graph[i, j];
            }
        }

        for (int k = 0; k < verticesCount; k++)
        {
            Parallel.For(0, verticesCount, options, i =>
            {
                for (int j = 0; j < verticesCount; j++)
                {
                    if (i != j && i != k && j != k)
                    {
                        if (distance[i, k] + distance[k, j] < distance[i, j])
                        {
                            lock (distance)
                            {
                                if (distance[i, k] + distance[k, j] < distance[i, j])
                                {
                                    distance[i, j] = distance[i, k] + distance[k, j];
                                }
                            }
                        }
                    }
                }
            });
        }

        return distance;
    }

    public static int GetPathFromMatrix(int[,] matrix, int row, int column)
    {
        if (row >= 0 && row < matrix.GetLength(0) && column >= 0 && column < matrix.GetLength(1))
        {
            return matrix[row, column];
        }
        else
        {
            Console.WriteLine("These vertices do not exist in the given graph.");
            return int.MinValue;
        }
    }







    public static void Main(string[] args)
    {


        Console.WriteLine("Program Finding the shortest paths between all pairs of vertices in a graph");
        Console.Write("Enter a number of vertices: ");

        string input = Console.ReadLine();

        if (!int.TryParse(input, out int verticesCount))
        {
            Console.WriteLine("The entered expression should be a real number.");
        }
        Console.Write("Enter a number of threads: ");

        string inputThreads = Console.ReadLine();

        if (!int.TryParse(inputThreads, out int countThreads))
        {
            Console.WriteLine("The entered expression should be a real number.");
        }

        //Console.Write("Enter a first  vericle: ");

        //string firstInvericle = Console.ReadLine();

        //if (!int.TryParse(inputThreads, out int firstvericle))
        //{
        //    Console.WriteLine("The entered expression should be a real number.");
        //}

        //Console.Write("Enter a second  vericle: ");

        //string secondInvericle = Console.ReadLine();

        //if (!int.TryParse(inputThreads, out int secondvericle))
        //{
        //    Console.WriteLine("The entered expression should be a real number.");
        //}



        //int[,] graph = {
        //    {0, 5, INF, 10},
        //    {INF, 0, 3, INF},
        //    {INF, INF, 0, 1},
        //    {INF, INF, INF, 0}
        //};

        int[,] graph = GenerateRandomMatrix(verticesCount, maxValue, INF);

        //Print(graph, verticesCount);
        //Console.WriteLine($"The shortest path from {firstInvericle} to {secondInvericle} is {GetPathFromMatrix(FloydStandart(graph, verticesCount), firstvericle, secondvericle)}");
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        FloydStandart(graph, verticesCount);
        stopwatch.Stop();
        var time = stopwatch.ElapsedMilliseconds;

        Console.WriteLine($"Standart Time : {time} ms");


        var stopwatch2 = new Stopwatch();
        stopwatch2.Start();
        ImprovedFW(verticesCount, graph);
        stopwatch2.Stop();

        var time2 = stopwatch2.ElapsedMilliseconds;

        Console.WriteLine($"Improved Time : {time2} ms");


        var stopwatch3 = new Stopwatch();
        stopwatch3.Start();
        ManualParallelFloyd(graph, verticesCount);
        stopwatch3.Stop();
        var time3 = stopwatch3.ElapsedMilliseconds;

        Console.WriteLine($"Manual parallel Time : {time3} ms");

        var stopwatch4 = new Stopwatch();
        stopwatch4.Start();
        ParallelFloyd(graph, verticesCount, countThreads);
        stopwatch3.Stop();
        var time4 = stopwatch4.ElapsedMilliseconds;

        Console.WriteLine($"ParallelFor Time : {time4} ms");
        Console.WriteLine($"Number of threads : {countThreads}");




        double MPspeedUp = (double)time / (double)time3;
        double PFspeedUp = (double)time / (double)time4;

        double MPefficiency = MPspeedUp / 4;
        double PFefficiency = PFspeedUp / countThreads;

        Console.WriteLine($"Manual parallel speedUp : {MPspeedUp}");
        Console.WriteLine($"Manual parallel efficiency : {MPefficiency}");

        Console.WriteLine($"ParallelFor speedUp : {PFspeedUp}");
        Console.WriteLine($"ParallelFor efficiency : {PFefficiency}");

    }
}
