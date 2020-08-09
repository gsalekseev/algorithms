using System;
using Algorithms.Core;
using NUnit.Framework;

namespace Algorithms.Test
{
    [TestFixture]
    public class QfDynamicConnectorTests
    {
        private QFDynamicConnector _connector;
        
        [SetUp]
        public void SetUp()
        {
            int pointCount = 10;
            _connector = new QFDynamicConnector(pointCount);
        }
        
        [Test]
        public void Ctor_Initialize_Points_Sucess()
        {
            int pointCount = 10;
            var connector = new QFDynamicConnector(pointCount);
            Assert.AreEqual(pointCount,connector.PointCount);
        }
        
        [Test]
        public void Ctor_Initialize_PointsCount_Sucess()
        {
            var array = new int[10] {0, 1, 1, 8, 8, 0, 0, 1, 8, 8};
            var connector = new QFDynamicConnector(array);
            Assert.AreEqual(array.Length, connector.PointCount);
        }
        
        [Test]
        public void FindComponentIdIdentifier_ReturnCorrectIdentifier_WhenExists()
        {
            var array = new int[10] {0, 1, 1, 8, 8, 0, 0, 1, 8, 8};
            var connector = new QFDynamicConnector(array);
            var componentId = connector.FindComponentId(2);
            Assert.AreEqual(componentId, array[2]);
        }
        
        [Test]
        public void FindComponentId_ReturnCorrectIdentifier_WhenNotExists()
        {
            var array = new int[10] {0, 1, 1, 8, 8, 0, 0, 1, 8, 8};
            var connector = new QFDynamicConnector(array);
            Assert.Throws<InvalidOperationException>(() => connector.FindComponentId(111));
        }
        
        [Test]
        public void Union_ConnectTwoComponents()
        {
            var array = new int[10] {0, 1, 1, 8, 8, 0, 0, 1, 8, 8};
            var connector = new QFDynamicConnector(array);
            connector.Union(6, 1);
            var actual = connector.Points;
            var expected = new int[] {1, 1, 1, 8, 8, 1, 1, 1, 8, 8};
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void GetComponentCount_Return_3_WhenThreeComponentExists()
        {
            var array = new int[10] {0, 1, 1, 8, 8, 0, 0, 1, 8, 8};
            var connector = new QFDynamicConnector(array);
            var actual = connector.GetComponentCount();
            var expected = 3;
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void GetComponentCount_Return_1_WhenOneComponentExists()
        {
            var array = new int[10] {1, 1, 1, 1, 1, 1, 1, 1, 1, 1};
            var connector = new QFDynamicConnector(array);
            var actual = connector.GetComponentCount();
            var expected = 1;
            Assert.AreEqual(expected, actual);
        }
    }
}