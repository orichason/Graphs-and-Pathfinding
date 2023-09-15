using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Comparers
{
    public class AStarComparer : IComparer<Vertex<Vector2>>
    {
        public int Compare(Vertex<Vector2>? vertex, Vertex<Vector2>? goal)
        {
            if (vertex.FinalDistance < goal.FinalDistance) return -1;

            else if (vertex.FinalDistance > goal.FinalDistance) return 1;

            else return 0;
        }
    }
}
