using System;

namespace L8_1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 55, 25, 89, 34, 12, 19, 78, 100, 1, 95, 70, 12, 4, 7, 54, 68, 2, 3, 32, 6, 9, 48, 35, 69, 32, 55, 32 };
            Sort sortObj = new Sort();
            int[] arr1 = sortObj.HeapSort(arr);
            int[] arr2 = sortObj.BucketSort(arr, 20);
            Console.WriteLine("Пирамидальная сортировка:");
            for (int i = 0; i < arr2.Length; i++)
            {
                Console.Write(arr1[i] + " ");
            }
            Console.WriteLine();
            Console.WriteLine("Блочная сортировка:");
            for (int i = 0; i < arr2.Length; i++)
            {
                Console.Write(arr2[i] + " ");
            }
        }
    }

    interface ISort
    {
        int[] HeapSort(int[] array);
        int[] BucketSort(int[] array, int buckets);
    }

    class Sort : ISort
    {
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

        // Сортировка пирамидальная
        public int[] HeapSort(int[] array)
        {
            int n = array.Length;
            for (int i = n / 2 - 1; i >= 0; i--)
                array = Heapify(array, n, i);
            for (int i = n - 1; i >= 0; i--)
            {
                Swap(ref array[0], ref array[i]);
                array = Heapify(array, i, 0);
            }
            return array;
        }

        // Блочная сортировка
        public int[] BucketSort(int[] InputArray, int buckets)
        {
            int[][] arr = new int[buckets][];
            int[] Elements = new int[buckets];
            int size = InputArray.Length;

            int minValue = InputArray[0];
            int maxValue = InputArray[0];

            for (int i = 1; i < size; ++i)
            {
                if (InputArray[i] < minValue) minValue = InputArray[i];
                else if (InputArray[i] > maxValue) maxValue = InputArray[i];
            }

            var step = (maxValue - minValue) / buckets;

            for (int i = 0; i < buckets; i++)
            {
                int k = 0;
                for (int j = 0; j < size; j++)
                {
                    if (i != (buckets - 1))
                    {
                        if ((InputArray[j] >= (i * step + minValue)) && (InputArray[j] < ((i + 1) * step + minValue)))
                        {
                            k++;
                        }
                    }
                    else if (InputArray[j] >= (i * step + minValue))
                    {
                        k++;
                    }
                }
                arr[i] = new int[k];
            }

            for (int i = 0; i < buckets; i++)
            {
                int k = 0;
                for (int j = 0; j < size; j++)
                {
                    if (i != (buckets - 1))
                    {
                        if ((InputArray[j] >= (i * step + minValue)) && (InputArray[j] < ((i + 1) * step + minValue)))
                        {
                            arr[i][k] = InputArray[j];
                            k++;
                        }
                    }
                    else if (InputArray[j] >= (i * step + minValue))
                    {
                        arr[i][k] = InputArray[j];
                        k++;
                    }
                }
                Elements[i] = k;
            }

            for (int i = 0; i < buckets; i++)
            {
                arr[i] = InsertionSort(arr[i]);
            }

            int k2 = 0;
            for (int i = 0; i < buckets; i++)
            {
                for (int j = 0; j < Elements[i]; j++)
                {
                    InputArray[k2] = arr[i][j];
                    k2++;
                }
            }
            return InputArray;
        }

        // Сортировка дерева для пирамидального метода
        private int[] Heapify(int[] array, int n, int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;
            if (left < n && array[left] > array[largest])
                largest = left;
            if (right < n && array[right] > array[largest])
                largest = right;
            if (largest != i)
            {
                Swap(ref array[i], ref array[largest]);
                Heapify(array, n, largest);
            }
            return array;
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