using System;
using System.Collections.Generic;
using System.Text;

namespace MyGraph
{
    public class StandardGraph
    {
        public StandardGraph()
        {
            this.graphNodes = new List<GraphNode>();
            this.count = 0;
        }

        public IList<GraphNode> graphNodes;
        private int count;

        public bool AddNode(int value)
        {
            if (Find(value) != null)
            {
                return false;
            }

            this.graphNodes.Add(new GraphNode(value));
            this.count++;
            return true;
        }

        // Add a edge between the vertex, in other words, add a conections between them.
        public bool AddEdge(int n1, int n2)
        {            
            GraphNode gn1 = Find(n1); // Parent
            GraphNode gn2 = Find(n2); // Child

            // Checks if the graphs already exist.
            if (gn1 == null && gn2 == null)
            {
                return false;
            }
            // Checks if they are not already neighbors.
            else if (gn1.neighbors.Contains(gn2))
            {
                return false;
            } else
            {
                gn1.AddNeighbor(gn2);
                return true;
            }
        }

        GraphNode Find(int value)
        {
            foreach (var graph in graphNodes)
            {
                if (graph.value.Equals(value))
                {
                    return graph;
                }
            }

            return null;
        }

        public bool RemoveNode(int value)
        {
            GraphNode graphNode = Find(value);

            if (graphNode != null)
            {
                graphNodes.Remove(graphNode);

                foreach (var graph in graphNodes)
                {
                    RemoveEdge(graph.value, value);
                }

                return true;
            }

            return false;
        }

        public bool RemoveEdge(int n1, int n2)
        {
            GraphNode gn1 = Find(n1);
            GraphNode gn2 = Find(n2);

            if (gn1 == null || gn2 == null)
            {
                return false;
            } else if (!gn1.neighbors.Contains(gn2))
            {
                return false;
            } else
            {
                gn1.RemoveNeighbor(gn2);
                return true;
            }
        }

        public bool Clear()
        {            
            foreach (var graph in graphNodes)
            {
                graph.RemoveAllNeighbors();                
            }

            for (int i = count - 1; i >= 0; i--)
            {
                graphNodes.RemoveAt(i);
            }

            return true;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("=======================================");
            sb.AppendLine("New Graph Adjacency List Implementation");
            sb.AppendLine("---------[Non Zero Index Based]--------");
            sb.AppendLine("=======================================");

            for (int i = 0; i < count; i++)
            {
                sb.AppendLine(graphNodes[i].ToString());
            }

            Console.WriteLine(sb);
            return sb.ToString();
        }
    }
}
