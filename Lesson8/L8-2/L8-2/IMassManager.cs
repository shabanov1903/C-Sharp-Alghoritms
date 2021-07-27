using System;


namespace L8_2
{
    interface IMassManager
    {
        int[] GetMassiv(string path);
        void SetMassiv(string path, int[] massiv);
        public int[] GetMassivInterval(string path, int lowLevel, int highLevel);
        public int GetLength(string path);
        public int GetMinElement(string path);
        public int GetMaxElement(string path);
        public void CreateFile(string path);
        public void DeleteFile(string path);
    }
}