using System;

namespace L2_1
{
    class Program
    {
        static void Main(string[] args)
        {
            TestNode();
        }

        public static void TestNode()
        {
            // Тестовый список
            GBLinkedList gbLinkedList = new GBLinkedList();
            // Тестовая ссылочная переменная
            Node testNode;
            // Добавление в список 5 элементов
            gbLinkedList.AddNode(0);
            gbLinkedList.AddNode(1);
            gbLinkedList.AddNode(2);
            gbLinkedList.AddNode(3);
            gbLinkedList.AddNode(4);
            // Вывод на экран имеющегося списка (с проверкой на null)
            Console.WriteLine($"Первоначальный список ({gbLinkedList.GetCount()} элементов):");
            testNode = gbLinkedList.FindNode(0);
            Console.WriteLine(testNode?.Value);
            testNode = gbLinkedList.FindNode(1);
            Console.WriteLine(testNode?.Value);
            testNode = gbLinkedList.FindNode(2);
            Console.WriteLine(testNode?.Value);
            testNode = gbLinkedList.FindNode(3);
            Console.WriteLine(testNode?.Value);
            testNode = gbLinkedList.FindNode(4);
            Console.WriteLine(testNode?.Value);
            // Удаление элемента, содержащего значение 2
            gbLinkedList.RemoveNode(2);
            // Удаление элемента, содержащего значение 4 через ссылку
            testNode = gbLinkedList.FindNode(4);
            gbLinkedList.RemoveNode(testNode);
            // Вывод на экран имеющегося списка (с проверкой на null)
            Console.WriteLine($"Список без элементов со сзначениями 2 и 4 ({gbLinkedList.GetCount()} элементов):");
            testNode = gbLinkedList.FindNode(0);
            Console.WriteLine(testNode?.Value);
            testNode = gbLinkedList.FindNode(1);
            Console.WriteLine(testNode?.Value);
            testNode = gbLinkedList.FindNode(2);
            Console.WriteLine(testNode?.Value); // Вывод пустой строки
            testNode = gbLinkedList.FindNode(3);
            Console.WriteLine(testNode?.Value);
            testNode = gbLinkedList.FindNode(4);
            Console.WriteLine(testNode?.Value); // Вывод пустой строки
            // Добавление элемента, содержащего значение 2, после элемента, содержащего значение 1
            testNode = gbLinkedList.FindNode(1);
            gbLinkedList.AddNodeAfter(testNode, 2);
            // Добавление элемента, содержащего значение 4, после элемента, содержащего значение 3
            testNode = gbLinkedList.FindNode(3);
            gbLinkedList.AddNodeAfter(testNode, 4);
            // Вывод на экран имеющегося списка (с проверкой на null)
            Console.WriteLine($"Список с возвращенными элементами ({gbLinkedList.GetCount()} элементов):");
            testNode = gbLinkedList.FindNode(0);
            Console.WriteLine(testNode?.Value);
            testNode = gbLinkedList.FindNode(1);
            Console.WriteLine(testNode?.Value);
            testNode = gbLinkedList.FindNode(2);
            Console.WriteLine(testNode?.Value);
            testNode = gbLinkedList.FindNode(3);
            Console.WriteLine(testNode?.Value);
            testNode = gbLinkedList.FindNode(4);
            Console.WriteLine(testNode?.Value);
        }
    }

    public class Node
    {
        public int Value { get; set; }
        public Node NextNode { get; set; }
        public Node PrevNode { get; set; }
    }

    public interface ILinkedList
    {
        int GetCount(); // Возвращает количество элементов в списке
        void AddNode(int value); // Добавляет новый элемент списка
        void AddNodeAfter(Node node, int value); // Добавляет новый элемент списка после определённого элемента
        void RemoveNode(int index); // Удаляет элемент по порядковому номеру
        void RemoveNode(Node node); // Удаляет указанный элемент
        Node FindNode(int searchValue); // Ищет элемент по его значению
    }

    public class GBLinkedList : ILinkedList
    {
        private Node startNode;
        private Node lastNode;

        public void AddNode(int value)
        {
            if ((startNode == null) && (lastNode == null))
            {
                startNode = new Node { Value = value };
                return;
            }
            if ((startNode != null) && (lastNode == null))
            {
                lastNode = new Node { Value = value };
                startNode.NextNode = lastNode;
                lastNode.PrevNode = startNode;
                return;
            }
            if ((startNode != null) && (lastNode != null))
            {
                var newNode = new Node { Value = value };
                lastNode.NextNode = newNode;
                newNode.PrevNode = lastNode;
                lastNode = newNode;
                return;
            }
        }

        public void AddNodeAfter(Node node, int value)
        {
            if (startNode != null)
            {
                if (node == startNode)
                {
                    var newNode = new Node { Value = value };
                    newNode.PrevNode = startNode;
                    newNode.NextNode = startNode.NextNode;
                    startNode.NextNode.PrevNode = newNode;
                    startNode.NextNode = newNode;
                    return;
                }
                if (node == lastNode)
                {
                    var newNode = new Node { Value = value };
                    lastNode.NextNode = newNode;
                    newNode.PrevNode = lastNode;
                    lastNode = newNode;
                    return;
                }
                var cNode = startNode;
                while (cNode.NextNode != null)
                {
                    if (cNode == node)
                    {
                        var newNode = new Node { Value = value };
                        newNode.PrevNode = cNode;
                        newNode.NextNode = cNode.NextNode;
                        cNode.NextNode.PrevNode = newNode;
                        cNode.NextNode = newNode;                        
                        break;
                    }
                    cNode = cNode.NextNode;
                }
            }
        }

        public Node FindNode(int searchValue)
        {
            if (startNode != null)
            {
                var cNode = startNode;
                while (cNode.NextNode != null)
                {
                    if (cNode.Value == searchValue)
                    {
                        return cNode;
                    }
                    cNode = cNode.NextNode;
                }
                if (cNode.Value == searchValue)
                {
                    return cNode;
                }
            }
            return null;
        }

        public int GetCount()
        {
            int counter = 0;
            if (startNode != null)
            {
                var cNode = startNode;
                counter++;
                while (cNode.NextNode != null)
                {
                    counter++;
                    cNode = cNode.NextNode;
                }
                return counter;
            }
            return counter;
        }

        public void RemoveNode(int index)
        {
            if ((this.GetCount() - 1) < index)
            {
                return;
            }

            int counter = 0;
            if (startNode != null)
            {
                if (index == 0)
                {
                    startNode = startNode.NextNode;
                    return;
                }
                if (index == (this.GetCount()-1))
                {
                    lastNode = lastNode.PrevNode;
                    lastNode.NextNode = null;
                    return;
                }
                var cNode = startNode;
                while (true)
                {
                    if (counter == index)
                    {
                        cNode.PrevNode.NextNode = cNode.NextNode;
                        cNode.NextNode.PrevNode = cNode.PrevNode;
                        return;
                    }
                    counter++;
                    cNode = cNode.NextNode;
                }
                
            }
        }

        public void RemoveNode(Node node)
        {
            if (startNode != null)
            {
                if (node == startNode)
                {
                    startNode = startNode.NextNode;
                    return;
                }
                if (node == lastNode)
                {
                    lastNode = lastNode.PrevNode;
                    lastNode.NextNode = null;
                    return;
                }
                var cNode = startNode;
                while (cNode.NextNode != null)
                {
                    if (cNode == node)
                    {
                        cNode.PrevNode.NextNode = cNode.NextNode;
                        cNode.NextNode.PrevNode = cNode.PrevNode;
                        break;
                    }
                    cNode = cNode.NextNode;
                }
            }
        }
    }
}
