using SudokuGraphicCreator.Model;

namespace SudokuGraphicCreator.Rules
{
    /// <summary>
    /// This class deside if number can be placed in given row and col by sum rules.
    /// Rules: sum of two adjacent number must be same as number in circle between them.
    /// </summary>
    public class SumRules
    {
        /// <summary>
        /// Deside if <paramref name="number"/> can be placed in given <paramref name="row"/> and <paramref name="col"/> in <paramref name="grid"/> by sum rules.
        /// </summary>
        /// <param name="grid">Grid of sudoku.</param>
        /// <param name="row">Row in which is <paramref name="number"/> placing.</param>
        /// <param name="col">Col in which is <paramref name="number"/> placing.</param>
        /// <param name="number">Value which is placing in <paramref name="grid"/>.</param>
        /// <returns>true if <paramref name="number"/> can be placed in <paramref name="grid"/> by sum rules.</returns>
        public static bool IsSumSafe(int[,] grid, int row, int col, int number)
        {
            return IsSumNeighbourSafe(grid, row - 1, col, row, col, number, ElementLocationType.Column) &&
                IsSumNeighbourSafe(grid, row + 1, col, row + 1, col, number, ElementLocationType.Column) &&
                IsSumNeighbourSafe(grid, row, col - 1, row, col, number, ElementLocationType.Row) &&
                IsSumNeighbourSafe(grid, row, col + 1, row, col + 1, number, ElementLocationType.Row);
        }

        private static bool IsSumNeighbourSafe(int[,] grid, int row, int col, int elemRow, int elemCol, int number, ElementLocationType location)
        {
            if (SudokuRules.AreIndexesInBound(grid.GetLength(0), row, col))
            {
                int circleValue;
                if (SudokuRules.IsCircleWithNumberElem(elemRow, elemCol, SudokuElementType.Sum, location, out circleValue))
                {
                    if (grid[row, col] != 0 && grid[row, col] + number != circleValue)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
