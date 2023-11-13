using System;
using System.Diagnostics;
using System.Threading.Tasks;



static class MatrixExt
{
    public static int RowsCount(this int[,] matrix)
    {
        return matrix.GetLength(0);
    }


    public static int ColumnsCount(this int[,] matrix)
    {
        return matrix.GetLength(1);
    }
}

class Lab2
{

    static int[,] GetMatrixFromConsole(string name)
    {
        Console.Write("Кількість рядків матриці {0}: ", name);
        var n = int.Parse(Console.ReadLine());
        Console.Write("Кількість стовпців матриці {0}: ", name);
        var m = int.Parse(Console.ReadLine());

        var matrix = new int[n, m];
        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < m; j++)
            {
                Console.Write("{0}[{1},{2}] = ", name, i, j);
                matrix[i, j] = int.Parse(Console.ReadLine());
            }
        }

        return matrix;
    }


    static int[,] GenerateMatrix(string name)
    {
        Console.Write("Кількість рядків матриці {0}: ", name);
        var n = int.Parse(Console.ReadLine());
        Console.Write("Кількість стовпців матриці {0}: ", name);
        var m = int.Parse(Console.ReadLine());
        var matrix = new int[n, m];

        Random rand = new Random();

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                matrix[i, j] = rand.Next(1, 11);
            }
        }

        return matrix;

    }


    static void PrintMatrix(int[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        if (rows > 5 || cols > 5)
        {
            rows = 5;
            cols = 5;
        }

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                Console.Write(matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }



    static int[,] MatrixMultiply(int[,] matrixA, int[,] matrixB)
    {
        int rowsA = matrixA.GetLength(0);
        int colsA = matrixA.GetLength(1);
        int rowsB = matrixB.GetLength(0);
        int colsB = matrixB.GetLength(1);

        if (colsA != rowsB)
        {
            throw new Exception("Неможливо перемножити матриці: кількість стовпців першої матриці не дорівнює кількості рядків другої матриці.");
        }

        var matrixC = new int[rowsA, colsB];

        for (var i = 0; i < rowsA; i++)
        {
            for (var j = 0; j < colsB; j++)
            {
                int sum = 0;
                for (var k = 0; k < colsA; k++)
                {
                    sum += matrixA[i, k] * matrixB[k, j];
                }
                matrixC[i, j] = sum;
            }
        }

        return matrixC;
    }
    //static int[,] MultiplyMatricesParallel(int[,] matrixA, int[,] matrixB, int Threads)
    //{
    //    int rowsA = matrixA.GetLength(0);
    //    int colsA = matrixA.GetLength(1);
    //    int rowsB = matrixB.GetLength(0);
    //    int colsB = matrixB.GetLength(1);

    //    if (colsA != rowsB)
    //    {
    //        throw new Exception("Неможливо перемножити матриці: кількість стовпців першої матриці не дорівнює кількості рядків другої матриці.");
    //    }

    //    int[,] resultMatrix = new int[rowsA, colsB];

    //    int maxDegreeOfParallelism = Threads;
    //    ParallelOptions parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = maxDegreeOfParallelism };

    //    Parallel.For(0, rowsA, parallelOptions, i =>
    //    {
    //        for (int j = 0; j < colsB; j++)
    //        {
    //            int sum = 0;
    //            Parallel.For(0, colsA, parallelOptions, k =>
    //            {
    //                sum += matrixA[i, k] * matrixB[k, j];
    //            });
    //            resultMatrix[i, j] = sum;
    //        }
    //    });

    //    return resultMatrix;
    //}

    static int[,] MultiplyMatricesParallel1(int[,] matrixA, int[,] matrixB, int numThreads)
    {
        int rowsA = matrixA.GetLength(0);
        int colsA = matrixA.GetLength(1);
        int rowsB = matrixB.GetLength(0);
        int colsB = matrixB.GetLength(1);

        if (colsA != rowsB)
            throw new Exception("Columns A not equal Rows B");

        var result = new int[rowsA, colsB];

        Parallel.For(0, rowsA, new ParallelOptions { MaxDegreeOfParallelism = numThreads }, i =>
        {
            for (int j = 0; j < colsB; ++j)
                for (int k = 0; k < colsA; ++k)
                    result[i, j] += matrixA[i, k] * matrixB[k, j];
        });

        return result;
    }

    static void Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        //-------------------------------------------------------------------------------------------------------------------------//
        // Multiply MATRIX A AND B

        Console.WriteLine("Програма для знаходження добутку двох матриць");

        var matrix_a = GenerateMatrix("matrix a");
        var matrix_b = GenerateMatrix("matrix b");

        Console.WriteLine("Матриця A:");
        PrintMatrix(matrix_a);

        Console.WriteLine("Матриця B:");
        PrintMatrix(matrix_b);



        Stopwatch stopwatch = new Stopwatch();


        stopwatch.Start();

        var result = MatrixMultiply(matrix_a, matrix_b);

        stopwatch.Stop();


        double elapsedMilliseconds = stopwatch.ElapsedMilliseconds;


        Console.WriteLine("Матриця Result:");
        PrintMatrix(result);

        int numIterations = 10;
        double[] executionTimes = new double[numIterations];

        for (int threads = 1; threads <= numIterations; threads++)
        {
            Stopwatch Newstopwatch = new Stopwatch();

            Newstopwatch.Start();

            var NewResult = MultiplyMatricesParallel1(matrix_a, matrix_b, threads);

            Newstopwatch.Stop();

            double NewelapsedMilliseconds = Newstopwatch.ElapsedMilliseconds;
            Console.WriteLine($"Час виконання програми при {threads} потоках : {NewelapsedMilliseconds} мс");

            executionTimes[threads - 1] = NewelapsedMilliseconds;
        }


        double minExecutionTime = executionTimes.Min();
        double maxExecutionTime = executionTimes.Max();
        double AvgExecutionTime = executionTimes.Sum() / numIterations;
        int minIndex = Array.IndexOf(executionTimes, minExecutionTime);
        int maxIndex = Array.IndexOf(executionTimes, maxExecutionTime);

        Console.WriteLine('\n');
        Console.WriteLine($"Час виконання послідовного  множення матриць : {elapsedMilliseconds} мс");
        Console.WriteLine('\n');
        Console.WriteLine($"Найменший час виконання при паралельному обчисленні: {minExecutionTime} мс, кількість потоків : {minIndex + 1}");
        Console.WriteLine('\n');
        Console.WriteLine($"Найбільший час виконання при  паралельному обчисленні: {maxExecutionTime} мс кількість потоків : {maxIndex + 1}");
        Console.WriteLine('\n');
        Console.WriteLine($"Середній  час виконання при  паралельному обчисленні: {AvgExecutionTime} мс");
        ;


        Console.WriteLine('\n');
        

        Console.Write("Введіть кількість потоків : ");
        var Threads = int.Parse(Console.ReadLine());

        Stopwatch Newstopwatch2 = new Stopwatch();

        Newstopwatch2.Start();
        var NewResult2 = MultiplyMatricesParallel1(matrix_a, matrix_b, 4);

        Newstopwatch2.Stop();

        double NewelapsedMilliseconds2 = Newstopwatch2.ElapsedMilliseconds;

        Console.WriteLine('\n');

        Console.WriteLine($"Час виконання програми при {Threads} потоках : {NewelapsedMilliseconds2} мс");
        Console.WriteLine('\n');

        double speedUpAdd = elapsedMilliseconds / NewelapsedMilliseconds2;
        double effiencyAdd = speedUpAdd / (Threads);

        Console.WriteLine($"Прискорення : {speedUpAdd}");
        Console.WriteLine('\n');
        Console.WriteLine($"Ефективність : {effiencyAdd}");


    }
}

