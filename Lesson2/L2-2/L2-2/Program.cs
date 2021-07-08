using System;

namespace L2_2
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] myMass = new int[10] { 0, 10, 27, 48, 49, 57, 64, 88, 99, 135 };
            Console.WriteLine(BinarySearch(myMass, 10));
            Console.WriteLine(BinarySearch(myMass, 57));
            Console.WriteLine(BinarySearch(myMass, 135));
        }

        /*
         * Расчет асимптотической сложности:
         * Алгоритм будет иметь ряд итераций: [N]/2 + [(N/2)]/2 + [((N/2)/2)]/2 + ...
         * Аналогичная запись: [N]/2^1 + [N]/2^2 + [N]/2^3 + ... + [N]/2^x, где X - колическтво итераций (асимптотическая функция)
         * В наихудшем случае: [N]/2^x = 1 => 2^x = [N] => xlog2(2) = log2(N)
         * Итого имеем: x = log2(N) - Логарифмическая зависимость
         */

        public static int BinarySearch(int[] inputArray, int searchValue)
        {
            int min = 0;
            int max = inputArray.Length - 1;
            while (min <= max)
            {
                int mid = (min + max) / 2;
                if (searchValue == inputArray[mid])
                {
                    return mid;
                }
                else if (searchValue < inputArray[mid])
                {
                    max = mid - 1;
                }
                else
                {
                    min = mid + 1;
                }
            }
            return -1;
        }
    }
}
