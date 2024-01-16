using System;
using System.Collections.Generic;

namespace AllPathBetweenPoints
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var s = new Solution();

            int[][] grid = new int[][]
            {
                new int[] { 0, 0, 0, 0 },
                new int[] { 1, 1, 0, 0 },
                new int[] { 0, 0, 0, 1 },
                new int[] { 0, 1, 0, 0 }
            };

            Console.WriteLine(s.CountPaths(grid));
        }
    }

    public class Solution
    {
        private HashSet<string> visited = new HashSet<string>();

        private int[][] neighbours = new int[][]
        {
            new int[] { 0, 1 }, //right
            new int[] { 0, 1 }, //left
            new int[] { 0, 1 }, //down
            new int[] { 0, 1 }, //up
        };

        public int CountPaths(int[][] grid) // O(4 ^ row*column)
        {
            return SearchDfs(grid, 0, 0);
        }

        private int SearchDfs(int[][] grid, int row, int column)
        {
            var numberOfRows = grid.Length;
            var numberOfColumns = grid[0].Length;

            if (row < 0 || row >= numberOfRows || column < 0 ||
             column >= numberOfColumns || visited.Contains(row + "-" + column)
             || grid[row][column] == 1)
            {
                return 0;
            }

            if (row == numberOfRows - 1 && column == numberOfColumns - 1)
            {
                return 1;
            }

            visited.Add(row + "-" + column);

            int path = 0;

            for (int i = 0; i < neighbours.Length; i++)
            {
                var childRow = neighbours[i][0];
                var childColumn = neighbours[i][1];

                path += SearchDfs(grid, row + childRow, column + childColumn);
            }

            visited.Remove(row + "-" + column);

            return path;
        }
    }

}
