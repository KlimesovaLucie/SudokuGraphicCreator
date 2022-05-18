namespace SudokuGraphicCreator.Rules
{
    /// <summary>
    /// This class deside if number can be placed in given row and col by widoku rules.
    /// Rules: numbers cannot repeat in four extra boxes.
    /// </summary>
    public class WindokuRules
    {
        /// <summary>
        /// Deside if <paramref name="number"/> can be placed in given <paramref name="row"/> and <paramref name="col"/> in <paramref name="grid"/> by windoku rules.
        /// </summary>
        /// <param name="grid">Grid of sudoku.</param>
        /// <param name="row">Row in which is <paramref name="number"/> placing.</param>
        /// <param name="col">Col in which is <paramref name="number"/> placing.</param>
        /// <param name="number">Value which is placing in <paramref name="grid"/>.</param>
        /// <returns>true if <paramref name="number"/> can be placed in <paramref name="grid"/> by windoku rules.</returns>
        public static bool IsWindokuRules(int[,] grid, int row, int col, int number)
        {
            if (IsLeftUpWindokuRegion(row, col))
            {
                return IsWindokuRegionSafe(grid, 1, 1, number);
            }

            if (IsRightUpWindokuRegion(row, col))
            {
                return IsWindokuRegionSafe(grid, 1, 5, number);
            }

            if (IsLeftDownWindokuRegion(row, col))
            {
                return IsWindokuRegionSafe(grid, 5, 1, number);
            }

            if (IsRightDownWindokuRegion(row, col))
            {
                return IsWindokuRegionSafe(grid, 5, 5, number);
            }

            return true;
        }

        private static bool IsLeftUpWindokuRegion(int row, int col)
        {
            return 1 <= row && row <= 3 && 1 <= col && col <= 3;
        }

        private static bool IsRightUpWindokuRegion(int row, int col)
        {
            return 1 <= row && row <= 3 && 5 <= col && col <= 7;
        }

        private static bool IsLeftDownWindokuRegion(int row, int col)
        {
            return 5 <= row && row <= 7 && 1 <= col && col <= 3;
        }

        private static bool IsRightDownWindokuRegion(int row, int col)
        {
            return 5 <= row && row <= 7 && 5 <= col && col <= 7;
        }

        private static bool IsWindokuRegionSafe(int[,] grid, int startRow, int startCol, int number)
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (grid[startRow + row, startCol + col] == number)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
