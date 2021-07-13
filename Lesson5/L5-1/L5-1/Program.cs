using System;
using System.Collections.Generic;

namespace L5_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Node<int> nodeTree;
            // Построение дерева на 12 элементов со случайными значениями от 0 до 10
            nodeTree = Tree(12, 10);
            // Поиск значения 5 с помощью алгоритма BFS
            Console.WriteLine("Поиск с помощью алгоритма BFS:");
            BFS(nodeTree, 5);
            // Поиск значения 5 с помощью алгоритма DFS
            Console.WriteLine("Поиск с помощью алгоритма DFS:");
            DFS(nodeTree, 5);
        }

        // Функция постоения идеального дерева
        public static Node<int> Tree(int n, int randomLevel)
        {
            Node<int> newNode = null;
            if (n == 0)
                return null;
            else
            {
                var nl = n / 2;
                var nr = n - nl - 1;
                newNode = new Node<int>();
                newNode.Data = new Random().Next(0, randomLevel);
                newNode.Left = Tree(nl, randomLevel);
                newNode.Right = Tree(nr, randomLevel);
            }
            return newNode;
        }

        // Функция поиска значения в ширину
        public static void BFS(Node<int> node, int searchVar)
        {
            Node<int> nodeBtw;
            Queue<Node<int>> queue = new Queue<Node<int>>();
            queue.Enqueue(node);
            while (true)
            {
                try
                {
                    nodeBtw = queue.Dequeue();
                    Console.WriteLine(nodeBtw.Data);
                    if (nodeBtw.Data == searchVar)
                    {
                        Console.WriteLine($"Найдено число: {searchVar}");
                        return;
                    }
                    if (nodeBtw.Left != null) queue.Enqueue(nodeBtw.Left);
                    if (nodeBtw.Right != null) queue.Enqueue(nodeBtw.Right);
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("Поиск не дал результата");
                    return;
                }
            }
        }

        // Функция поиска значения в глубину
        public static void DFS(Node<int> node, int searchVar)
        {
            Node<int> nodeBtw;
            Stack<Node<int>> stack = new Stack<Node<int>>();
            stack.Push(node);
            while (true)
            {
                try
                {
                    nodeBtw = stack.Pop();
                    Console.WriteLine(nodeBtw.Data);
                    if (nodeBtw.Data == searchVar)
                    {
                        Console.WriteLine($"Найдено число: {searchVar}");
                        return;
                    }
                    if (nodeBtw.Left != null) stack.Push(nodeBtw.Left);
                    if (nodeBtw.Right != null) stack.Push(nodeBtw.Right);
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("Поиск не дал результата");
                    return;
                }
            }
        }

    }

    // Обобщенный элемент дерева
    public class Node<T>
    {
        public T Data { get; set; }
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }
    }
}
