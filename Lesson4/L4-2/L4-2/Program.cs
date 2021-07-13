using System;

namespace L4_2
{
    class Program
    {
        static void Main(string[] args)
        {
            TestNode();
        }

        public static void TestNode()
        {
            // Добавление набора чисел для образования дерева
            Tree tree = new Tree();
            tree.AddItem(50);
            tree.AddItem(20);
            tree.AddItem(10);
            tree.AddItem(30);
            tree.AddItem(100);
            tree.AddItem(70);
            tree.AddItem(110);
            tree.AddItem(5);
            tree.AddItem(15);
            tree.AddItem(25);
            tree.AddItem(35);
            tree.AddItem(80);
            tree.AddItem(90);
            tree.AddItem(65);

            // Вывод на экран дерева в линейной форме
            tree.PrintTree();
            Console.WriteLine("\n");

            // Проверка функций:
            Console.WriteLine($"Корневой элемент имеет значение {tree.GetRoot().Value}");
            Console.WriteLine($"Поиск элемента по значению 90 дал результат {tree.GetNodeByValue(90).Value}");
            Console.WriteLine();

            // Проверка удаления элементов
            tree.RemoveItem(20);
            tree.RemoveItem(50);
            tree.RemoveItem(80);

            // Вывод на экран дерева в линейной форме
            tree.PrintTree();
            Console.WriteLine("\n");
        }
    }

    // Элемент дерева
    public class TreeNode
    {
        public int Value { get; set; }
        public TreeNode LeftChild { get; set; }
        public TreeNode RightChild { get; set; }
    }
    public interface ITree
    {
        TreeNode GetRoot(); // Получение ссылки на корневой элемент
        void AddItem(int value); // Добавить узел
        void RemoveItem(int value); // Удалить узел по значению
        TreeNode GetNodeByValue(int value); // Получить узел дерева по значению
        void PrintTree(); // Вывести дерево в консоль
    }

    public class Tree : ITree
    {
        private TreeNode rootNode { get; set; }
        private TreeNode btweNode { get; set; }

        /*
         * При реализации функций AddItem и RemoveItem была использована информация из следующего источника:
         * https://code.tutsplus.com/tutorials/the-binary-search-tree--cms-20668
         */

        public void AddItem(int value)
        {
            if (rootNode == null)
            {
                rootNode = new TreeNode { Value = value };
                return;
            }
            if (rootNode != null)
            {
                AddTo(rootNode, value);
                return;
            }
        }

        private void AddTo(TreeNode node, int value)
        {
            if (value.CompareTo(node.Value) < 0)
            {
                if (node.LeftChild == null)
                {
                    node.LeftChild = new TreeNode { Value = value };
                }
                else
                {
                    AddTo(node.LeftChild, value);
                }
            }
            else
            {
                if (node.RightChild == null)
                {
                    node.RightChild = new TreeNode { Value = value };
                }
                else
                {
                    AddTo(node.RightChild, value);
                }
            }
        }
        public TreeNode GetNodeByValue(int value)
        {
            btweNode = null;
            FindObject(this.GetRoot(), value);
            return btweNode;
        }

        private void FindObject(TreeNode node, int value)
        {
            if (node != null)
            {
                if (node?.Value == value)
                {
                    btweNode = node;
                    return;
                }
                FindObject(node.LeftChild, value);
                FindObject(node.RightChild, value);
            }
        }

        public TreeNode GetRoot()
        {
            return rootNode;
        }

        public void PrintTree()
        {
            PrintRootTree(this.GetRoot());
        }

        private void PrintRootTree(TreeNode node)
        {
            if (node != null)
            {
                Console.Write($"{node.Value}");

                if (node.LeftChild != null || node.RightChild != null)
                {
                    Console.Write($"(");
                    if (node.LeftChild != null)
                    {
                        PrintRootTree(node.LeftChild);
                    }
                    else
                    {
                        Console.Write($"nil");
                    }
                    Console.Write($",");
                    if (node.RightChild != null)
                    {
                        PrintRootTree(node.RightChild);
                    }
                    else
                    {
                        Console.Write($"nil");
                    }
                    Console.Write($")");
                }
            }
        }

        public void RemoveItem(int value)
        {
            TreeNode current, parent;
            current = FindWithParent(value, out parent);

            if (current == null)
            {
                return;
            }

            if (current.RightChild == null)
            {
                if (parent == null)
                {
                    rootNode = current.LeftChild;
                }
                else
                {
                    int result = parent.Value.CompareTo(current.Value);
                    if (result > 0)
                    {
                        parent.LeftChild = current.LeftChild;
                    }
                    else if (result < 0)
                    {
                        parent.RightChild = current.LeftChild;
                    }
                }
            }
            else if (current.RightChild.LeftChild == null)
            {
                current.RightChild.LeftChild = current.LeftChild;

                if (parent == null)
                {
                    rootNode = current.RightChild;
                }
                else
                {
                    int result = parent.Value.CompareTo(current.Value);
                    if (result > 0)
                    {
                        parent.LeftChild = current.RightChild;
                    }
                    else if (result < 0)
                    {
                        parent.RightChild = current.RightChild;
                    }
                }
            }
            else
            {
                TreeNode leftmost = current.RightChild.LeftChild;
                TreeNode leftmostParent = current.RightChild;

                while (leftmost.LeftChild != null)
                {
                    leftmostParent = leftmost;
                    leftmost = leftmost.LeftChild;
                }
                leftmostParent.LeftChild = leftmost.RightChild;
                leftmost.LeftChild = current.LeftChild;
                leftmost.RightChild = current.RightChild;

                if (parent == null)
                {
                    rootNode = leftmost;
                }
                else
                {
                    int result = parent.Value.CompareTo(current.Value);
                    if (result > 0)
                    {
                        parent.LeftChild = leftmost;
                    }
                    else if (result < 0)
                    {
                        parent.RightChild = leftmost;
                    }
                }
            }
            return;
        }

        private TreeNode FindWithParent(int value, out TreeNode parent)
        {
            TreeNode current = rootNode;
            parent = null;

            while (current != null)
            {
                int result = current.Value.CompareTo(value);

                if (result > 0)
                {
                    parent = current;
                    current = current.LeftChild;
                }
                else if (result < 0)
                {
                    parent = current;
                    current = current.RightChild;
                }
                else
                {
                    break;
                }
            }
            return current;
        }
    }
}