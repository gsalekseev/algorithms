using System;
using System.Collections.Generic;

namespace Algorithms.Core
{
    /// <summary>
    /// Quick Union
    /// </summary>
    public class QUDynamicConnector : IDynamicConnector
    {
        public QUDynamicConnector(int pointCount)
        {
            PointCount = pointCount;
            var points = new int[pointCount];
            for (int index = 0; index < points.Length; index++) 
                points[index] = index;
            Points = points;
        }

        public QUDynamicConnector(int[] points)
        {
            Points = points;
            PointCount = points.Length;
        }
        
        public int[] Points { get; }
        public int PointCount { get; }
        
        public void Union(int first, int second)
        {
            var firstParent = FindRoot(first);
            var secondParent = FindRoot(second);
            Points[firstParent] = secondParent;
        }

        public bool IsConnected(int first, int second)
        {
            return FindRoot(first) == FindRoot(second);
        }

        public int FindComponentId(int point)
        {
            if (point >= PointCount)
            {
                throw new InvalidOperationException($"No component with id {point}");
            }
            
            return FindRoot(point);
        }

        public int GetComponentCount()
        {
            var buffer = new List<int>();
            for (var index = 0; index < Points.Length; index++)
            {
                var component = Points[index];
                if (!buffer.Contains(component) && component == index)
                    buffer.Add(component);
            }

            return buffer.Count;
        }

        private int FindRoot(int element)
        {
            if (Points[element] == element)
                return element;
            return FindRoot(Points[element]);
        }
    }
}