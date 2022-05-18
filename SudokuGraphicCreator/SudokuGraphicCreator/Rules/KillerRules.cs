using SudokuGraphicCreator.Model;
using System;
using System.Collections.Generic;

namespace SudokuGraphicCreator.Rules
{
    /// <summary>
    /// This class deside if number can be placed in given row and col by killer rules.
    /// Rules: sum of numbers in cage have to be equal to number in cage.
    /// </summary>
    public class KillerRules
    {
        /// <summary>
        /// Deside if <paramref name="number"/> can be placed in given <paramref name="row"/> and <paramref name="col"/> in <paramref name="grid"/> by killer rules.
        /// </summary>
        /// <param name="grid">Grid of sudoku.</param>
        /// <param name="row">Row in which is <paramref name="number"/> placing.</param>
        /// <param name="col">Col in which is <paramref name="number"/> placing.</param>
        /// <param name="number">Value which is placing in <paramref name="grid"/>.</param>
        /// <returns>true if <paramref name="number"/> can be placed in <paramref name="grid"/> by killer rules.</returns>
        public static bool IsKillerSafe(int[,] grid, int row, int col, int number)
        {
            List<Cage> cages = FindKillerCages(new Tuple<int, int>(row, col));
            if (cages.Count == 0)
            {
                return true;
            }

            return AreAllCagesSafe(grid, cages, number);
        }

        private static List<Cage> FindKillerCages(Tuple<int, int> cell)
        {
            List<Cage> result = new List<Cage>();

            foreach (var elem in Stores.SudokuStore.Instance.Sudoku.SudokuVariants)
            {
                Cage cage = elem as Cage;
                if (cage != null && cage.Positions.Contains(cell))
                {
                    result.Add(cage);
                }
            }

            return result;
        }

        private static bool AreAllCagesSafe(int[,] grid, List<Cage> cages, int number)
        {
            foreach (var cage in cages)
            {
                if (!IsOneCageSafe(grid, cage, number))
                {
                    return false;
                }
            }
            return true;
        }

        private static bool IsOneCageSafe(int[,] grid, Cage cage, int number)
        {
            List<int> numbers = new List<int>();
            int missing = 0;
            foreach (var cell in cage.Positions)
            {
                int actualNumber = grid[cell.Item1, cell.Item2];
                if (actualNumber == 0)
                {
                    missing++;
                    actualNumber = number;
                }
                numbers.Add(actualNumber);
            }
            if (missing != 1)
            {
                return true;
            }

            int totalSum = 0;
            foreach (var value in numbers)
            {
                totalSum += value;
            }
            return totalSum == cage.Number;
        }
    }
}
