using System;
using System.Collections.Generic;
using System.Text;

namespace MyGraph
{
    public class GraphNode
    {
        public int value;
        public List<GraphNode> neighbors;

        public GraphNode(int value)
        {
            this.value = value;
            this.neighbors = new List<GraphNode>();
        }

        public bool AddNeighbor(GraphNode neighbor)
        {
            if (neighbors.Contains(neighbor))
            {
                return false;
            }

            neighbors.Add(neighbor);
            return true;
        }

        public bool RemoveNeighbor(GraphNode neighbor)
        {
            if (neighbors.Contains(neighbor))
            {
                neighbors.Remove(neighbor);
                return true;
            }

            return false;
        }

        public bool RemoveAllNeighbors()
        {
            neighbors.Clear();
            return true;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"[Node Value: {this.value} with Neighbors");

            foreach (GraphNode neighbor in this.neighbors)
            {
                sb.Append($" -> {neighbor.value}");
            }
            sb.Append(" ]");
            sb.AppendLine();
            return sb.ToString();
        }
    }
}
