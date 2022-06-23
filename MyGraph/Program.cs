using System;

namespace MyGraph
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Basic Graph
            BasicGraph bg = new BasicGraph(8);
            //bg.AddEdge(0, 2);
            //bg.AddEdge(1, 3);
            //bg.AddEdge(1, 4);
            //bg.AddEdge(2, 5);
            //bg.AddEdge(3, 5);
            //bg.AddEdge(4, 6);
            //bg.AddEdge(6, 7);
            //bg.PrintAdjacencyList();
            //bg.CreateAdjacencyMatrix(bg);
            #endregion

            #region Standard Graph
            StandardGraph sg = new StandardGraph();
            //sg.AddNode(1);
            //sg.AddNode(2);
            //sg.AddNode(3);
            //sg.AddNode(4);
            //sg.AddNode(5);
            //sg.AddNode(6);
            //sg.AddNode(7);
            //sg.AddNode(8);

            //sg.AddEdge(1, 3);
            //sg.AddEdge(3, 6);
            //sg.AddEdge(2, 4);
            //sg.AddEdge(2, 5);
            //sg.AddEdge(5, 7);
            //sg.AddEdge(7, 8);

            //sg.ToString();
            #endregion

            #region Generic Graph
            Graph<int> gg = new Graph<int>();
            gg.AddNode(1);
            gg.AddNode(4);
            gg.AddNode(5);
            gg.AddNode(7);
            gg.AddNode(10);
            gg.AddNode(11);
            gg.AddNode(12);
            gg.AddNode(42);

            gg.AddEdge(1, 5);
            gg.AddEdge(4, 11);
            gg.AddEdge(4, 42);
            gg.AddEdge(5, 11);
            gg.AddEdge(5, 12);
            gg.AddEdge(5, 42);
            gg.AddEdge(7, 10);
            gg.AddEdge(7, 11);
            gg.AddEdge(10, 11);
            gg.AddEdge(11, 42);
            gg.AddEdge(12, 42);

            Graph<int>.PrintGraph(gg);
            #endregion

            #region Searching in Graph
            int source = 4, destination = 1;

            gg.Search(source, destination, gg, SearchType.BFS);

            #endregion
        }
    }
}
