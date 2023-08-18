namespace Graphs
{
#nullable disable
    public class DijkstraComparer<T> : IComparer<Vertex<T>>
    {
        public int Compare(Vertex<T> vertex, Vertex<T> other)
        {
            if (vertex.DistanceFromStart < other.DistanceFromStart) return -1;

            else if (vertex.DistanceFromStart > other.DistanceFromStart) return 1;

            else return 0;
        }
    }
}
