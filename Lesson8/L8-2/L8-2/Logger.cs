using System;

namespace L8_2
{
    interface ILogger
    {
        void Print(string message);
    }
    class Logger : ILogger
    {
        public void Print(string message)
        {
            Console.WriteLine(message);
        }
    }
}