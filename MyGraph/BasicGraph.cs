using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGraph
{
    public class BasicGraph
    {
        /* Glossary | pt-BR
         * 
         * Edges = Arestas
         * Vertex = Vertices
         * 
         */
        private int totalVertices;
        private LinkedList<int>[] linkedListArray;

        public BasicGraph(int n)
        {
            linkedListArray = new LinkedList<int>[n];

            for (int i = 0; i < linkedListArray.Length; i++)
            {
                linkedListArray[i] = new LinkedList<int>();
            }
        }

        public void AddEdge(int vertexIndex, int adjacentyVextex)
        {
            linkedListArray[vertexIndex].AddLast(adjacentyVextex);            
            this.totalVertices++;
        }

        // Print each Vertex and their Vertex connected
        public void PrintAdjacencyList()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("---------------------------------");
            sb.AppendLine("---------------------------------");
            sb.AppendLine("The Adjacency List Representation");
            sb.AppendLine("---------------------------------");

            for (int i = 0; i < linkedListArray.Length; i++)
            {
                sb.Append($"[Node Value: {i} with Neighbors");

                foreach (int vertex in this.linkedListArray[i])
                {
                    sb.Append($"-> {vertex}");
                }
                sb.Append(" ]");
                sb.AppendLine();
            }

            Console.WriteLine(sb);
        }

        public void CreateAdjacencyMatrix(BasicGraph graph)
        {
            int?[,] adjacencyMatrix = new int?[graph.totalVertices, graph.totalVertices];

            for (int parentVertex = 0; parentVertex < graph.totalVertices; parentVertex++)
            {
                LinkedList<int> parentNode = this.linkedListArray[parentVertex];

                for (int childNode = 0; childNode < graph.totalVertices; childNode++)
                {
                    if (parentVertex != childNode)
                    {
                        LinkedList<int> arc = parentNode.Find(childNode)?.List;

                        if (arc != null)
                        {
                            adjacencyMatrix[parentVertex, childNode] = 1;
                        }
                    }
                }
            }

            PrintAdjacencyMatrix(adjacencyMatrix, this.totalVertices);
        }

        public void PrintAdjacencyMatrix(int?[,] adjacencyMatrix, int count)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("-----------------------------------------");
            sb.AppendLine("-----------------------------------------");
            sb.AppendLine("The Graph Adjacency Matrix Representation");
            sb.AppendLine("-----------------------------------------");

            sb.Append("Nodes");

            for (int node = 0; node < count; node++)
            {
                sb.Append($" {node}");
            }

            sb.AppendLine();

            for (int parentVertex = 0; parentVertex < count; parentVertex++)
            {
                sb.Append($"{parentVertex} | [ ");

                for (int childNode = 0; childNode < count; childNode++)
                {
                    if (parentVertex == childNode)
                    {
                        sb.Append("x ");
                    }
                    else if (adjacencyMatrix[parentVertex, childNode] == null)
                    {
                        sb.Append(". ");
                    } else
                    {
                        sb.Append("1 ");
                    }
                }

                sb.AppendLine("]");
            }

            Console.WriteLine(sb);
        }
    }
}

