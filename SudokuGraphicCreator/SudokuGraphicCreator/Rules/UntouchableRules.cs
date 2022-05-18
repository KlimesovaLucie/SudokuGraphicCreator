namespace SudokuGraphicCreator.Rules
{
    /// <summary>
    /// This class deside if number can be placed in given row and col by untouchable rules.
    /// Rules: two same number cannot share a corner.
    /// </summary>
    public class UntouchableRules
    {
        /// <summary>
        /// Deside if <paramref name="number"/> can be placed in given <paramref name="row"/> and <paramref name="col"/> in <paramref name="grid"/> by untouchable rules.
        /// </summary>
        /// <param name="grid">Grid of sudoku.</param>
        /// <param name="row">Row in which is <paramref name="number"/> placing.</param>
        /// <param name="col">Col in which is <paramref name="number"/> placing.</param>
        /// <param name="number">Value which is placing in <paramref name="grid"/>.</param>
        /// <returns>true if <paramref name="number"/> can be placed in <paramref name="grid"/> by untouchable rules.</returns>
        public static bool IsUntouchableSafe(int[,] grid, int row, int col, int number)
        {
            return !IsSameNumber(grid, row - 1, col + 1, number) &&
                !IsSameNumber(grid, row - 1, col - 1, number) &&
                !IsSameNumber(grid, row + 1, col - 1, number) &&
                !IsSameNumber(grid, row + 1, col + 1, number);
        }

        private static bool IsSameNumber(int[,] grid, int row, int col, int number)
        {
            return SudokuRules.AreIndexesInBound(grid.GetLength(0), row, col) && grid[row, col] == number;
        }
    }
}
