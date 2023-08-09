using System.Collections.Specialized;

namespace Graphs
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Graph<char> graph = new Graph<char>();

            var a = new Vertex<char>('A');
            var b = new Vertex<char>('B');
            var c = new Vertex<char>('C');
            var d = new Vertex<char>('D');
            var e = new Vertex<char>('E');
            var f = new Vertex<char>('F');
            var g = new Vertex<char>('G');
            var h = new Vertex<char>('H');


            graph.AddVertex(a);
            graph.AddVertex(b);
            graph.AddVertex(c);
            graph.AddVertex(d);
            graph.AddVertex(e);
            graph.AddVertex(f);
            graph.AddVertex(g);
            graph.AddVertex(h);



            graph.AddEdge(a, b, 10);
            graph.AddEdge(a, c, 2);
            graph.AddEdge(a, e, 50);
            graph.AddEdge(b, e, 5);
            graph.AddEdge(c, e, 20);
            graph.AddEdge(d, f, 50);
            graph.AddEdge(e, f, 3);

            //var DFSList = graph.DepthFirstSearch(a, g);

            //for (int i = DFSList.Count - 1; i >= 0; i--)
            //    Console.Write(DFSList[i]);

            //Console.WriteLine();

            //var BFSList = graph.BredthFirstSearch(a, g);
            //for (int i = BFSList.Count - 1; i >= 0; i--)
            //    Console.Write(BFSList[i]);

            var path = graph.Dijkstra(a, f);
            ;
        }
    }
}