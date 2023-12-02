using System;
using System.Collections;
using System.Collections.Generic;


namespace Lib
{

    public class Node<T>
    {
        public T Value { get; set; }
        public Node(T value)
        {
            this.Value = value;
        }

        public Node<T> Prev { get; set; }
        public Node<T> Next { get; set; }
    }

    public class MyQueue<T> : IEnumerable<T>
    {

        public event Action<T> AddedEvent;
        public event Action<T> RemovedEvent;
        public event Action ClearedEvent;

        Node<T> first;
        Node<T> end;
        int count = 0;

        public MyQueue()
        {
        }


        public void Enqueue(T value)
        {
            Node<T> node = new Node<T>(value);

            if (first == null)
                first = node;
            else
            {
                end.Next = node;
                node.Prev = end;
            }
            end = node;
            count++;

            AddedEvent?.Invoke(value);
        }
        public T Peek()
        {
            if (count == 0)
            {
                throw new InvalidOperationException("Помилка, черга пуста");
            }

            return first.Value;
        }
        public T Dequeue()
        {
            if (count == 0)
            {
                throw new InvalidOperationException("Помилка при виддалені елемента, черга пуста");
            }
            T output = first.Value;
            if (count == 1)
            {
                first = end = null;
            }
            else
            {
                first = first.Next;
                first.Prev = null;
            }
            count--;

            RemovedEvent?.Invoke(output);

            return output;
        }

        public void Clear()
        {
            first = null;
            end = null;
            count = 0;

            ClearedEvent?.Invoke();
        }

        public bool Contains(T data)
        {
            Node<T> current = first;
            while (current != null)
            {
                if (current.Value != null && current.Value.Equals(data))
                    return true;
                current = current.Next;
            }
            return false;
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            Node<T> current = first;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

    }
}
