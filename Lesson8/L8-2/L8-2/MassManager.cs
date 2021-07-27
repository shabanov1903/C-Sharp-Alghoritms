using System;
using System.IO;

namespace L8_2
{
    class MassManager : IMassManager
    {
        ILogger iLogger;
        public MassManager(ILogger iLogger) => this.iLogger = iLogger;

        // Выдает количество элементов текстового массива
        public int GetLength(string path)
        {
            int elements = 0;
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.ReadLine() != null)
                    {
                        elements++;
                    }
                }
            }
            catch (Exception e)
            {
                iLogger.Print(e.Message);
            }
            return elements;
        }

        // Считывает весь массив в нужном формате
        public int[] GetMassiv(string path)
        {
            int[] massiv = new int[GetLength(path)];
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    int i = 0;
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        massiv[i] = Convert.ToInt32(line);
                        i++;
                    }
                }
            }
            catch (Exception e)
            {
                iLogger.Print(e.Message);
            }
            return massiv;
        }

        // Считывает элементы в промежутке и отдает массив
        public int[] GetMassivInterval(string path, int lowLevel, int highLevel)
        {
            int[] massiv;
            int length;
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    int i = 0;
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (Int32.Parse(line) >= lowLevel && Int32.Parse(line) < highLevel) i++;
                    }
                    length = i;
                }
                massiv = new int[length];
                using (StreamReader sr = new StreamReader(path))
                {
                    int i = 0;
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (Int32.Parse(line) >= lowLevel && Int32.Parse(line) < highLevel)
                        {
                            massiv[i] = Int32.Parse(line);
                            i++;
                        }
                    }
                }
                return massiv;
            }
            catch (Exception e)
            {
                iLogger.Print(e.Message);
            }
            return null;
        }

        public int GetMaxElement(string path)
        {
            int maxValue = 0;
            for (int i = 1; i < GetLength(path); ++i)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (Int32.Parse(line) > maxValue) maxValue = Int32.Parse(line);
                        }
                    }
                }
                catch (Exception e)
                {
                    iLogger.Print(e.Message);
                }
            }
            return maxValue;
        }

        public int GetMinElement(string path)
        {
            int minValue = Int32.MaxValue;
            for (int i = 1; i < GetLength(path); ++i)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (Int32.Parse(line) < minValue) minValue = Int32.Parse(line);
                        }
                    }
                }
                catch (Exception e)
                {
                    iLogger.Print(e.Message);
                }
            }
            return minValue;
        }

        // Добавляет массив в конец поэлементно
        public void SetMassiv(string path, int[] massiv)
        {
            try
            {
                for (int i = 0; i < massiv.Length; i++)
                {
                    File.AppendAllText(path, massiv[i].ToString() + Environment.NewLine);
                }
            }
            catch (Exception e)
            {
                iLogger.Print(e.Message);
            }
        }

        // Создает временный файл
        public void CreateFile(string path)
        {
            try
            {
                using (FileStream fileStream = File.Create(path)) { };
            }
            catch (Exception e)
            {
                iLogger.Print(e.Message);
            }
        }

        // Удаляет временный файл
        public void DeleteFile(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception e)
            {
                iLogger.Print(e.Message);
            }
        }
    }
}
