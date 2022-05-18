using SudokuGraphicCreator.Model;

namespace SudokuGraphicCreator.Rules
{
    /// <summary>
    /// This class deside if number can be placed in given row and col by kropki rules.
    /// Rules: In all cases where two adjancent number are twice bigger is placed black circle,
    /// if their difference is one, there is white circle.
    /// </summary>
    public class KropkiRules
    {
        /// <summary>
        /// Deside if <paramref name="number"/> can be placed in given <paramref name="row"/> and <paramref name="col"/> in <paramref name="grid"/> by kroki rules.
        /// </summary>
        /// <param name="grid">Grid of sudoku.</param>
        /// <param name="row">Row in which is <paramref name="number"/> placing.</param>
        /// <param name="col">Col in which is <paramref name="number"/> placing.</param>
        /// <param name="number">Value which is placing in <paramref name="grid"/>.</param>
        /// <returns>true if <paramref name="number"/> can be placed in <paramref name="grid"/> by kropki rules.</returns>
        public static bool IsKropkiSafe(int[,] grid, int row, int col, int number)
        {
            return IsKropkiNeighbourSafe(grid, row - 1, col, row, col, number, ElementLocationType.Column) &&
                IsKropkiNeighbourSafe(grid, row + 1, col, row + 1, col, number, ElementLocationType.Column) &&
                IsKropkiNeighbourSafe(grid, row, col - 1, row, col, number, ElementLocationType.Row) &&
                IsKropkiNeighbourSafe(grid, row, col + 1, row, col + 1, number, ElementLocationType.Row);
        }

        private static bool IsKropkiNeighbourSafe(int[,] grid, int row, int col, int elemRow, int elemCol, int number, ElementLocationType location)
        {
            if (SudokuRules.AreIndexesInBound(grid.GetLength(0), row, col))
            {
                if (IsBlackKropkiElem(elemRow, elemCol, location))
                {
                    if (grid[row, col] != 0 && grid[row, col] * 2 != number && grid[row, col] != number * 2)
                    {
                        return false;
                    }
                }
                else if (IsWhiteKropkiElem(elemRow, elemCol, location))
                {
                    if (grid[row, col] != 0 && grid[row, col] + 1 != number && grid[row, col] - 1 != number)
                    {
                        return false;
                    }
                }
                else
                {
                    if (grid[row, col] != 0 && (grid[row, col] + 1 == number || grid[row, col] - 1 == number ||
                        grid[row, col] * 2 == number || grid[row, col] == number * 2))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static bool IsBlackKropkiElem(int row, int col, ElementLocationType locationType)
        {
            foreach (var element in Stores.SudokuStore.Instance.Sudoku.SudokuVariants)
            {
                BlackCircle circle = element as BlackCircle;
                if (circle != null && circle.RowIndex == row && circle.ColIndex == col && circle.Location == locationType)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool IsWhiteKropkiElem(int row, int col, ElementLocationType locationType)
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
