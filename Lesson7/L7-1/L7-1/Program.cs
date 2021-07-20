using System;

namespace L7_1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] chessBoard;
            WalkHorse walkHorseObjekt = new WalkHorse();

            Console.WriteLine("Введите размер матрицы:");
            var N = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите x0:");
            var X0 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите y0:");
            var Y0 = Convert.ToInt32(Console.ReadLine());

            chessBoard = walkHorseObjekt.GetWalk(X0, Y0, N);

            Console.WriteLine("Результат, которого Вы так ждали:");
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Console.Write(String.Format("{0,3}", chessBoard[i, j]));
                }
                Console.WriteLine();
            }
        }
    }

    class WalkHorse
    {
        private int[,] matrix;
        private bool Step(int x0, int y0, int N, int nstep = 0)
        {
            // Двоичный вектор координат со смещениями
            int[,] xy = new int[8, 2] { { 1, -2 }, { 1, 2 }, { -1, -2 }, { -1, 2 }, { 2, -1 }, { 2, 1 }, { -2, 1 }, { -2, -1 } };
            // Условие выхода из рекурсии (выдаст цепочку True и завершение функции)
            if (nstep == N * N) return true;
            // Возвращает False при неверно переданных координатах, после чего производится выход на уровень выше и проверка следующей точки
            if (x0 < 0 || x0 >= N || y0 < 0 || y0 >= N) return false;
            // Возвращает False, если точка уже пройдена, после чего производится выход на уровень выше и проверка следующей точки
            if (matrix[x0, y0] != 0) return false;
            // Приравнивание номера шага
            matrix[x0, y0] = nstep + 1;
            for (int i = 0; i < 8; i++)
                // Рекурсивный метод вызова с перебором всевозможных ходов
                if (Step(x0 + xy[i, 0], y0 + xy[i, 1], N, nstep + 1))
                    return true;
            // Если было пройдено 8 ходов, но функция не завершилась, значит, необходимо произвести откат
            matrix[x0, y0] = 0;
            return false;
        }

        public int[,] GetWalk(int x, int y, int N)
        {
            matrix = new int[N, N];
            Step(x, y, N);
            return matrix;
        }
    }
}