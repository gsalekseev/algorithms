using System;
using Algorithms.Core;
using NUnit.Framework;

namespace Algorithms.Test
{
    [TestFixture]
    public class QuDynamicConnectorTests
    {
        private QuDynamicConnector _connector;
        
        [SetUp]
        public void SetUp()
        {
            var array = new int[] {0, 1, 9, 4, 9, 6, 6, 7, 8, 9};
            _connector = new QuDynamicConnector(array);
        }
        
        [Test]
        public void Ctor_InitializeFromPointCount()
        {
            var connector = new QuDynamicConnector(10);
            var array = new int[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            Assert.AreEqual(array, connector.Points);
        }
        
        [Test]
        public void Ctor_InitializeFromArray_Success()
        {
            var array = new int[] {0, 1, 9, 4, 9, 6, 6, 7, 8, 9};
            var connector = new QuDynamicConnector(array);
            Assert.AreEqual(array, connector.Points);
        }
        
        [Test]
        public void FindComponentIdIdentifier_ReturnCorrectIdentifier_WhenExists()
        {
            Assert.AreEqual(9, _connector.FindComponentId(2));
        }
        
        [Test]
        public void FindComponentId_ThrowsInvalidOperationException_WhenNotExists()
        {
            Assert.Throws<InvalidOperationException>(() => _connector.FindComponentId(233));
        }
        
        [Test]
        public void Union_ConnectTwoComponents_WithWeightAndCompression()
        {
            _connector.Union(9, 6);
            Assert.AreEqual(new int[] {0, 1, 9, 9, 9, 6, 9, 7, 8, 9}, _connector.Points);
        }
        
        [Test]
        public void GetComponentCount_Return_6_WhenSixComponentExists()
        {
            Assert.AreEqual(6, _connector.GetComponentCount());
        }
        
        [Test]
        public void IsConnected_ReturnTrue_WhenConnectionExists()
        {
            Assert.True(_connector.IsConnected(2, 3));
        }
        
        [Test]
        public void IsConnected_ReturnFalse_WhenConnectionDoesNotExists()
        {
            Assert.False(_connector.IsConnected(2, 5));
        }
        
        [Test]
        public void Ctor_InitializeSizes_WhenInitFromArray()
        {
            var expected = new int[] {1, 1, 4, 4, 4, 2, 2, 1, 1, 4};
            var actual = _connector.Sizes;
            Assert.AreEqual(expected, actual);
        }
    }
}