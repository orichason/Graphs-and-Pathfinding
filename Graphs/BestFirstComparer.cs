using System.Numerics;

namespace Graphs
{
    public class BestFirstComparer : IComparer<Vertex<Vector2>>
    {
        public int Compare(Vertex<Vector2>? vertex, Vertex<Vector2>? other)
        {
            if (vertex.DistanceFromEnd < other.DistanceFromEnd) return -1;

            else if (vertex.DistanceFromEnd > other.DistanceFromStart) return 1;

            return 0;
        }
    }
}
