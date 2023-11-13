using MathNet.Numerics.LinearAlgebra;
using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using System.Collections.Concurrent;

class SeidelSolver
{
    public static double[] Solve(double[,] A, double[] b, double tolerance = 1e-6, int maxIterations = 1000000)
    {
        int n = A.GetLength(0);
        double[] x = new double[n];
        double[] prevX = new double[n];

        for (int iteration = 0; iteration < maxIterations; iteration++)
        {
            Array.Copy(x, prevX, n);

            for (int i = 0; i < n; i++)
            {
                double sum1 = 0;
                double sum2 = 0;

                for (int j = 0; j < i; j++)
                {
                    sum1 += A[i, j] * x[j];
                }

                for (int j = i + 1; j < n; j++)
                {
                    sum2 += A[i, j] * prevX[j];
                }

                x[i] = (b[i] - sum1 - sum2) / A[i, i];
            }

           
            double maxError = 0;
            for (int i = 0; i < n; i++)
            {
                double error = Math.Abs(x[i] - prevX[i]);
                if (error > maxError)
                {
                    maxError = error;
                }
            }

            if (maxError < tolerance)
            {
                return x;
            }
        }

        Console.WriteLine("Метод Зейделя не сходиться за вказаною кількістю ітерацій.");
        return x;
    }
}

public class ParallelSeidelSolver
{
    public static double[] Solve(double[,] A, double[] b, double tolerance = 1e-6, int maxIterations = 1000, int numThreads = 1)
    {
        int n = A.GetLength(0);
        double[] x = new double[n];
        double[] prevX = new double[n];

        for (int iteration = 0; iteration < maxIterations; iteration++)
        {
            Array.Copy(x, prevX, n);

            
            Parallel.ForEach(Partitioner.Create(0, n),
                new ParallelOptions { MaxDegreeOfParallelism = numThreads }, range =>
                {
                    for (int i = range.Item1; i < range.Item2; i++)
                    {
                        double sum1 = 0;
                        double sum2 = 0;

                        for (int j = 0; j < i; j++)
                        {
                            sum1 += A[i, j] * x[j];
                        }

                        for (int j = i + 1; j < n; j++)
                        {
                            sum2 += A[i, j] * prevX[j];
                        }

                        x[i] = (b[i] - sum1 - sum2) / A[i, i];
                    }
                });

          
            double maxError = 0;
            for (int i = 0; i < n; i++)
            {
                double error = Math.Abs(x[i] - prevX[i]);
                if (error > maxError)
                {
                    maxError = error;
                }
            }

            if (maxError < tolerance)
            {
               
                return x;
            }
        }

        Console.WriteLine("Метод Зейделя не сходиться за вказаною кількістю ітерацій.");
        return x;
    }
}

public class MatrixGenerator
{
    public static (double[,], double[]) GenerateDiagonallyDominantMatrix(int n)
    {
        var random = new Random();
        var matrix = new double[n, n];
        var vector = new double[n];

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
               
                matrix[i, j] = random.NextDouble() * 10.0;
            }

           
            double rowSum = 0.0;
            for (int j = 0; j < n; j++)
            {
                if (j != i)
                {
                    rowSum += Math.Abs(matrix[i, j]);
                }
            }

           
            matrix[i, i] = rowSum + 1.0;

          
            vector[i] = random.NextDouble() * 10.0;
        }

        return (matrix, vector);
    }
}
class Program
{


    //static double[] ParallelSLAR(int n, double[,] a)
    //{
    //    double[] x = new double[n];
    //    double[] b = new double[n];

       
    //    for (int i = 0; i < n; i++)
    //    {
    //        b[i] = a[i, n];
    //    }

    //    Parallel.For(0, n, i =>
    //    {
    //        double bi1 = a[i, 0];
    //        Parallel.For(0, n, j =>
    //        {
    //            a[i, j] /= bi1;
    //        });
    //        b[i] /= bi1;
    //    });

    //    Parallel.For(1, n, m =>
    //    {
    //        Parallel.For(m, n, i =>
    //        {
    //            for (int k = 0; k < m; k++)
    //            {
    //                a[i, n] -= a[i, k] * a[k, m - 1];
    //            }
    //            a[i, m - 1] = a[i, n];
    //        });

    //        Parallel.For(m + 1, n, j =>
    //        {
    //            for (int k = 0; k < m; k++)
    //            {
    //                a[j, n] -= a[m, k] * a[k, j];
    //            }
    //            a[j, m] = a[j, n] / a[m, m];
    //        });
    //    });

    //    Parallel.For(0, n, k =>
    //    {
    //        int i = n - k - 1;
    //        x[i] = b[i];
    //        Parallel.For(i + 1, n, j =>
    //        {
    //            x[i] -= a[i, j] * x[j];
    //        });
    //    });

    //    Console.WriteLine("Result:");
    //    for (int i = 0; i < n; i++)
    //    {
    //        Console.WriteLine("x" + (i + 1) + " = " + x[i]);
    //    };

    //    return x;
    //}

   
    
    //static double[,] GenerateRandomMatrix(int n)
    //{
    //    Random random = new Random();
    //    double[,] matrix = new double[n, n + 1];

    //    for (int i = 0; i < n; i++)
    //    {
    //        for (int j = 0; j < n + 1; j++)
    //        {
    //            matrix[i, j] = random.NextDouble() * 5; 
    //        }
    //    }

    //    return matrix;
    //}

    static void PrintMatrix(double[,] matrix)
    {
        int numRows = matrix.GetLength(0);
        int numCols = matrix.GetLength(1);

        for (int i = 0; i < numRows; i++)
        {
            for (int j = 0; j < numCols; j++)
            {
                Console.Write(matrix[i, j].ToString("0.00").PadLeft(8) + " "); 
            }
            Console.WriteLine();
        }
    }
    //static double[] RemoveLastColumn(double[,] matrix, out double[,] reducedMatrix)
    //{
    //    int numRows = matrix.GetLength(0);
    //    int numCols = matrix.GetLength(1);

    //    double[] lastColumn = new double[numRows];
    //    reducedMatrix = new double[numRows, numCols - 1];

    //    for (int i = 0; i < numRows; i++)
    //    {
    //        lastColumn[i] = matrix[i, numCols - 1];

    //        for (int j = 0; j < numCols - 1; j++)
    //        {
    //            reducedMatrix[i, j] = matrix[i, j];
    //        }
    //    }

    //    return lastColumn;
    //}





    static void Main()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        //double[,] aArray = {
        //    {4, -1, 0, 0},
        //    {-1, 4, -1, 0},
        //    {0, -1, 4, -1},
        //    {0, 0, -1, 3}
        //};

        Console.WriteLine("Введіть кількість невідомих :");
        var numberOfUnknowns = int.Parse(Console.ReadLine());

        Console.WriteLine("Введіть кількість потоків :");
        var numberOfthreads = int.Parse(Console.ReadLine());

       

        //double[] bArray = { 1, 2, 0, 1 };


        (double[,] aArray, double [] bArray) = MatrixGenerator.GenerateDiagonallyDominantMatrix(numberOfUnknowns);





        Matrix<double> A = Matrix<double>.Build.DenseOfArray(aArray);
        Vector<double> b = Vector<double>.Build.Dense(bArray);


        Stopwatch stopwatch2 = new Stopwatch();


        stopwatch2.Start();

        A.Solve(b);

        stopwatch2.Stop();


        double elapsedMilliseconds2 = stopwatch2.ElapsedMilliseconds;

        Console.WriteLine($"Час послідовного виконання за допомогою бібліотеки : {elapsedMilliseconds2} мс");


        Console.WriteLine("результат:");
        Vector<double> solution = A.Solve(b);
        foreach (double ele in solution)
        {
            Console.WriteLine("x = " + ele);
        }
        //-----------------------------------------------------------------------------------------------//

        Stopwatch stopwatch = new Stopwatch();


         stopwatch.Start();

        double[] x = SeidelSolver.Solve(aArray, bArray);

         stopwatch.Stop();


        double elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

        Console.WriteLine($"Час  виконання  Методу Зейделя: {elapsedMilliseconds} мс");


        Console.WriteLine("Розв'язок:");
        for (int i = 0; i < x.Length; i++)
        {
            Console.WriteLine($"x{i + 1} = {x[i]}");
        }



        //--------------------------------------------------------------------------------------//
        //---------------------------------------------------------------------------------------//
        //----------------------------------------------------------------------------------------//


        Stopwatch stopwatch3 = new Stopwatch();


        stopwatch3.Start();

        double[] parax = ParallelSeidelSolver.Solve(aArray, bArray, 1e-6, 1000, numberOfthreads); 

        stopwatch3.Stop();


        double elapsedMilliseconds3 = stopwatch3.ElapsedMilliseconds;

        Console.WriteLine($"Час  виконання  Методу Зейделя паралельно: {elapsedMilliseconds3} мс");

        Console.WriteLine("Розв'язок:");
        for (int i = 0; i < parax.Length; i++)
        {
            Console.WriteLine($"x{i + 1} = {parax[i]}");
        }
        double speedUp = elapsedMilliseconds / elapsedMilliseconds3;
        double effiency = speedUp / numberOfthreads;

        Console.WriteLine($"Прискорення : {speedUp}");
        Console.WriteLine($"Ефективність : {effiency}");


    }
}
