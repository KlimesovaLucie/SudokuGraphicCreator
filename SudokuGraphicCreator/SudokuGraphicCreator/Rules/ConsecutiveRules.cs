using SudokuGraphicCreator.Model;

namespace SudokuGraphicCreator.Rules
{
    /// <summary>
    /// This class deside if number can be placed in given row and col by consecutive rules.
    /// Rules: in all cases where two cell contains consecutive numbers must be placed white circle.
    /// </summary>
    public class ConsecutiveRules
    {
        /// <summary>
        /// Deside if <paramref name="number"/> can be placed in given <paramref name="row"/> and <paramref name="col"/> in <paramref name="grid"/> based on consecutive rules.
        /// </summary>
        /// <param name="grid">Grid of sudoku.</param>
        /// <param name="row">Row in which is <paramref name="number"/> placing.</param>
        /// <param name="col">Col in which is <paramref name="number"/> placing.</param>
        /// <param name="number">Value which is placing in <paramref name="grid"/>.</param>
        /// <returns>true if <paramref name="number"/> can be placed in <paramref name="grid"/> by consecutive rules.</returns>
        public static bool IsConsecutiveSafe(int[,] grid, int row, int col, int number)
        {
            return IsConsecutiveNeighbourSafe(grid, row - 1, col, row, col, number, ElementLocationType.Column) &&
                IsConsecutiveNeighbourSafe(grid, row + 1, col, row + 1, col, number, ElementLocationType.Column) &&
                IsConsecutiveNeighbourSafe(grid, row, col - 1, row, col, number, ElementLocationType.Row) &&
                IsConsecutiveNeighbourSafe(grid, row, col + 1, row, col + 1, number, ElementLocationType.Row);
        }

        private static bool IsConsecutiveNeighbourSafe(int[,] grid, int row, int col, int elemRow, int elemCol, int number, ElementLocationType location)
        {
            if (SudokuRules.AreIndexesInBound(grid.GetLength(0), row, col))
            {
                if (IsConsecutiveElem(elemRow, elemCol, location))
                {
                    if (grid[row, col] != 0 && grid[row, col] + 1 != number && grid[row, col] - 1 != number)
                    {
                        return false;
                    }
                }
                else
                {
                    if (grid[row, col] != 0 && (grid[row, col] + 1 == number || grid[row, col] - 1 == number))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static bool IsConsecutiveElem(int row, int col, ElementLocationType locationType)
        {
            foreach (var element in Stores.SudokuStore.Instance.Sudoku.SudokuVariants)
            {
                WhiteCircle circle = element as WhiteCircle;
                if (circle != null && circle.RowIndex == row && circle.ColIndex == col && circle.Location == locationType)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
