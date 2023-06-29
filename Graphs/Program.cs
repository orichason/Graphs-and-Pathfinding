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

            var DFSList = graph.DepthFirstSearch(a, h);

            for (int i = DFSList.Count - 1; i >= 0; i--)
                Console.Write(DFSList[i]);

            Console.WriteLine();

            var BFSList = graph.BredthFirstSearch(a, h);
            for (int i = BFSList.Count - 1; i >= 0; i--)
                Console.Write(BFSList[i]);
        }
    }
}