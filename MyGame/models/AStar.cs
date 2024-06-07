using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.models
{
    public class Node
    {
        public int x;
        public int y;
        public int totalCost;
        public int currentDistance;
        public int evristicCost;
        public Node parent;

        public Node(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.totalCost = 0;
            this.currentDistance = 0;
            this.evristicCost = 0;
            this.parent = null;
        }
    }

    public class AStar
    {
        public static List<Node> FindPath(char[][] grid, int startX, int startY, int targetX, int targetY)
        {
            List<Node> openList = new List<Node>();
            List<Node> closedList = new List<Node>();
            var forbiddenChars = new char[] { 'b', 't', 'z', 'w' };

            Node startNode = new Node(startX, startY);
            Node targetNode = new Node(targetX, targetY);

            openList.Add(startNode);

            while (openList.Count > 0)
            {
                Node currentNode = openList[0];
                int currentIndex = 0;

                for (int i = 0; i < openList.Count; i++)
                {
                    if (openList[i].totalCost < currentNode.totalCost)
                    {
                        currentNode = openList[i];
                        currentIndex = i;
                    }
                }

                openList.RemoveAt(currentIndex);
                closedList.Add(currentNode);

                if (currentNode.x == targetX && currentNode.y == targetY)
                {
                    List<Node> path = new List<Node>();
                    Node current = currentNode;
                    while (current != null)
                    {
                        path.Add(current);
                        current = current.parent;
                    }
                    path.Reverse();
                    return path;
                }

                List<Node> children = new List<Node>();

                int[] dx = { 1, 0, -1, 0 };
                int[] dy = { 0, 1, 0, -1 };

                for (int i = 0; i < 4; i++)
                {
                    int newX = currentNode.x + dx[i];
                    int newY = currentNode.y + dy[i];

                    if (newX >= 0 && newX < grid.Length && newY >= 0 && newY < grid[0].Length && !forbiddenChars.Contains(grid[newY][newX]))
                    {
                        Node newNode = new Node(newX, newY);
                        newNode.currentDistance = currentNode.currentDistance + 1;
                        newNode.evristicCost = Math.Abs(newX - targetX) + Math.Abs(newY - targetY);
                        newNode.totalCost = newNode.currentDistance + newNode.evristicCost;
                        newNode.parent = currentNode;

                        if (!closedList.Exists(node => node.x == newNode.x && node.y == newNode.y) &&
                            !openList.Exists(node => node.x == newNode.x && node.y == newNode.y))
                        {
                            openList.Add(newNode);
                        }
                    }
                }
            }

            return null;
        }
    }
}
