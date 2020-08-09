using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Core
{
    public class QFDynamicConnector:IDynamicConnector
    {
        public QFDynamicConnector(int pointCount)
        {
            PointCount = pointCount;
            var points = new int[pointCount];
            for (int index = 0; index < points.Length; index++) 
                points[index] = index;
            Points = points;
        }

        public QFDynamicConnector(int[] points)
        {
            PointCount = points.Length;
            Points = points;
        }

        public int[] Points { get; private set; }

        public int PointCount { get; private set; }
        
        public void Union(int first, int second)
        {
            var firstId = FindComponentId(first);
            var secondId = FindComponentId(second);
            for (int i = 0; i < PointCount; i++)
            {
                if (Points[i] == firstId) 
                    Points[i] = secondId;
            }
        }

        public bool IsConnected(int first, int second)
        {
            return Points[first] == Points[second];
        }

        public int FindComponentId(int point)
        {
            if (point >= Points.Length)
            {
                throw new InvalidOperationException($"Points does not contain point {point}");
            }
            return Points[point];
        }

        public int GetComponentCount()
        {
            var buffer = new List<int>();
            foreach (var component in Points)
                if (!buffer.Contains(component))
                    buffer.Add(component);
            return buffer.Count;
        }
    }
}