using HeapTree;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    static class Extensions
    {
        public static float Distance(this Vertex<Vector2> start, Vertex<Vector2> end)
        {
            //return MathF.Sqrt(MathF.Pow(end.Value.X - start.Value.X, 2) + MathF.Pow(end.Value.Y - start.Value.Y, 2));
            return Vector2.Distance(start.Value, end.Value);
        }   

        public static List<Vertex<Vector2>> BestFirstSearch(this Graph<Vector2> graph, Vertex<Vector2> startVertex, Vertex<Vector2> endVertex)
        {
            MinHeap<Vertex<Vector2>> priorityQueue = new(new BestFirstComparer());

            for (int i = 0; i < graph.Vertices.Count; i++)
            {
                graph.Vertices[i].isVisited = false;
                graph.Vertices[i].DistanceFromEnd = float.PositiveInfinity;
                graph.Vertices[i].founder = null;
            }

            startVertex.DistanceFromEnd = startVertex.Distance(endVertex);
            priorityQueue.Insert(startVertex);

            Vertex<Vector2> currentVertex;

            do
            {
                currentVertex = priorityQueue.Pop();

                if(currentVertex == endVertex)
                {
                    break;
                }

                if (currentVertex.isVisited) continue;

                for (int i = 0; i < currentVertex.NeighborCount; i++)
                {
                    if (!currentVertex.Neighbors[i].EndingPoint.isVisited)
                    {
                        priorityQueue.Insert(currentVertex.Neighbors[i].EndingPoint);
                    }
                }

            } while (priorityQueue.Count > 0);

            List<Vertex<Vector2>> path = new();

            currentVertex = endVertex;

            while (currentVertex != startVertex)
            {
                path.Add(currentVertex);
                currentVertex = currentVertex.founder;
            }

            path.Add(currentVertex);

            return path;
        }
    }
}
