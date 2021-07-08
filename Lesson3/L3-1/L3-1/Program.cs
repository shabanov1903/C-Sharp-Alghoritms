using System;
using System.Diagnostics;

namespace L3_1
{
    public delegate float Test1(PointClassFloat point1, PointClassFloat point2);
    public delegate float Test2(PointStructFloat point1, PointStructFloat point2);
    public delegate double Test3(PointStructDouble point1, PointStructDouble point2);
    public delegate float Test4(PointStructFloat point1, PointStructFloat point2);

    class Program
    {
        static void Main(string[] args)
        {
            // Присвоение делегатов для тестирования
            Test1 test1 = CalculateDistance;
            Test2 test2 = CalculateDistance;
            Test3 test3 = CalculateDistance;
            Test4 test4 = CalculateDistanceSimple;

            // Тестирование
            TestClass testClass = new TestClass();
            testClass.TestPointClassFloat(test1);
            testClass.TestPointStructFloat(test2);
            testClass.TestPointStructDouble(test3);
            testClass.TestPointStructFloatSimple(test4);
        }

        // Функции для вычисления дистанции
        static float CalculateDistance(PointClassFloat point1, PointClassFloat point2)
        {
            return (float)(Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2)));
        }
        static float CalculateDistance(PointStructFloat point1, PointStructFloat point2)
        {
            return (float)(Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2)));
        }
        static double CalculateDistance(PointStructDouble point1, PointStructDouble point2)
        {
            return Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2));
        }
        static float CalculateDistanceSimple(PointStructFloat point1, PointStructFloat point2)
        {
            return (float)(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2));
        }
    }

    public class PointClassFloat
    {
        public float X { get; set; }
        public float Y { get; set; }

        public PointClassFloat(float X, float Y)
        {
            this.X = X;
            this.Y = Y;
        }
    }

    public class PointClassDouble
    {
        public double X { get; set; }
        public double Y { get; set; }

        public PointClassDouble(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }
    }

    public struct PointStructFloat
    {
        public float X { get; set; }
        public float Y { get; set; }

        public PointStructFloat(float X, float Y)
        {
            this.X = X;
            this.Y = Y;
        }
    }

    public struct PointStructDouble
    {
        public double X { get; set; }
        public double Y { get; set; }

        public PointStructDouble(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }
    }

    class TestClass
    {
        // Входные параметры для тестирования
        float fX1 = 64.224f;
        float fY1 = 89.654f;
        float fX2 = 81.301f;
        float fY2 = 50.988f;
        double dX1 = 64.224;
        double dY1 = 89.654;
        double dX2 = 81.301;
        double dY2 = 50.988;

        // Функции для тестирования через делегаты
        public void TestPointClassFloat(Test1 test1)
        {
            PointClassFloat pointClassFloat1 = new PointClassFloat(fX1, fY1);
            PointClassFloat pointClassFloat2 = new PointClassFloat(fX2, fY2);
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var result = test1(pointClassFloat1, pointClassFloat2);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine($"Обычный метод расчёта дистанции со ссылочным типом (float): {result}; Время расчета: {ts.TotalMilliseconds} мс");
        }
        public void TestPointStructFloat(Test2 test2)
        {
            PointStructFloat pointStructFloat1 = new PointStructFloat(fX1, fY1);
            PointStructFloat pointStructFloat2 = new PointStructFloat(fX2, fY2);
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var result = test2(pointStructFloat1, pointStructFloat2);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine($"Обычный метод расчёта дистанции со значимым типом (float): {result}; Время расчета: {ts.TotalMilliseconds} мс");
        }
        public void TestPointStructDouble(Test3 test3)
        {
            PointStructDouble pointStructDouble1 = new PointStructDouble(dX1, dY1);
            PointStructDouble pointStructDouble2 = new PointStructDouble(dX2, dY2);
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var result = test3(pointStructDouble1, pointStructDouble2);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine($"Обычный метод расчёта дистанции со значимым типом (double): {result}; Время расчета: {ts.TotalMilliseconds} мс");
        }
        public void TestPointStructFloatSimple(Test4 test4)
        {
            PointStructFloat pointStructFloat1 = new PointStructFloat(fX1, fY1);
            PointStructFloat pointStructFloat2 = new PointStructFloat(fX2, fY2);
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var result = test4(pointStructFloat1, pointStructFloat2);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine($"Метод расчёта дистанции без квадратного корня со значимым типом (float): {result}; Время расчета: {ts.TotalMilliseconds} мс");
        }
    }
}
