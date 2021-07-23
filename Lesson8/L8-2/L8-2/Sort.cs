using System;
using System.Text;

namespace L8_2
{
    class Sort
    {
        readonly IMassManager iMassManager;
        public Sort(IMassManager iMassManager) => this.iMassManager = iMassManager;

        // Cортировка вставками
        public static int[] InsertionSort(int[] array)
        {
            for (var i = 0; i < array.Length; i++)
            {
                var key = array[i];
                var j = i;
                while ((j > 0) && (array[j - 1] > key))
                {
                    Swap(ref array[j - 1], ref array[j]);
                    j--;
                }
                array[j] = key;
            }
            return array;
        }

        // Блочная сортировка
        public void BucketSort(string path, int buckets)
        {
            int[] arr;
            int minValue = iMassManager.GetMinElement(path);
            int maxValue = iMassManager.GetMaxElement(path);

            var step = (maxValue - minValue) / buckets;

            for (int i = 0; i < buckets; i++)
            {
                if (i < (buckets - 1)) arr = iMassManager.GetMassivInterval(path, (i * step + minValue), ((i + 1) * step + minValue));
                else arr = iMassManager.GetMassivInterval(path, (i * step + minValue), Int32.MaxValue);
                arr = InsertionSort(arr);
                StringBuilder sb = new StringBuilder(path);
                sb.Insert((sb.Length - 4), i.ToString());
                iMassManager.SetMassiv(sb.ToString(), arr);
            }
            iMassManager.DeleteFile(path);
            for (int i = 0; i < buckets; i++)
            {
                StringBuilder sb = new StringBuilder(path);
                sb.Insert((sb.Length - 4), i.ToString());
                arr = iMassManager.GetMassiv(sb.ToString());
                iMassManager.DeleteFile(sb.ToString());
                iMassManager.SetMassiv(path, arr);
            }
        }

        // Обмен элементами
        private static void Swap(ref int e1, ref int e2)
        {
            var temp = e1;
            e1 = e2;
            e2 = temp;
        }
    }
}