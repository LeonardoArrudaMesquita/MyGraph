using System;
using System.Collections.Generic;
using System.Text;

namespace MyGraph
{
    public class GraphNode<T>   
    {
        public List<GraphNode<T>> neighbors;
        public T value;

        public GraphNode(T value)
        {
            this.value = value;
            this.neighbors = new List<GraphNode<T>>();
        }

        public bool AddNeighbor(GraphNode<T> neighbor)
        {
            if (neighbors.Contains(neighbor))
            {
                return false;
            }

            neighbors.Add(neighbor);
            return true;
        }

        public bool RemoveNeighbor(GraphNode<T> neighbor)
        {
            return neighbors.Remove(neighbor);
        }

        public bool RemoveAllNeighbors()
        {
            for (int i = neighbors.Count; i >= 0; i--)
            {
                neighbors.RemoveAt(i);
            }

            return true;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"[Node Value: {this.value} with Neighbors");

            foreach (GraphNode<T> neighbor in this.neighbors)
            {
                sb.Append($" -> {neighbor.value}");
            }
            sb.Append(" ]");
            sb.AppendLine();
            return sb.ToString();
        }
    }

    public class PathNodeInfo<T>
    {
        public GraphNode<T> previous;

        public PathNodeInfo(GraphNode<T> previous)
        {
            this.previous = previous;
        }
    }

    enum SearchType
    {
        DFS,
        BFS
    }

    public class Graph<T>
    {
        private List<GraphNode<T>> nodes;
        private int count;

        public Graph()
        {
            this.nodes = new List<GraphNode<T>>();
            this.count = 0;
        }

        public bool AddNode(T value)
        {
            if (Find(value) == null)
            {
                nodes.Add(new GraphNode<T>(value));
                this.count++;
                return true;
            }

            return false;
        }

        public bool AddEdge(T value1, T value2)
        {
            GraphNode<T> graphNode1 = Find(value1);
            GraphNode<T> graphNode2 = Find(value2);

            // Nodes need to exist to have an edge (Connection).
            if (graphNode1 == null || graphNode2 == null)
            {
                return false;
            }
            // Check if the nodes already have an edge between them.
            else if (graphNode1.neighbors.Contains(graphNode2))
            {
                return false;
            }
            // Add an edge between the graphs in order to make it bidirectional.
            else
            {
                graphNode1.AddNeighbor(graphNode2);
                graphNode2.AddNeighbor(graphNode1);
                return true;
            }
        }

        public GraphNode<T> Find(T value)
        {
            foreach (GraphNode<T> node in nodes)
            {
                if (node.value.Equals(value))
                {
                    return node;
                }
            }

            return null;
        }

        public bool RemoveNode(T value)
        {
            GraphNode<T> nodeToRemove = Find(value);

            if (nodeToRemove == null)
            {
                return false;
            }

            nodes.Remove(nodeToRemove);

            foreach (GraphNode<T> node in nodes)
            {
                node.RemoveNeighbor(nodeToRemove);
            }

            return true;
        }

        public void Clear()
        {
            foreach (GraphNode<T> node in nodes)
            {
                node.RemoveAllNeighbors();
            }

            for (int i = nodes.Count - 1; i >= 0; i--)
            {
                nodes.RemoveAt(i);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("=======================================");
            sb.AppendLine("Generic Bi-Directional Graph");
            sb.AppendLine("Adjacency List Implementation");
            sb.AppendLine("=======================================");

            for (int i = 0; i < count; i++)
            {
                sb.AppendLine(nodes[i].ToString());
            }

            Console.WriteLine(sb);
            return sb.ToString();
        }

        internal static string PrintGraph(Graph<int> graph)
        {
            return graph.ToString();
        }

        static string ConvertPathToString(GraphNode<int> endNode, Dictionary<GraphNode<int>, PathNodeInfo<int>> pathNodes)
        {
            LinkedList<GraphNode<int>> path = new LinkedList<GraphNode<int>>();
            path.AddFirst(endNode);
            GraphNode<int> previous = pathNodes[endNode].previous;

            while (previous != null)
            {
                path.AddFirst(previous);
                previous = pathNodes[previous].previous;
            }

            StringBuilder pathString = new StringBuilder();
            LinkedListNode<GraphNode<int>> currentNode = path.First;

            int nodeCount = 0;

            while (currentNode != null)
            {
                nodeCount++;
                pathString.Append(currentNode.Value.value.ToString());

                if (nodeCount < path.Count)
                {
                    pathString.Append(' ');
                }

                currentNode = currentNode.Next;
            }

            return pathString.ToString();
        }

        internal string Search(int start, int finish, Graph<int> graph, SearchType searchType)
        {
            LinkedList<GraphNode<int>> searchList = new LinkedList<GraphNode<int>>();
            GraphNode<int> startNode = graph.Find(start);
            GraphNode<int> finishNode = graph.Find(finish);

            if (start == finish)
            {
                return start.ToString();
            }
            else if (startNode == null || finishNode == null)
            {
                return "";
            }
            else
            {
                Dictionary<GraphNode<int>, PathNodeInfo<int>> pathNodes = new Dictionary<GraphNode<int>, PathNodeInfo<int>>();

                pathNodes.Add(startNode, new PathNodeInfo<int>(null));
                searchList.AddFirst(startNode);

                while (searchList.Count > 0)
                {
                    GraphNode<int> currentNode = searchList.First.Value;
                    searchList.RemoveFirst();

                    foreach (GraphNode<int> neighbor in currentNode.neighbors)
                    {
                        if (neighbor.value == finish)
                        {
                            pathNodes.Add(neighbor, new PathNodeInfo<int>(currentNode));
                            string path = "\nFinal Path is " + ConvertPathToString(neighbor, pathNodes);
                            Console.WriteLine(path);
                            return path;
                        }
                        else if (pathNodes.ContainsKey(neighbor))
                        {
                            // check for cycle, skip this neighbor.
                            continue;
                        } else
                        {
                            pathNodes.Add(neighbor, new PathNodeInfo<int>(currentNode));

                            if (searchType == SearchType.DFS)
                            {
                                searchList.AddFirst(neighbor);
                            } else
                            {
                                // BFS
                                searchList.AddLast(neighbor);
                            }

                            Console.WriteLine("Just Added " + neighbor.value + " to search list");
                        }
                    }
                }

                return "";
            }
        }
    }
}
