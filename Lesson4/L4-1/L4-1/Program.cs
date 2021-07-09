using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;

namespace L4_1
{
    public delegate bool MassStrings(string[] args, string elem);
    public delegate bool MassHash(HashSet<string> args, string elem);
    class Program
    {
        static void Main(string[] args)
        {
            MassStrings massStrings = FindString;
            MassHash massHash = FindHash;
            AutoTest autoTest = new AutoTest(10000);
            autoTest.TestFindString(massStrings);
            autoTest.TestFindHash(massHash);
        }
        static bool FindString(string[] args, string elem)
        {
            foreach (string str in args)
            {
                if (str == elem) return true;
            }
            return false;
        }
        static bool FindHash(HashSet<string> args, string elem)
        {
            return args.Contains(elem);
        }
    }

    class AutoTest
    {
        // Исходный массив для тестирования
        private string[] testMass;

        // Конструктор с созданием N количества элементов строк
        public AutoTest (int testCount)
        {
            testMass = new string[testCount];
            for (int i = 0; i < testMass.Length; i++)
            {
                testMass[i] = GenRandomString("QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm", 6);
            }
            LOG("Тест запущен " + DateTime.Now.ToString() + ":");
        }
        
        // Генерирование случайной строки
        static string GenRandomString(string Alphabet, int Length)
        {
            Random rnd = new Random();
            StringBuilder sb = new StringBuilder(Length - 1);
            int Position = 0;
            for (int i = 0; i < Length; i++)
            {
                Position = rnd.Next(0, Alphabet.Length - 1);
                sb.Append(Alphabet[Position]);
            }
            return sb.ToString();
        }

        // Функция для генерации лога:
        private void LOG(string log)
        {
            var workDir = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\"));
            var path = Path.Combine(workDir, "LOG.txt");
            File.AppendAllText(path, log + Environment.NewLine);
        }

        // Функция для тестирования поиска строки
        public void TestFindString(MassStrings massStrings)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            var test = massStrings(testMass, testMass[5000]);

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            var str = $"Результат работы поиска в массиве - {test}; Затрачено времени: {ts.TotalMilliseconds} мс";
            LOG(str);
            Console.WriteLine(str);
        }

        // Функция для тестирования поиска Hash
        public void TestFindHash(MassHash massHash)
        {
            var hashSet = new HashSet<string>();
            for (int i = 0; i < testMass.Length; i++)
            {
                hashSet.Add(testMass[i]);
            }
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            var test = massHash(hashSet, testMass[5000]);

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            var str = $"Результат работы поиска в HashSet - {test}; Затрачено времени: {ts.TotalMilliseconds} мс";
            LOG(str);
            Console.WriteLine(str);
        }
    }
}