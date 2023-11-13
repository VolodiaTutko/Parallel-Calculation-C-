using System;
using System.Diagnostics;



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

class Program
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

        int rows = matrix.RowsCount();
        int col = matrix.ColumnsCount();

        if(rows > 5 || col > 5)
        {
            rows = 5;
            col = 5;
        }

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < col; j++)
            {
                Console.Write(matrix[i, j].ToString().PadLeft(4));
            }

            Console.WriteLine();
        }
    }



    static int[,] MatrixSum(int[,] matrixA, int[,] matrixB)
    {
        if (matrixA.ColumnsCount() != matrixB.ColumnsCount() || matrixA.RowsCount() != matrixA.RowsCount())
        {
            throw new Exception("Different size matrix");
        }

        var matrixC = new int[matrixA.RowsCount(), matrixB.ColumnsCount()];

        for (var i = 0; i < matrixA.RowsCount(); i++)
        {
            for (var j = 0; j < matrixB.ColumnsCount(); j++)
            {
                matrixC[i, j] = matrixA[i, j] + matrixB[i, j];
            }
        }

        return matrixC;
    }



    //static int[,] AddMatrixParallel(int[,] matrixA, int[,] matrixB, int numberOfThreads)
    //{
    //    int rows = matrixA.GetLength(0);
    //    int columns = matrixA.GetLength(1);

    //    if (matrixA.ColumnsCount() != matrixB.RowsCount())
    //    {
    //        throw new Exception("Different size matrix");
    //    }
    //    int[,] result = new int[rows, columns];

       

    //    ParallelOptions parallelOptions = new ParallelOptions
    //    {
    //        MaxDegreeOfParallelism = numberOfThreads
    //    };

    //    Parallel.For(0, rows, parallelOptions, i =>
    //    {
    //        for (int j = 0; j < columns; j++)
    //        {
    //            result[i, j] = matrixA[i, j] + matrixB[i, j];
    //        }
    //    });

    //    return result;
    //}


    static int[,] AddMatrixTasks(int[,] matrixA, int[,] matrixB, int numberOfThreads)
    {
        int rows = matrixA.RowsCount();
        int columns = matrixA.ColumnsCount();

        if (matrixA.ColumnsCount() != matrixB.ColumnsCount() || matrixA.RowsCount() != matrixA.RowsCount())
        {
            throw new Exception("Different size matrix");
        }
        int[,] result = new int[rows, columns];

        int elementsPerThread = rows / numberOfThreads;

        List<Task> tasks = new List<Task>();

        for (int i = 0; i < numberOfThreads; i++)
        {
            int startRow = i * elementsPerThread;
            int endRow = (i == numberOfThreads - 1) ? rows : startRow + elementsPerThread;

            tasks.Add(Task.Factory.StartNew(() =>
            {
                for (int j = startRow; j < endRow; j++)
                {
                    for (int k = 0; k < columns; k++)
                    {
                        result[j, k] = matrixA[j, k] + matrixB[j, k];
                    }
                }
            }));
        }

        Task.WhenAll(tasks).Wait();

        return result;
    }



    static int[,] MatrixDiff(int[,] matrixA, int[,] matrixB)
    {
        if (matrixA.ColumnsCount() != matrixB.ColumnsCount() || matrixA.RowsCount() != matrixA.RowsCount())
        {
            throw new Exception("Different size matrix");
        }

        var matrixC = new int[matrixA.RowsCount(), matrixB.ColumnsCount()];

        for (var i = 0; i < matrixA.RowsCount(); i++)
        {
            for (var j = 0; j < matrixB.ColumnsCount(); j++)
            {
                matrixC[i, j] = matrixA[i, j] - matrixB[i, j];
            }
        }

        return matrixC;
    }

    static int[,] DiffMatrixTasks(int[,] matrixA, int[,] matrixB, int numberOfThreads)
    {
        int rows = matrixA.RowsCount();
        int columns = matrixA.ColumnsCount();

        if (matrixA.ColumnsCount() != matrixB.ColumnsCount() || matrixA.RowsCount() != matrixA.RowsCount())
        {
            throw new Exception("Different size matrix");
        }
        int[,] result = new int[rows, columns];

        int elementsPerThread = rows / numberOfThreads;

        List<Task> tasks = new List<Task>();

        for (int i = 0; i < numberOfThreads; i++)
        {
            int startRow = i * elementsPerThread;
            int endRow = (i == numberOfThreads - 1) ? rows : startRow + elementsPerThread;

            tasks.Add(Task.Factory.StartNew(() =>
            {
                for (int j = startRow; j < endRow; j++)
                {
                    for (int k = 0; k < columns; k++)
                    {
                        result[j, k] = matrixA[j, k] - matrixB[j, k];
                    }
                }
            }));
        }

        Task.WhenAll(tasks).Wait();

        return result;
    }

    static void Main(string[] args)
    {

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        //-------------------------------------------------------------------------------------------------------------------------//
        // SUM MATRIX A AND B



    
        Console.WriteLine("Програма для знаходження суми двох матриць");

        var matrix_a = GenerateMatrix("matrix a");
        var matrix_b = GenerateMatrix("matrix b");

        Console.WriteLine("Матриця A:");
        PrintMatrix(matrix_a);

        Console.WriteLine("Матриця B:");
        PrintMatrix(matrix_b);

        

        Stopwatch stopwatch1 = new Stopwatch();

        
        stopwatch1.Start();

        var result = MatrixSum(matrix_a, matrix_b);

        stopwatch1.Stop();

        
        long elapsedMilliseconds1 = stopwatch1.ElapsedMilliseconds;


        Console.WriteLine("Матриця Result:");
        PrintMatrix(result);


        int numIterationsAdd = 100;
        double[] executionTimesAdd = new double[numIterationsAdd];

        for (int threads = 1; threads <= numIterationsAdd; threads++)
        {
            Stopwatch Newstopwatch = new Stopwatch();

            Newstopwatch.Start();

            var NewResult = AddMatrixTasks(matrix_a, matrix_b, threads);

            Newstopwatch.Stop();

            double NewelapsedMilliseconds = Newstopwatch.ElapsedMilliseconds;
            Console.WriteLine($"Час виконання програми при {threads} потоках : {NewelapsedMilliseconds} мс");

            executionTimesAdd[threads - 1] = NewelapsedMilliseconds;
        }


        double minExecutionTimeAdd = executionTimesAdd.Min();
        double maxExecutionTimeAdd = executionTimesAdd.Max();
        double AvgExecutionTimeAdd = executionTimesAdd.Sum() / numIterationsAdd;
        int minIndexAdd = Array.IndexOf(executionTimesAdd, minExecutionTimeAdd);
        int maxIndexAdd = Array.IndexOf(executionTimesAdd, maxExecutionTimeAdd);

        


        Console.WriteLine('\n');
        Console.WriteLine($"Час виконання послідовного додавання матриць : {elapsedMilliseconds1} мс");
        Console.WriteLine('\n');
        Console.WriteLine($"Найменший час виконання при паралельному обчисленні: {minExecutionTimeAdd} мс, кількість потоків : {minIndexAdd+1}");
        Console.WriteLine('\n');
        Console.WriteLine($"Найбільший час виконання при  паралельному обчисленні: {maxExecutionTimeAdd} мс кількість потоків : {maxIndexAdd+1}");
        Console.WriteLine('\n');
        Console.WriteLine($"Середній  час виконання при  паралельному обчисленні: {AvgExecutionTimeAdd} мс");
        Console.WriteLine('\n');

        Console.Write("Введіть кількість потоків : ");
        var ThreadsAdd = int.Parse(Console.ReadLine());

        Stopwatch Newstopwatch2 = new Stopwatch();

        Newstopwatch2.Start();
        var NewResult2 = DiffMatrixTasks(matrix_a, matrix_b, ThreadsAdd);

        Newstopwatch2.Stop();

        double NewelapsedMilliseconds2 = Newstopwatch2.ElapsedMilliseconds;

        Console.WriteLine('\n');

        Console.WriteLine($"Час виконання програми при {ThreadsAdd} потоках : {NewelapsedMilliseconds2} мс");
        Console.WriteLine('\n');

        double speedUpAdd = elapsedMilliseconds1 / NewelapsedMilliseconds2;
        double effiencyAdd = speedUpAdd / (ThreadsAdd);

        Console.WriteLine($"Прискорення : {speedUpAdd}");
        Console.WriteLine('\n');
        Console.WriteLine($"Ефективність : {effiencyAdd}");




        //-------------------------------------------------------------------------------------------------------------------------//
        // DIFFERENCE MATRIX A AND B

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n");
        Console.WriteLine("Програма для знаходження різниці двох матриць");

        Console.WriteLine("Матриця A:");
        PrintMatrix(matrix_a);

        Console.WriteLine("Матриця B:");
        PrintMatrix(matrix_b);

        Stopwatch stopwatch4 = new Stopwatch();


        stopwatch4.Start();

        var result4 = MatrixDiff(matrix_a, matrix_b);

        stopwatch4.Stop();


        double elapsedMilliseconds4 = stopwatch4.ElapsedMilliseconds;

        Console.WriteLine('\n');
        Console.WriteLine("Матриця Result:");
        PrintMatrix(result4);


        int numIterationsDiff = 100;
        double[] executionTimesDiff = new double[numIterationsDiff];

        for (int threads = 1; threads <= numIterationsDiff; threads++)
        {
            Stopwatch Newstopwatch = new Stopwatch();

            Newstopwatch.Start();

            var NewResult = DiffMatrixTasks(matrix_a, matrix_b, threads);

            Newstopwatch.Stop();

            double NewelapsedMilliseconds = Newstopwatch.ElapsedMilliseconds;

            
            Console.WriteLine($"Час виконання програми при {threads} потоках : {NewelapsedMilliseconds} мс");

            executionTimesDiff[threads - 1] = NewelapsedMilliseconds;
        }


        double minExecutionTimeDiff = executionTimesDiff.Min();
        double maxExecutionTimeDiff = executionTimesDiff.Max();
        double AvgExecutionTimeDiff = executionTimesDiff.Sum()/numIterationsDiff;

        int minIndexDiff = Array.IndexOf(executionTimesDiff, minExecutionTimeDiff);
        int maxIndexDiff = Array.IndexOf(executionTimesDiff, maxExecutionTimeDiff);

        

        Console.WriteLine('\n');
        Console.WriteLine($"Час виконання послідовного віднімання матриць : {elapsedMilliseconds4} мс");
        Console.WriteLine('\n');
        Console.WriteLine($"Найменший час виконання при паралельному обчисленні: {minExecutionTimeDiff} мс, кількість потоків : {minIndexDiff+1} ");
        Console.WriteLine('\n');
        Console.WriteLine($"Найбільший час виконання при  паралельному обчисленні: {maxExecutionTimeDiff} мс,  кількість потоків : {maxIndexDiff+1} ");
        Console.WriteLine('\n');
        Console.WriteLine($"Середній  час виконання при  паралельному обчисленні: {AvgExecutionTimeDiff} мс");
        Console.WriteLine('\n');
       

        Console.Write("Введіть кількість потоків : ");
        var Threads = int.Parse(Console.ReadLine());


        Stopwatch Newstopwatch1 = new Stopwatch();

        Newstopwatch1.Start();
        var NewResult1 = DiffMatrixTasks(matrix_a, matrix_b, Threads);

        Newstopwatch1.Stop();

        double NewelapsedMilliseconds1 = Newstopwatch1.ElapsedMilliseconds;
        Console.WriteLine('\n');
        Console.WriteLine($"Час виконання програми при {Threads} потоках : {NewelapsedMilliseconds1} мс");

        double speedUpDiff = elapsedMilliseconds4 / NewelapsedMilliseconds1;
        double effiencyDiff = speedUpDiff / (Threads);

        Console.WriteLine($"Прискорення : {speedUpDiff}");
        Console.WriteLine('\n');
        Console.WriteLine($"Ефективність : {effiencyDiff}");

       
        

        Console.ReadLine();
    }
}