using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//Имеется пустой участок земли (двумерный массив) и план сада, который необходимо реализовать.
//Эту задачу выполняют два садовника, которые не хотят встречаться друг с другом.
//Первый садовник начинает работу с верхнего левого угла сада и перемещается слева направо, сделав ряд, он спускается вниз.
//Второй садовник начинает работу с нижнего правого угла сада и перемещается снизу вверх, сделав ряд, он перемещается влево.
//Если садовник видит, что участок сада уже выполнен другим садовником, он идет дальше. Садовники должны работать параллельно.
//Создать многопоточное приложение, моделирующее работу садовников.

namespace HW_21.Multithreading
{
    internal class Program
    {
        static int[,] garden;
        static int m;
        static int n;

        static object locker = new object();
        static void Main(string[] args)
        {
            Console.WriteLine("Введите размерность сада:");
            n = Convert.ToInt32(Console.ReadLine());
            m = Convert.ToInt32(Console.ReadLine());

            garden = new int[n, m];

            //ThreadStart threadStart = new ThreadStart(Gardener1);
            //Thread thread = new Thread(threadStart);
            //thread.Start();

            //Gardener2();

            Thread gardener1 = new Thread(Gardener1);
            Thread gardener2 = new Thread(Gardener2);

            gardener1.Start();
            gardener2.Start();

            gardener1.Join();
            gardener2.Join();

            Console.WriteLine("\nИтоговый сад:\n");

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Console.Write(garden[i, j] + " ");
                }
                Console.WriteLine();
            }

        }

        static void Gardener1()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    lock(locker)
                    {
                        if (garden[i, j] == 0)
                            garden[i, j] = 1;
                    }
                    Thread.Sleep(3);
                }
            }
        }

        static void Gardener2()
        {
            for (int i = m - 1; i >= 0; i--)
            {
                for (int j = n - 1; j >= 0; j--)
                {
                    lock (locker)
                    {
                        if (garden[j, i] == 0)
                            garden[j, i] = 2;
                    }
                    Thread.Sleep(3);
                }
            }
        }
    }
}
