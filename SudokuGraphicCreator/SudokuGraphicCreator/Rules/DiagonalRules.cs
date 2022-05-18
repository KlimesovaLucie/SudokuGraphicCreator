namespace SudokuGraphicCreator.Rules
{
    /// <summary>
    /// This class deside if number can be placed in given row and col by diagonal rules.
    /// Rules: on both diagonals must be different numbers.
    /// </summary>
    public class DiagonalRules
    {
        /// <summary>
        /// Deside if <paramref name="number"/> can be placed in given <paramref name="row"/> and <paramref name="col"/> in <paramref name="grid"/> by diagonal rules.
        /// </summary>
        /// <param name="grid">Grid of sudoku.</param>
        /// <param name="row">Row in which is <paramref name="number"/> placing.</param>
        /// <param name="col">Col in which is <paramref name="number"/> placing.</param>
        /// <param name="number">Value which is placing in <paramref name="grid"/>.</param>
        /// <returns>true if <paramref name="number"/> can be placed in <paramref name="grid"/> by diagonal rules.</returns>
        public static bool IsDiagonalSafe(int[,] grid, int row, int col, int number)
        {
            if (IsOnLeftDiagonal(row, col))
            {
                if (!IsLeftDiagonalSafe(grid, grid.GetLength(0), number))
                {
                    return false;
                }
            }

            if (IsOnRightDiagonal(row, col, grid.GetLength(0)))
            {
                return IsRightDiagonalSafe(grid, grid.GetLength(0), number);
            }

            return true;
        }

        private static bool IsOnLeftDiagonal(int row, int col)
        {
            return row == col;
        }

        private static bool IsOnRightDiagonal(int row, int col, int gridSize)
        {
            return (gridSize - 1 - row) == col;
        }

        private static bool IsLeftDiagonalSafe(int[,] grid, int gridSize, int number)
        {
            for (int i = 0; i < gridSize; i++)
            {
                if (grid[i, i] == number)
                {
                    return false;
                }
            }
            return true;
        }

        private static bool IsRightDiagonalSafe(int[,] grid, int gridSize, int number)
        {
            for (int i = 0; i < gridSize; i++)
            {
                if (grid[gridSize - 1 - i, i] == number)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
