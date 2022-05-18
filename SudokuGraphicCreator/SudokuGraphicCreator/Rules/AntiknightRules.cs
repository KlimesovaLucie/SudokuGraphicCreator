namespace SudokuGraphicCreator.Rules
{
    /// <summary>
    /// This class deside if number can be placed in given row and col by antiknight rules.
    /// Rule: in step of knight cannost be same numbers.
    /// </summary>
    public class AntiknightRules
    {
        /// <summary>
        /// Deside if <paramref name="number"/> can be placed in given <paramref name="row"/> and <paramref name="col"/> in <paramref name="grid"/> by antiknight rules.
        /// </summary>
        /// <param name="grid">Grid of sudoku.</param>
        /// <param name="row">Row in which is <paramref name="number"/> placing.</param>
        /// <param name="col">Col in which is <paramref name="number"/> placing.</param>
        /// <param name="number">Value which is placing in <paramref name="grid"/>.</param>
        /// <returns>true if <paramref name="number"/> can be placed in <paramref name="grid"/> by antiknight rules.</returns>
        public static bool IsAntiknightSafe(int[,] grid, int row, int col, int number)
        {
            for (int i = -2; i < 3; i++)
            {
                int colIndex = 2;
                if (i % 2 == 0)
                {
                    colIndex = 1;
                }
                if (SudokuRules.AreIndexesInBound(grid.GetLength(0), row + i, col - colIndex) && grid[row + i, col - colIndex] == number)
                {
                    return false;
                }
                else if (SudokuRules.AreIndexesInBound(grid.GetLength(0), row + i, col + colIndex) && grid[row + i, col + colIndex] == number)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
