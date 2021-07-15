using System;
using System.IO;
using System.Collections.Generic;

namespace L6_1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Путь необходимо вводить в формате: D:\Programming\Lesson6\L6-1\L6-1\Matrix.txt
            Console.WriteLine("Введите полный путь к матрице:");
            var path = Console.ReadLine();
            Console.WriteLine("Введите номер вершины графа для поиска маршрутов (нумерация с нуля):");
            var vert = Console.ReadLine();
            MatrixReader mr = new MatrixReader();
            Dexter dexter = new Dexter(mr);
            int[] mass = dexter.GetRoutesFromMatrix(path, Int32.Parse(vert));
            // При тестировании других матриц необходимо учесть их размер в цикле
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Маршрут от вершины {Int32.Parse(vert)} до вершины {i} равен {mass[i]}");
            }
        }
    }

    // Интерфейс для релизации зависимости
    public interface IMatrixReader
    {
        public int[,] ReadMatrix(string path);
    }

    // Класс реализации метода чтения файла и парсинг с передачей двумерного массива
    public class MatrixReader : IMatrixReader
    {
        public int[,] ReadMatrix(string path)
        {
            int size = 0;
            Queue<int> matrix = new Queue<int>();

            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    String line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        size++;
                        string[] elements = line.Split(" ");
                        for (int i = 0; i < elements.Length; i++)
                        {
                            matrix.Enqueue(Int32.Parse(elements[i]));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            if (matrix.Count == size * size)
            {
                int[,] fullmatrix = new int[size, size];
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        fullmatrix[i, j] = matrix.Dequeue();
                    }
                }
                return fullmatrix;
            }
            else return null;
        }
    }

    // Класс для реализации расчета минимальных путей
    public class Dexter
    {
        private readonly IMatrixReader _iMatrixReader;
        public Dexter(IMatrixReader imatrixreader) => _iMatrixReader = imatrixreader;

        public int[] GetRoutesFromMatrix(string path, int element)
        {
            int[,] matrix = _iMatrixReader.ReadMatrix(path);
            int size = matrix.GetLength(0);
            int[] R = new int[size];
            int[] P = new int[size];
            int[] S = new int[size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (matrix[i, j] == 0) matrix[i, j] = 1_000_000;
                }
            }

            for (int j = 0; j < size; j++)
            {
                R[j] = matrix[element, j];
                P[j] = 1;
            }

            R[element] = 0;
            P[element] = 0;

            while(EndProgram(P))
            {
                for (int i = 0; i < size; i++)
                {
                    S[i] = R[i] * P[i];
                }

                int minIndex = MinIndex(S);
                for (int j = 0; j < size; j++)
                {
                    if ((matrix[minIndex, j] + R[minIndex]) < R[j]) R[j] = matrix[minIndex, j] + R[minIndex];
                }
                P[minIndex] = 0;
            }
            return R;
        }

        // Условие окончания программы: массив P заполнен нулями
        private bool EndProgram(int[] mass)
        {
            int counter = 0;
            for (int i = 0; i < mass.Length; i++)
            {
                if (mass[i] == 0) counter++;
            }
            if (counter == mass.Length) return false;
            else return true;
        }

        // Поиск минимального веса среди непроверенных ребер
        private int MinIndex(int[] mass)
        {
            int min = 1_000_000;
            int index = 0;
            for (int i = 0; i < mass.Length; i++)
                if (min > mass[i] && mass[i] != 0)
                {
                    min = mass[i];
                    index = i;
                }
            return index;
        }
    }
}