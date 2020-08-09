namespace Algorithms.Core
{
    public interface IDynamicConnector
    {
        public int[] Points { get; }
        public int PointCount { get; }
        void Union(int first, int second);
        bool IsConnected(int first, int second);
        int FindComponentId(int point);
        int GetComponentCount();
    }
}