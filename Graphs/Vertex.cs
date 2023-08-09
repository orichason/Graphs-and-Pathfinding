using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class Vertex<T> : IComparable<Vertex<T>>
    {
        public float DistanceFromStart;
        public T Value { get; set; }
        public List<Edge<T>> Neighbors { get; set; }

        public int NeighborCount => Neighbors.Count;

        public Vertex<T> founder;
        public bool isVisited;
        public Vertex (T value)
        {
            this.Value = value;
            Neighbors = new List<Edge<T>>();
            this.isVisited = false;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public int CompareTo(Vertex<T>? other)
        {
            if (DistanceFromStart < other.DistanceFromStart) return -1;

            else if (DistanceFromStart > other.DistanceFromStart) return 1;

            else return 0;
        }

    }
}
