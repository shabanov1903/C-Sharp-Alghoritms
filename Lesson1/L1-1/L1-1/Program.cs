using System;

namespace L1_1
{
    class Program
    {
        static void Main(string[] args)
        {
            CheckNumber(InputNumber());

            // Реализация тестового запуска функции CheckNumber
            Console.WriteLine("Передача тестовых аргументов для функции CheckNumber:");
            // Проверка простого чиста 5
            CheckNumber(5);
            // Проверка не простого чиста 35
            CheckNumber(35);
            // Проверка простого чиста 73
            CheckNumber(73);
        }

        // Реализация функции ввода числа пользователя с мониторингом ошибок ввода
        private static int InputNumber()
        {
            int number;
            while (true)
            {
                Console.WriteLine("Введите целое число больше единицы:");
                try
                {
                    number = Convert.ToInt32(Console.ReadLine());
                    if (number < 2) throw new FormatException();
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка формата ввода!");
                }
            }
            return number;
        }

        // Функция проверки чисел
        private static void CheckNumber(int number)
        {
            int d = 0;
            int i = 2;

            while (i < number)
            {
                if (number % i == 0)
                {
                    d++;
                }
                i++;
            }
            if (d == 0 && number != 1 && number != 0)
            {
                Console.WriteLine($"Введено простое число {number}");
            }
            else
            {
                Console.WriteLine($"Введено не простое число {number}");
            }
        }
    }
}
