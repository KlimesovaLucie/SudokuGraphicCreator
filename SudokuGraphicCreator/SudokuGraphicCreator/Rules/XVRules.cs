using SudokuGraphicCreator.Model;

namespace SudokuGraphicCreator.Rules
{
    /// <summary>
    /// This class deside if number can be placed in given row and col by XV rules.
    /// Rules: in all cases where sum of two adjacent numbers is ten / five is place X / V.
    /// </summary>
    public class XVRules
    {
        /// <summary>
        /// Deside if <paramref name="number"/> can be placed in given <paramref name="row"/> and <paramref name="col"/> in <paramref name="grid"/> by XV rules.
        /// </summary>
        /// <param name="grid">Grid of sudoku.</param>
        /// <param name="row">Row in which is <paramref name="number"/> placing.</param>
        /// <param name="col">Col in which is <paramref name="number"/> placing.</param>
        /// <param name="number">Value which is placing in <paramref name="grid"/>.</param>
        /// <returns>true if <paramref name="number"/> can be placed in <paramref name="grid"/> by XV rules.</returns>
        public static bool IsXVSafe(int[,] grid, int row, int col, int number)
        {
            return IsXVNeighbourSafe(grid, row - 1, col, row, col, number, ElementLocationType.Column) &&
                IsXVNeighbourSafe(grid, row + 1, col, row + 1, col, number, ElementLocationType.Column) &&
                IsXVNeighbourSafe(grid, row, col - 1, row, col, number, ElementLocationType.Row) &&
                IsXVNeighbourSafe(grid, row, col + 1, row, col + 1, number, ElementLocationType.Row);
        }

        private static bool IsXVNeighbourSafe(int[,] grid, int row, int col, int elemRow, int elemCol, int number, ElementLocationType location)
        {
            if (SudokuRules.AreIndexesInBound(grid.GetLength(0), row, col))
            {
                if (SudokuRules.IsCharacterElem(elemRow, elemCol, SudokuElementType.XvX, location))
                {
                    if (grid[row, col] != 0 && grid[row, col] + number != 10)
                    {
                        return false;
                    }
                }
                else if (SudokuRules.IsCharacterElem(elemRow, elemCol, SudokuElementType.XvV, location))
                {
                    if (grid[row, col] != 0 && grid[row, col] + number != 5)
                    {
                        return false;
                    }
                }
                else
                {
                    if (grid[row, col] != 0 && (grid[row, col] + number == 5 || grid[row, col] + number == 10))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
