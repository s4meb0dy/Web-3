using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lib;

namespace UnitTestProject1
{
    [TestClass]
    public class NodeTests
    {
        [TestMethod]
        public void Node_Constructor_SetsValue()
        {
            // Arrange
            int expectedValue = 42;

            // Act
            Node<int> node = new Node<int>(expectedValue);

            // Assert
            Assert.AreEqual(expectedValue, node.Value);
        }

        [TestMethod]
        public void Node_PrevAndNext_SetCorrectly()
        {
            // Arrange
            Node<int> node1 = new Node<int>(1);
            Node<int> node2 = new Node<int>(2);

            // Act
            node1.Next = node2;
            node2.Prev = node1;

            // Assert
            Assert.AreEqual(node2, node1.Next);
            Assert.AreEqual(node1, node2.Prev);
        }

        [TestMethod]
        public void Node_PrevAndNext_CanBeNull()
        {
            // Arrange
            Node<int> node = new Node<int>(42);

            // Act & Assert
            Assert.IsNull(node.Prev);
            Assert.IsNull(node.Next);
        }
    }
}