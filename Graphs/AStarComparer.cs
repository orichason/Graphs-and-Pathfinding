using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class AStarComparer : IComparer<Vertex<Vector2>>
    {
        public int Compare(Vertex<Vector2>? vertex, Vertex<Vector2>? goal)
        {
            if ((vertex.DistanceFromStart + vertex.DistanceFromEnd) < (goal.DistanceFromStart + goal.DistanceFromEnd)) return -1;

            else if ((vertex.DistanceFromStart + vertex.DistanceFromEnd) > (goal.DistanceFromStart + goal.DistanceFromEnd)) return 1;

            else return 0;
        }
    }
}
