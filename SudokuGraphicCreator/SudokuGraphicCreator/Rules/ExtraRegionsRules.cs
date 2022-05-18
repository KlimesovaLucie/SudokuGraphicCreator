using System;

namespace SudokuGraphicCreator.Rules
{
    /// <summary>
    /// This class deside if number can be placed in given row and col by extra regions rules.
    /// Rules: in extra regions have to be diferent digits.
    /// </summary>
    public class ExtraRegionsRules
    {
        /// <summary>
        /// Deside if <paramref name="number"/> can be placed in given <paramref name="row"/> and <paramref name="col"/> in <paramref name="grid"/> by extra regions rules.
        /// </summary>
        /// <param name="grid">Grid of sudoku.</param>
        /// <param name="row">Row in which is <paramref name="number"/> placing.</param>
        /// <param name="col">Col in which is <paramref name="number"/> placing.</param>
        /// <param name="number">Value which is placing in <paramref name="grid"/>.</param>
        /// <returns>true if <paramref name="number"/> can be placed in <paramref name="grid"/> by extra regions rules.</returns>
        public static bool IsExtraRegionsSafe(int[,] grid, int row, int col, int number)
        {
            Tuple<int, int> cell = new Tuple<int, int>(row, col);
            foreach (var box in Stores.SudokuStore.Instance.Sudoku.Grid.ExtraRegions)
            {
                if (box.Contains(cell))
                {
                    foreach (var actualCell in box)
                    {
                        int actualNumber = grid[actualCell.Item1, actualCell.Item2];
                        if (actualNumber == number)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}
