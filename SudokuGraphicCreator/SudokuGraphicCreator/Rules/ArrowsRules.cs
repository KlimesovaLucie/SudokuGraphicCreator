using SudokuGraphicCreator.Model;
using System;
using System.Collections.Generic;

namespace SudokuGraphicCreator.Rules
{
    /// <summary>
    /// This class deside if number can be placed in given row and col by arrows rules.
    /// Rules: number in circle must be same as sum of all number on arrow.
    /// </summary>
    public class ArrowsRules
    {
        /// <summary>
        /// Deside if <paramref name="number"/> can be placed in given <paramref name="row"/> and <paramref name="col"/> in <paramref name="grid"/> by arrows rules.
        /// </summary>
        /// <param name="grid">Grid of sudoku.</param>
        /// <param name="row">Row in which is <paramref name="number"/> placing.</param>
        /// <param name="col">Col in which is <paramref name="number"/> placing.</param>
        /// <param name="number">Value which is placing in <paramref name="grid"/>.</param>
        /// <returns>true if <paramref name="number"/> can be placed in <paramref name="grid"/> by arrows rules.</returns>
        public static bool IsArrowsSafe(int[,] grid, int row, int col, int number)
        {
            List<LongArrowWithCircle> arrows = FindLongArrowWithCircle(new Tuple<int, int>(row, col));
            if (arrows.Count == 0)
            {
                return true;
            }

            return AreArrowsSafe(grid, arrows, number);
        }

        private static List<LongArrowWithCircle> FindLongArrowWithCircle(Tuple<int, int> cell)
        {
            List<LongArrowWithCircle> result = new List<LongArrowWithCircle>();

            foreach (var elem in Stores.SudokuStore.Instance.Sudoku.SudokuVariants)
            {
                LongArrowWithCircle arrow = elem as LongArrowWithCircle;
                if (arrow != null && arrow.Arrow.Positions.Contains(cell))
                {
                    result.Add(arrow);
                }
            }

            return result;
        }

        private static bool AreArrowsSafe(int[,] grid, List<LongArrowWithCircle> arrows, int number)
        {
            foreach (var arrow in arrows)
            {
                if (!IsActualArrowSave(grid, arrow, number))
                {
                    return false;
                }
            }
            return true;
        }

        private static bool IsActualArrowSave(int[,] grid, LongArrowWithCircle arrow, int number)
        {
            List<int> numbers = new List<int>();
            int missing = 0;
            foreach (var cell in arrow.Arrow.Positions)
            {
                if (grid[cell.Item1, cell.Item2] == 0)
                {
                    missing++;
                    numbers.Add(number);
                }
                else
                {
                    numbers.Add(grid[cell.Item1, cell.Item2]);
                }
            }
            if (missing != 1)
            {
                return true;
            }

            int totalSum = numbers[0];
            int actualSum = 0;
            for (int i = 1; i < numbers.Count; i++)
            {
                actualSum += numbers[i];
            }
            return totalSum == actualSum;
        }
    }
}
