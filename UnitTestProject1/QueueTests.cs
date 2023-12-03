using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lib;
using System.Collections.Generic;
using System.Linq;

namespace Queue.Tests
{
    [TestClass]
    public class QueueTests
    {
        [TestMethod]
        public void Enqueue_AddsElementToQueue()
        {
            MyQueue<int> queue = new MyQueue<int>();

            queue.Enqueue(42);

            Assert.AreEqual(1, queue.Count);
            Assert.AreEqual(42, queue.Peek());
        }

        [TestMethod]
        public void Dequeue_RemovesElementFromQueue()
        {
            MyQueue<int> queue = new MyQueue<int>();
            queue.Enqueue(42);
            queue.Enqueue(54);

            int result = queue.Dequeue();

            Assert.AreEqual(1, queue.Count);
            Assert.AreEqual(42, result);

            result = queue.Dequeue();

            Assert.AreEqual(0, queue.Count);
            Assert.AreEqual(54, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Dequeue_OnEmptyQueue_ThrowsException()
        {
            MyQueue<int> queue = new MyQueue<int>();

            int result = queue.Dequeue();
        }

        [TestMethod]
        public void Peek_ReturnsElementWithoutRemovingIt()
        {
            MyQueue<int> queue = new MyQueue<int>();
            queue.Enqueue(42);

            int result = queue.Peek();

            Assert.AreEqual(1, queue.Count);
            Assert.AreEqual(42, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Peek_OnEmptyQueue_ThrowsException()
        {
            MyQueue<int> queue = new MyQueue<int>();

            int result = queue.Peek(); // This should throw an exception
        }

        [TestMethod]
        public void Clear_RemovesAllElementsFromQueue()
        {
            MyQueue<int> queue = new MyQueue<int>();
            queue.Enqueue(42);
            queue.Enqueue(24);

            queue.Clear();

            Assert.AreEqual(0, queue.Count);
            Assert.IsFalse(queue.Contains(42));
            Assert.IsFalse(queue.Contains(24));
        }

        [TestMethod]
        public void Contains_ReturnsTrueIfElementExistsInQueue()
        {
            MyQueue<int> queue = new MyQueue<int>();
            queue.Enqueue(42);
            queue.Enqueue(24);

            Assert.IsTrue(queue.Contains(42));
            Assert.IsTrue(queue.Contains(24));
        }

        [TestMethod]
        public void Contains_ReturnsFalseIfElementDoesNotExistInQueue()
        {
            MyQueue<int> queue = new MyQueue<int>();
            queue.Enqueue(42);

            Assert.IsFalse(queue.Contains(24));
        }

        [TestMethod]
        public void GetEnumerator_ReturnsAllElementsInCorrectOrder()
        {
            MyQueue<int> queue = new MyQueue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);

            List<int> result = queue.ToList();

            CollectionAssert.AreEqual(new List<int> { 1, 2, 3 }, result);
        }

        [TestMethod]
        public void GetEnumerator_OnEmptyQueue_ReturnsEmptyCollection()
        {
            MyQueue<int> queue = new MyQueue<int>();

            List<int> result = queue.ToList();

            CollectionAssert.AreEqual(new List<int>(), result);
        }
    }
}
