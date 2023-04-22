using System.Diagnostics;

namespace ASYNC1
{
    internal class Program
    {

        private static void Main()
        {
            Task<List<double[,]>> task;                         // выполняет пересчёт матриц асинхронно
            Stopwatch stopwatch = new();
            List<double[,]> matrixes = new();

            // ввод
            Console.WriteLine("Введите кол-во матриц:");
            int m = int.Parse(Console.ReadLine());

            for (int i = 0; i < m; i++)
            {
                double[,] matrix = new double[10, 10];
                matrixes.Add(Logic.FillMatrixRand(matrix));
            }
            // умножение матриц паралелльно
            stopwatch.Start();

            // ввод
            Console.WriteLine("Введите кол-во потоков:");
            int n = int.Parse(Console.ReadLine());
            

            task = Logic.parrallelmul(matrixes, n);
            task.Wait();
            stopwatch.Stop();


            // умножение матриц не паралелльно
            Console.WriteLine("Время(async): " + stopwatch.ElapsedMilliseconds + "мс");
            stopwatch.Start();
            Logic.mul(matrixes);

            Console.WriteLine("Время(sync): " + stopwatch.ElapsedMilliseconds + "мс");
        }

    }
}