using HeapTree;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public static class Extensions
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
                    List<Vertex<Vector2>> path = new();

                    while (currentVertex != startVertex)
                    {
                        path.Add(currentVertex);
                        currentVertex = currentVertex.founder;
                    }

                    path.Add(currentVertex);

                    return path;
                }

                for (int i = 0; i < currentVertex.NeighborCount; i++)
                {
                    if (!currentVertex.Neighbors[i].EndingPoint.isVisited)
                    {
                        priorityQueue.Insert(currentVertex.Neighbors[i].EndingPoint);
                        currentVertex.Neighbors[i].EndingPoint.founder = currentVertex;
                        currentVertex.Neighbors[i].EndingPoint.DistanceFromEnd = Distance(currentVertex.Neighbors[i].EndingPoint, endVertex);
                        currentVertex.Neighbors[i].EndingPoint.isVisited = true;
                    }
                }

            } while (priorityQueue.Count > 0);

            throw new ArgumentException("Can't find a path");
        }

        public static List<Vertex<Vector2>> AStar(this Graph<Vector2> graph, Vertex<Vector2> startVertex, Vertex<Vector2> endVertex)
        {
            MinHeap<Vertex<Vector2>> priorityQueue = new MinHeap<Vertex<Vector2>>(new AStarComparer());

            for (int i = 0; i < graph.VertexCount; i++)
            {
                graph.Vertices[i].DistanceFromStart = float.PositiveInfinity;
                graph.Vertices[i].DistanceFromEnd = float.PositiveInfinity;
                graph.Vertices[i].FinalDistance = graph.Vertices[i].DistanceFromStart + graph.Vertices[i].DistanceFromEnd;
                graph.Vertices[i].founder = null;
                graph.Vertices[i].isVisited = false;
            }

            startVertex.DistanceFromStart = 0;
            startVertex.DistanceFromEnd = startVertex.Distance(endVertex);
            priorityQueue.Insert(startVertex);

            Vertex<Vector2> currentVertex;
            do
            {
                currentVertex = priorityQueue.Pop();

                if (currentVertex.isVisited) continue;

                currentVertex.isVisited = true;

                if(endVertex.isVisited)
                {
                    List<Vertex<Vector2>> path = new();

                    while (currentVertex != startVertex)
                    {
                        path.Add(currentVertex);
                        currentVertex = currentVertex.founder;
                    }

                    path.Add(currentVertex);

                    return path;
                }

                for (int i = 0; i < currentVertex.NeighborCount; i++)
                {
                    Vertex<Vector2> neighbor = currentVertex.Neighbors[i].EndingPoint;
                    float tentativeDistance = currentVertex.DistanceFromStart + currentVertex.Neighbors[i].Distance;

                    if (tentativeDistance < neighbor.DistanceFromStart)
                    {
                        neighbor.DistanceFromStart = tentativeDistance;
                        neighbor.founder = currentVertex;
                        neighbor.FinalDistance = neighbor.DistanceFromStart + neighbor.Distance(endVertex);
                    }

                    if (!neighbor.isVisited)
                    {
                        priorityQueue.Insert(neighbor);
                    }
                }
            } while (priorityQueue.Count > 0);

            throw new ArgumentException("Can't find a path");
        }
    }
}
