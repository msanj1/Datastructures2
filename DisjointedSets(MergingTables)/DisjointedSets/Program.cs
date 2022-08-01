using System;
using System.Collections.Generic;
using System.Linq;

namespace DisjointedSets
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var input = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            var numberOfRowsInTables = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            var numberOfOperations = new List<int[]>();

            var numberOfTables = input[0];
            var numberOfQueries = input[1];

            for (int i = 0; i < numberOfQueries; i++)
            {
                var mergeOperation = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                numberOfOperations.Add(mergeOperation);
            }

            var tables = new List<Table>();
            var maximumNumberOfRows = 0;
            foreach (var numberOfRows in numberOfRowsInTables)
            {
                maximumNumberOfRows = Math.Max(maximumNumberOfRows, numberOfRows);
                tables.Add(new Table(numberOfRows));
            }

            for (var i = 0; i < numberOfQueries; i++)
            {
                var mergeOperation = numberOfOperations[i];
                
                var destinationTableIndex = mergeOperation[0];
                var sourceTableIndex = mergeOperation[1];

                var destinationTable = tables[destinationTableIndex - 1];
                var sourceTable = tables[sourceTableIndex - 1];

                var destinationTableParent = destinationTable.GetParent();
                var sourceTableParent = sourceTable.GetParent();

                if (destinationTableParent == sourceTableParent)
                {
                    Console.WriteLine(maximumNumberOfRows);
                    continue;
                };

                if (sourceTableParent.Rank <= destinationTableParent.Rank)
                {
                    sourceTableParent.SetParent(destinationTableParent);
                    destinationTableParent.NumberOfRows += sourceTableParent.NumberOfRows;
                    sourceTableParent.NumberOfRows = 0;
                    if (sourceTableParent.Rank == destinationTableParent.Rank) destinationTableParent.Rank++;
                    maximumNumberOfRows = Math.Max(maximumNumberOfRows, destinationTableParent.NumberOfRows);
                }
                else
                {
                    destinationTableParent.SetParent(sourceTableParent);
                    sourceTableParent.NumberOfRows += destinationTableParent.NumberOfRows;
                    destinationTableParent.NumberOfRows = 0;
                    maximumNumberOfRows = Math.Max(maximumNumberOfRows, sourceTableParent.NumberOfRows);
                }

                Console.WriteLine(maximumNumberOfRows);
            }
        }
    }
}