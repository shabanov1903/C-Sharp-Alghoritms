using System;

namespace L1_3
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputNumber = InputNumber();
            Console.WriteLine($"Значение элемента ряда (через цикл): {fCycleSum(inputNumber)}");
            Console.WriteLine($"Значение элемента ряда (через рекурсию): {fRecurseSum(inputNumber)}");

            // Реализация тестового запуска функций
            Console.WriteLine("Передача тестовых аргументов для функций fCycleSum и fRecurseSum:");
            // Расчет для числа 10
            Console.WriteLine($"Значение элемента ряда {10} (через цикл): {fCycleSum(10)}");
            Console.WriteLine($"Значение элемента ряда {10} (через рекурсию): {fRecurseSum(10)}");
            // Расчет для числа 20
            Console.WriteLine($"Значение элемента ряда {20} (через цикл): {fCycleSum(20)}");
            Console.WriteLine($"Значение элемента ряда {20} (через рекурсию): {fRecurseSum(20)}");
            // Расчет для числа 30
            Console.WriteLine($"Значение элемента ряда {30} (через цикл): {fCycleSum(30)}");
            Console.WriteLine($"Значение элемента ряда {30} (через рекурсию): {fRecurseSum(30)}");
        }

        // Реализация функции ввода числа пользователя с мониторингом ошибок ввода
        private static int InputNumber()
        {
            int number;
            while (true)
            {
                Console.WriteLine("Введите целое число:");
                try
                {
                    number = Convert.ToInt32(Console.ReadLine());
                    if (number < 0) throw new FormatException();
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка формата ввода!");
                }
            }
            return number;
        }

        // Циклическая функция рассчета числа Фибоначчи
        static int fCycleSum(int n)
        {
            // Пременная для запоминания элемента n-2
            int fSumma1 = 0;
            // Пременная для запоминания элемента n-1
            int fSumma2 = 0;
            // Если пользователь ввёл число больше 0, то задаются начальные условия
            if (n > 0) fSumma2 = 1;
            for (int i = 0; i < n; i++)
            {
                fSumma2 = fSumma1 + fSumma2;
                // В случае нулевой итерации fSumma1 должна остаться нулевой
                if (i != 0) fSumma1 = fSumma2 - fSumma1;
            }
            return fSumma2;
        }

        // Рекурсивная функция рассчета числа Фибоначчи
        static int fRecurseSum(int n)
        {
            if (n == 0)
            {
                return 0;
            }
            else if (n == 1)
            {
                return 1;
            }
            else
            {
                return fRecurseSum(n - 1) + fRecurseSum(n - 2);
            }
        }
    }
}
