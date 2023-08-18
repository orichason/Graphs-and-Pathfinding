using System;
using System.Data;
using System.Numerics;
using System.Reflection;

using Graphs;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using static System.Math;

namespace GraphTest
{
    [TestClass]
    public class DFS
    {
        /// <summary>
        /// Checks for DFS with random input
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="graphSize"></param>
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

        /// <summary>
        /// Checks with manual input
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        [TestMethod]
        [DataRow(0, 6)]
        public void ManualDFS(int startIndex, int endIndex)
        {
            var graph = ExampleGraph();

            VerifyPath<char>(graph, graph.Vertices[startIndex], graph.Vertices[endIndex]);
        }
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

            else
            {
                //makes sure founders are correct in list
                for (int i = 0; i < list.Count; i++)
                {
                    if (i != list.Count - 1)
                    {
                        Assert.IsTrue(list[i].founder == list[i + 1]);
                    }
                }
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
        /// <summary>
        /// Creates example char graph for testing
        /// </summary>
        /// <returns></returns>
        static Graph<char> ExampleGraph()
        {
            Graph<char> graph = new Graph<char>();

            var a = new Vertex<char>('A');
            var b = new Vertex<char>('B');
            var d = new Vertex<char>('D');
            var e = new Vertex<char>('E');
            var f = new Vertex<char>('F');
            var g = new Vertex<char>('G');
            var h = new Vertex<char>('H');


            graph.AddVertex(a);
            graph.AddVertex(b);
            graph.AddVertex(d);
            graph.AddVertex(e);
            graph.AddVertex(f);
            graph.AddVertex(g);
            graph.AddVertex(h);



            graph.AddEdge(a, b, 1);
            graph.AddEdge(a, e, 1);
            graph.AddEdge(e, a, 1);
            graph.AddEdge(b, d, 1);
            graph.AddEdge(b, f, 1);
            graph.AddEdge(b, g, 1);
            graph.AddEdge(g, h, 1);

            return graph;
        }
    }

    [TestClass]
    public class BFS
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="seed"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns> graph with random vertices and edges</returns>
        static Graph<char> SampleGraph(int columns, int seed, out Vertex<char> start, out Vertex<char> end)
        {
            Graph<char> graph = new();
            
            Random random = new(seed);
           
            start = new('a');
            Vertex<char> path = new('b');
            end = new('c');
            graph.AddVertex(start);
            graph.AddVertex(path);
            graph.AddVertex(end);
            
            if(columns == 0)
            {
                graph.AddEdge(start, end, random.Next(1,10));
                graph.AddEdge(start, path, random.Next(1, 10));
                graph.AddEdge(path, end, random.Next(1, 10));

                return graph;
            }

            int rows;

            Vertex<char>[][] jaggedArray = new Vertex<char>[columns][];

            for (int i = 0; i < columns; i++)
            {
                rows = random.Next(2, 5);
                jaggedArray[i] = new Vertex<char>[rows];
                for (int j = 0; j < rows; j++)
                {
                    graph.AddVertex(jaggedArray[i][j] = new Vertex<char>((char)random.Next(97, 123)));
                }
            }

            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < jaggedArray[i].Length; j++)
                {
                    int columnMin = Max(i - 1, 0);
                    int columnMax = Min(columns, i + 2);
                    
                    int neighborCount = random.Next(3, 9);
                    for (int k = 0; k < neighborCount; k++)
                    {
                        int randomColumn = random.Next(columnMin, columnMax); //getting random column

                        graph.AddEdge(jaggedArray[i][j], jaggedArray[randomColumn][random.Next(0, jaggedArray[randomColumn].Length)], random.Next(1, 10));

                    }
                }
            }

            for (int i = 0; i < jaggedArray[0].Length - 1; i++)
            {
                graph.AddEdge(start, jaggedArray[0][i], random.Next(1,10));
            }
            graph.AddEdge(start, path, random.Next(1, 10));
            graph.AddEdge(path, end, random.Next(1, 10));
            for (int i = 0; i < jaggedArray[jaggedArray.Length - 1].Length; i++)
            {
                graph.AddEdge(jaggedArray[jaggedArray.Length - 1][i], end, random.Next(1, 10));
            }
            return graph;
        }
        /// <summary>
        /// Verifying that path is max 3 vertices long
        /// </summary>
        /// <param name="column"></param>

        [TestMethod]
        [DataRow(3)]
        [DataRow(0)]
        [DataRow(10)]
        [DataRow(2)]
        public void RandomBFS(int column)
        {
            var graph = SampleGraph(column, 1, out var start, out var end);
            var path = graph.BredthFirstSearch(start, end);

            Assert.IsTrue(path.Count <= (column == 0? 2 : 3));            
        }

    }

    [TestClass]
    public class Pathfinding
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="seed"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns> graph with random vertices and edges</returns>
        static Graph<char> SampleGraph(int columns, int seed, out Vertex<char> start, out Vertex<char> end)
        {
            Graph<char> graph = new();

            Random random = new(seed);

            start = new('a');
            Vertex<char> path = new('b');
            end = new('c');
            graph.AddVertex(start);
            graph.AddVertex(path);
            graph.AddVertex(end);

            if (columns == 0)
            {
                graph.AddEdge(start, end, 100000);
                graph.AddEdge(start, path, 100000);
                graph.AddEdge(path, end, 100000);

                return graph;
            }

            int rows;

            Vertex<char>[][] jaggedArray = new Vertex<char>[columns][];

            for (int i = 0; i < columns; i++)
            {
                rows = random.Next(2, 5);
                jaggedArray[i] = new Vertex<char>[rows];
                for (int j = 0; j < rows; j++)
                {
                    graph.AddVertex(jaggedArray[i][j] = new Vertex<char>((char)random.Next(97, 123)));
                }
            }

            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < jaggedArray[i].Length; j++)
                {
                    int columnMin = Max(i - 1, 0);
                    int columnMax = Min(columns, i + 2);

                    int neighborCount = random.Next(3, 9);
                    for (int k = 0; k < neighborCount; k++)
                    {
                        int randomColumn = random.Next(columnMin, columnMax); //getting random column

                        graph.AddEdge(jaggedArray[i][j], jaggedArray[randomColumn][random.Next(0, jaggedArray[randomColumn].Length)], random.Next(1, 10));

                    }
                }
            }

            for (int i = 0; i < jaggedArray[0].Length - 1; i++)
            {
                graph.AddEdge(start, jaggedArray[0][i], random.Next(1, 10));
            }
            graph.AddEdge(start, path, 1);
            graph.AddEdge(path, end, 1);
            for (int i = 0; i < jaggedArray[jaggedArray.Length - 1].Length; i++)
            {
                graph.AddEdge(jaggedArray[jaggedArray.Length - 1][i], end, random.Next(1, 10));
            }
            return graph;
        }
        [TestMethod]
        [DataRow(4)]
        [DataRow(1)]
        [DataRow(10)]
        [DataRow(20)]
        [DataRow(0)]
        public void Dijkstra(int column)
        {
            var graph = SampleGraph(column, 1, out var start, out var end);
            var path = graph.Dijkstra(start, end);

            Assert.IsTrue(path.Count <= (column == 0 ? 2 : 3));
        }

        [TestMethod]
        [DataRow(5, 5, 1, 0.3)]
        public /*Graph<Vector2>*/ void BestFirstSearchGraph(int columns, int rows, int seed,float holePercentage)
        {
            Random random = new();

            Graph<Vector2> graph = new();

            Vector2[,] vectors = new Vector2[columns, rows];

            float numOfHoles = (float)(vectors.Length * holePercentage);

            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    vectors[i,j] = new Vector2(i, j);
                }
            }

            return /*graph*/;
        }

    }
}