﻿using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class Graph<T>
    {
        private List<Vertex<T>> vertices;

        public IReadOnlyList<Vertex<T>> Vertices => vertices;
        public IReadOnlyList<Edge<T>> Edges
        {
            get
            {
                var edges = new List<Edge<T>>();

                for (int i = 0; i < VertexCount; i++)
                {
                    edges.AddRange(vertices[i].Neighbors);
                }

                return edges;
            }
        }
        public int VertexCount => vertices.Count;


        public Graph()
        {
            vertices = new List<Vertex<T>>();
        }

        /*private bool AreNeighbors(Vertex<T> a, Vertex<T> b)
        {
            bool neighbor1 = false;
            bool neighbor2 = false;

            for (int i = 0; i < a.NeighborCount; i++)
            {
                if (a.Neighbors[i].Equals(b))
                {
                    neighbor1 = true;
                    break;
                }
            }

            for (int i = 0; i < b.NeighborCount; i++)
            {
                if (b.Neighbors[i].Equals(a))
                {
                    neighbor2 = true;
                    break;
                }
            }

            return neighbor1 == neighbor2;
        }*/

        public void AddVertex(Vertex<T> vertex)
        {
            if (vertex != null && vertex.NeighborCount == 0 && !vertices.Contains(vertex))
            {
                vertices.Add(vertex);
            }
        }

        public bool RemoveVertex(Vertex<T> vertex)
        {
            if (!vertices.Contains(vertex)) return false;

            for (int i = 0; i < vertex.NeighborCount; i++)
            {
                //ertex.Neighbors[i].Neighbors.Remove(vertex);
                vertex.Neighbors.Remove(vertex.Neighbors[i]);
            }

            vertices.Remove(vertex);

            return true;
        }

        private Edge<T>? ContainsEdge(Vertex<T> a, Vertex<T> b)
        {
            for (int i = 0; i < a.NeighborCount; i++)
            {
                if (a.Neighbors[i].StartingPoint == a && a.Neighbors[i].EndingPoint == b)
                {
                    return a.Neighbors[i];
                }
            }

            return null;
        }

        public bool AddEdge(Vertex<T> a, Vertex<T> b, float distance)
        {
            if (a == null || b == null) return false;

            if (vertices.Contains(a) && vertices.Contains(a) && ContainsEdge(a, b) == null)
            {
                a.Neighbors.Add(new Edge<T>(a, b, distance));
            }

            return true;
        }

        public bool RemoveEdge(Vertex<T> a, Vertex<T> b)
        {
            if (a == null || b == null) return false;

            if (ContainsEdge(a, b) != null)
            {
                a.Neighbors.Remove(ContainsEdge(a, b));
                return true;
            }

            return false;
        }

        public Vertex<T>? Search(T value)
        {
            foreach (var vertex in vertices)
            {
                if (vertex.Value!.Equals(value))
                {
                    return vertex;
                }
            }

            return null;
        }

        public List<Vertex<T>>? DepthFirstSearch(Vertex<T> startingVertex, Vertex<T> endingVertex)
        {
            var list = new List<Vertex<T>>();

            //set all vertices isVisited to false
            for (int i = 0; i < Vertices.Count; i++)
                vertices[i].isVisited = false;
            
            var vertexStack = new Stack<Vertex<T>>();

            var current = startingVertex;

            vertexStack.Push(current);

            do
            {
                current = vertexStack.Pop();

                //if reached the ending vertex
                if(current == endingVertex)
                {
                    list.Add(current);
                    //go backwards starting from endingVertex
                    while (current != startingVertex)
                    {
                        //add founder to list and traverse backwards
                        list.Add(current.founder);
                        current = current.founder;
                    }
                    return list;
                }

                if (!current.isVisited)
                {
                    for (int i = 0; i < current.NeighborCount; i++)
                    {
                        vertexStack.Push(current.Neighbors[i].EndingPoint);
                        if (!current.Neighbors[i].EndingPoint.isVisited)
                        {
                            //set non visited neighbor's founder to current
                            current.Neighbors[i].EndingPoint.founder = current;
                        }
                    }
                    //set current visited to true
                    current.isVisited = true;
                }

            } while (vertexStack.Count > 0); //go until we reached endingVertex or stack count is 0

            return null;
        }

        public List<Vertex<T>>? BredthFirstSearch(Vertex<T> startingVertex, Vertex<T> endingVertex)
        {
            var list = new List<Vertex<T>>();
            var vertexQueue = new Queue<Vertex<T>>();

            for(int i = 0; i < Vertices.Count; i++)
                Vertices[i].isVisited = false;

            var current = startingVertex;

            vertexQueue.Enqueue(current);
            
            do
            {
                current = vertexQueue.Dequeue();

                if (current == endingVertex)
                {
                    list.Add(current);
                    while (current != startingVertex)
                    {
                        list.Add(current.founder);
                        current = current.founder;
                    }
                    return list;
                }

                for(int i = 0; i < current.NeighborCount; i++)
                {
                    if (!current.Neighbors[i].EndingPoint.isVisited)
                    {
                        vertexQueue.Enqueue(current.Neighbors[i].EndingPoint);
                        current.Neighbors[i].EndingPoint.founder = current;
                    }
                }

                current.isVisited = true;

            } while (vertexQueue.Count > 0);

            return null;
        }
    }
}

