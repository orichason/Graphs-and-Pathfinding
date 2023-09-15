using System.Collections.Specialized;
using System.Numerics;

namespace Graphs
{
    internal class Program
    {
        static Graph<char> SampleGraph()
        {
            Graph<char> graph = new Graph<char>();

            var a = new Vertex<char>('A');
            var b = new Vertex<char>('B');
            var c = new Vertex<char>('C');
            var d = new Vertex<char>('D');
            var e = new Vertex<char>('E');
            var f = new Vertex<char>('F');
            var g = new Vertex<char>('G');
            var j = new Vertex<char>('J');
            var h = new Vertex<char>('H');


            graph.AddVertex(a);
            graph.AddVertex(b);
            graph.AddVertex(c);
            graph.AddVertex(d);
            graph.AddVertex(e);
            graph.AddVertex(f);
            graph.AddVertex(g);
            graph.AddVertex(j);
            graph.AddVertex(h);



            graph.AddEdge(a, b, 3);
            graph.AddEdge(b, c, 10);
            graph.AddEdge(c, d, -1);
            graph.AddEdge(d, e, 5);
            graph.AddEdge(e, c, -3);
            //graph.AddEdge(c, a, -10);
            graph.AddEdge(a, f, 3);
            graph.AddEdge(f, g, 13);
            graph.AddEdge(f, e, 2);
            graph.AddEdge(j, h,-2);

            return graph;

        }
        static void Main(string[] args)
        {
            Graph<char> graph = SampleGraph();

            bool hello = graph.BellmanFord(graph.Vertices[0]);

            Console.WriteLine(hello);
        }
    }
}