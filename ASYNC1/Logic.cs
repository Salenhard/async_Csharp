using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASYNC1
{
    internal class Logic
    {
        public static double[,] MultiplyMatrix(double[,] A, double[,] B)
        {
            int rA = A.GetLength(0);
            int cA = A.GetLength(1);
            int rB = B.GetLength(0);
            int cB = B.GetLength(1);

            if (cA != rB)
            {
                throw new Exception("Matrixes can't be multiplied!!");
            }
            else
            {
                double temp = 0;
                double[,] kHasil = new double[rA, cB];

                for (int i = 0; i < rA; i++)
                {
                    for (int j = 0; j < cB; j++)
                    {
                        temp = 0;
                        for (int k = 0; k < cA; k++)
                        {
                            temp += A[i, k] * B[k, j];
                        }
                        kHasil[i, j] = temp;
                    }
                }

                return kHasil;
            }
        }
        public static async Task<List<double[,]>> parrallelmul(List<double[,]> oldMatr, int n)
        {
            List<double[,]> matrixes = new List<double[,]>();
            for (int i = 0; i < n - 1; i++)
            {
                // расчёт работы для потоков
                int from = i * oldMatr.Count / n;
                int to = (i + 1) * oldMatr.Count / n;
                int e = 0;
                if (i <= oldMatr.Count / n)
                {
                    await Task.Run(() =>
                    {
                        for (int i = from + e; i < to + e + 1; i++)
                        {
                            matrixes.Add(MultiplyMatrix(oldMatr[i], oldMatr[i + 1]));
                        }
                    });
                    e++;
                }
                else
                    await Task.Run(() =>
                    {
                        for (int i = from + e; i < to + e; i++)
                        {
                            matrixes.Add(MultiplyMatrix(oldMatr[i], oldMatr[i + 1]));
                        }
                    });
            }
            return matrixes;
        }

        public static double[,] FillMatrixRand(double[,] matrix)
        {
            Random rnd = new Random();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = rnd.NextDouble() * 100;
                }
            }
            return matrix;
        }

        public static void PrintMatrix(double[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(String.Format("{0:0.00} ", matrix[i, j]));
                }
                Console.WriteLine();
            }
        }

        public static List<double[,]> mul(List<double[,]> oldMatr)
        {

            List<double[,]> matrixes = new();
            for (int i = 0; i < oldMatr.Count - 1; i++)
            {
                matrixes.Add(MultiplyMatrix(oldMatr[i], oldMatr[i + 1]));
            }
            return matrixes;
        }
    }
}

