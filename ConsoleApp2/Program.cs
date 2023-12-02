using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            var Queue = new MyQueue<string>();


            void OnAdded(string data)
            {
                Console.WriteLine("Елемент додано: " + data);
            }
            void OnRemoved(string data)
            {
                Console.WriteLine("Елемент видалено: " + data);
            }
            void OnCleared()
            {
                Console.WriteLine("Черга була очищена");
            }

            Queue.AddedEvent += OnAdded;
            Queue.RemovedEvent += OnRemoved;
            Queue.ClearedEvent += OnCleared;


            bool isExit = false;
            do
            {
                try
                {
                    Console.Clear();

                    Console.WriteLine("1 - Показати елементи");
                    Console.WriteLine("2 - Додати елемент");
                    Console.WriteLine("3 - Дістати елемент з видаленням");
                    Console.WriteLine("4 - Дістати елемент без видалення");
                    Console.WriteLine("5 - Перевірка на наявність елемента");
                    Console.WriteLine("6 - Очистити чергу");

                    Console.WriteLine("7 - Вийти\n");


                    string action = Console.ReadLine();


                    if (action == "1")
                    {
                        foreach (var item in Queue)
                        {
                            Console.WriteLine(item);
                        }

                        Console.ReadKey();
                    }
                    else if (action == "2")
                    {
                        Console.WriteLine("Введіть, що хочете додати");
                        string newElement = Console.ReadLine();
                        Queue.Enqueue(newElement);

                    }
                    else if (action == "3")
                    {
                        string element = Queue.Dequeue();

                        if (element != null)
                        {
                            Console.WriteLine("Видалено елемент: " + element);
                            Console.ReadKey();
                        }
                    }
                    else if (action == "4")
                    {
                        Console.WriteLine("Елемент:");
                        Console.WriteLine(Queue.Peek());
                        Console.ReadKey();
                    }
                    else if (action == "5")
                    {
                        Console.WriteLine("Введіть елемент для перевірки");
                        string element = Console.ReadLine();

                        if (Queue.Contains(element))
                        {
                            Console.WriteLine("Елемент присутній");
                        }
                        else
                        {
                            Console.WriteLine("Елемент відсутній");
                        }

                        Console.ReadKey();
                    }
                    else if (action == "6")
                    {
                        Queue.Clear();
                    }
                    else if (action == "7")
                    {
                        Queue.AddedEvent -= OnAdded;
                        Queue.RemovedEvent -= OnRemoved;
                        Queue.ClearedEvent -= OnCleared;
                        return;
                    }


                    Console.ReadKey();
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine("Виникла помилка: " + ex.Message);
                    Console.ReadKey();
                };
            } while (!isExit);
        }

    }
}
