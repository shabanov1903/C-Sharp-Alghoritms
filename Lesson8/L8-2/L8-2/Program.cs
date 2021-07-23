using System;

namespace L8_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var manager = new MassManager(new Logger());
            Sort sort = new Sort(manager);

            Console.WriteLine("Введите путь к файлу для сортировки:");
            var path = Console.ReadLine();
            Console.WriteLine("Введите количество контейнеров:");
            var N = Convert.ToInt32(Console.ReadLine());
            sort.BucketSort(path, N);
        }
    } 
}
