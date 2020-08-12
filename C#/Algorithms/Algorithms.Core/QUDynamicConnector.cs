using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Algorithms.Core
{
    /// <summary>
    /// Quick Union
    /// </summary>
    public class QuDynamicConnector : IDynamicConnector
    {
        public QuDynamicConnector(int pointCount)
        {
            PointCount = pointCount;
            var points = new int[pointCount];
            for (int index = 0; index < points.Length; index++) 
                points[index] = index;
            Points = points;
            Sizes = new[] {1, 1, 1, 1, 1, 1, 1, 1, 1, 1};
        }

        public QuDynamicConnector(int[] points)
        {
            Points = points;
            Sizes = InitializeSizes(points);
            PointCount = points.Length;
        }

        private int[] InitializeSizes(int[] points)
        {
            var sizes = new int[10];
            var roots = new int[10];
            for (var element = 0; element < points.Length; element++)
            {
                var root = FindRoot(element);
                roots[element] = root;
            }
            
            for (var index = 0; index < sizes.Length; index++)
            {
                sizes[index] = roots.Count(r => r == roots[index]);
            }

            return sizes;
        }

        public int[] Points { get; }
        public int[] Sizes { get; private set; }
        public int PointCount { get; }
        
        public void Union(int first, int second)
        {
            var firstParent = FindRoot(first);
            var secondParent = FindRoot(second);
            var firstSize = Sizes[firstParent];
            var secondSize = Sizes[secondParent];
            var newSize = Sizes[secondParent] + Sizes[firstParent];
            if (firstSize < secondSize)
            {
                Points[firstParent] = secondParent;
                UpdateSizes(secondParent, newSize);
            }
            else
            {
                Points[secondParent] = firstParent;
                UpdateSizes(firstParent, newSize);
            }
        }

        private void UpdateSizes(int target, int newSize)
        {
            for (var element = 0; element < Points.Length; element++)
            {
                var point = Points[element];
                if (FindRoot(point) == target)
                {
                    Sizes[element] = newSize;
                }
            }
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
            while (element != Points[element])
            {
                Points[element] = Points[Points[element]]; //compression
                element = Points[element];
            }
            return element;
        }
    }
}