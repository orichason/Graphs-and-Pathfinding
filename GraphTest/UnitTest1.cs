using System;
using System.Data;
using System.Reflection;

using Graphs;

namespace GraphTest
{
    [TestClass]
    public class DFS
    {
        [TestMethod]
        [DataRow(97/*a*/, 122/*z*/, 20)]
        public void RandomDFS(int min, int max, int graphSize)
        {
            var random = new Random();
            Graph<char> graph = new Graph<char>();

            Vertex<char>[] vertices = new Vertex<char>[graphSize];

            //creates vertex with value of random index in array

            for (int i = 0; i < graphSize; i++)
            {
                vertices[i] = new Vertex<char>((char)random.Next(97, 123));
                graph.AddVertex(vertices[i]);
            }

            for (int i = 0; i < graphSize; i++)
            {
                //adding edge gets two random vertices from vertices array and a random distance(1-10)
                graph.AddEdge(vertices[random.Next(0, vertices.Length)], vertices[random.Next(0, vertices.Length)], random.Next(0, 11));
            }

            var startVertex = vertices[random.Next(0, vertices.Length)];
            var endVertex = vertices[random.Next(0, vertices.Length)];

            VerifyPath<char>(graph, startVertex, endVertex);
        }
        //TO DO: CREATE A FUNCTION THAT CHECKS FOR NON RANDOM DFS INPUT
        /// <summary>
        /// Verifies a depth first search
        /// </summary>
        /// <typeparam name="T">type of the graph</typeparam>
        /// <param name="graph">the graph being searched on</param>
        /// <param name="startVertex">vertex the search begins from</param>
        /// <param name="endVertex">vertex to search for</param>
        static void VerifyPath<T>(Graph<T> graph, Vertex<T> startVertex, Vertex<T> endVertex)
        {
            var list = graph.DepthFirstSearch(startVertex, endVertex);

            if (list == null)
            {
                var foundVertices = GetVertices(startVertex);

                Assert.IsFalse(foundVertices.Contains(endVertex));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="start"></param>
        /// <returns>hash set of all vertices connected to start</returns>
        static HashSet<Vertex<T>> GetVertices<T>(Vertex<T> start)
        {
            HashSet<Vertex<T>> foundVertices = new() { start };

            //gets all vertices connected to start
            GetVertices(foundVertices, start);
            return foundVertices;
        }

        static void GetVertices<T>(HashSet<Vertex<T>> verts, Vertex<T> current)
        {
            foreach (Edge<T> edge in current.Neighbors)
            {
                if (verts.Contains(edge.EndingPoint)) continue;
                verts.Add(edge.EndingPoint);
                GetVertices(verts, edge.EndingPoint);
            }
        }
    }
}