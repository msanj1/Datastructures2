using System;
using System.Collections.Generic;

namespace ShortestPath
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var grid = new int[][]
            {
                new int[] { 0, 0, 0, 0 },
                new int[] { 1, 1, 0, 0 },
                new int[] { 0, 0, 0, 1 },
                new int[] { 0, 1, 0, 0 }
            };

            var solution = new Solution();

            Console.WriteLine(solution.Search(grid));
        }
    }

    public class Solution
    {
        private int[][] neighbours = new int[][]
            {
            new int[] { 0, 1 }, //right
            new int[] { 0, -1 }, //left
            new int[] { 1, 0 }, //down
            new int[] { -1, 0 }, //up
            };
        private HashSet<string> visited = new HashSet<string>();
        private Queue<int[]> queue = new Queue<int[]>();

        public int ShortestPath(int[][] grid)
        {
            return Search(grid);
        }

        public int Search(int[][] grid)
        {
            var numberOfRows = grid.Length;
            var numberOfColumns = grid[0].Length;

            queue.Enqueue(new int[] { 0, 0 });
            int length = 0;

            while (queue.Count > 0)
            {
                int queueSize = queue.Count;
                for (int i = 0; i < queueSize; i++)
                {
                    var position = queue.Dequeue();

                    var row = position[0];
                    var column = position[1];
                    //if position is out of bounds
                    if (row < 0 || row >= numberOfRows || column < 0 ||
                    column >= numberOfColumns || visited.Contains(row + "-" + column)
                    || grid[row][column] == 1)
                    {
                        continue;
                    }

                    if (row == numberOfRows - 1 && column == numberOfColumns - 1)
                    {
                        return length;
                    }
                    
                    visited.Add(row + "-" + column);
                    //go through the neighbours
                    foreach (var neighbour in neighbours)
                    {
                        var newRow = row + neighbour[0];
                        var newColumn = column + neighbour[1];

                        queue.Enqueue(new int[] { newRow, newColumn });
                    }
                }

                length++;
            }

            return -1;
        }
    }
}
