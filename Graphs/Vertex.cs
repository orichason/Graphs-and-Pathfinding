using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class Vertex<T>
    {
        public float DistanceFromStart;

        public float DistanceFromEnd;

        public float FinalDistance;

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

    }
}
