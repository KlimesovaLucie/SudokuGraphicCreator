using SudokuGraphicCreator.Model;

namespace SudokuGraphicCreator.Rules
{
    /// <summary>
    /// This class deside if number can be placed in given row and col by greater tha rules.
    /// Rules: all number must follow inequality signs.
    /// </summary>
    public class GreaterThanRules
    {
        /// <summary>
        /// Deside if <paramref name="number"/> can be placed in given <paramref name="row"/> and <paramref name="col"/> in <paramref name="grid"/> based on greater than rules.
        /// </summary>
        /// <param name="grid">Grid of sudoku.</param>
        /// <param name="row">Row in which is <paramref name="number"/> placing.</param>
        /// <param name="col">Col in which is <paramref name="number"/> placing.</param>
        /// <param name="number">Value which is placing in <paramref name="grid"/>.</param>
        /// <returns>true if <paramref name="number"/> can be placed in <paramref name="grid"/> by greater than rules.</returns>
        public static bool IsGreaterThanSafe(int[,] grid, int row, int col, int number)
        {
            return IsGreaterThanNeighbourColUpSafe(grid, row - 1, col, row, col, number) &&
                IsGreaterThanNeighbourColDownSafe(grid, row + 1, col, row + 1, col, number) &&
                IsGreaterThanNeighbourRowLeftSafe(grid, row, col - 1, row, col, number) &&
                IsGreaterThanNeighbourRowRightSafe(grid, row, col + 1, row, col + 1, number);
        }

        private static bool IsGreaterThanNeighbourRowLeftSafe(int[,] grid, int row, int col, int elemRow, int elemCol, int number)
        {
            if (SudokuRules.AreIndexesInBound(grid.GetLength(0), row, col))
            {
                if (SudokuRules.IsCharacterElem(elemRow, elemCol, SudokuElementType.GreaterThanLeft, ElementLocationType.Row))
                {
                    if (grid[row, col] != 0 && grid[row, col] > number)
                    {
                        return false;
                    }
                    if (number == 1)
                    {
                        return false;
                    }
                }
                else if (SudokuRules.IsCharacterElem(elemRow, elemCol, SudokuElementType.GreaterThanRight, ElementLocationType.Row))
                {
                    if (grid[row, col] != 0 && grid[row, col] < number)
                    {
                        return false;
                    }
                    if (number == 9)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static bool IsGreaterThanNeighbourRowRightSafe(int[,] grid, int row, int col, int elemRow, int elemCol, int number)
        {
            if (SudokuRules.AreIndexesInBound(grid.GetLength(0), row, col))
            {
                if (SudokuRules.IsCharacterElem(elemRow, elemCol, SudokuElementType.GreaterThanLeft, ElementLocationType.Row))
                {
                    if (grid[row, col] != 0 && grid[row, col] < number)
                    {
                        return false;
                    }
                    if (number == 9)
                    {
                        return false;
                    }
                }
                else if (SudokuRules.IsCharacterElem(elemRow, elemCol, SudokuElementType.GreaterThanRight, ElementLocationType.Row))
                {
                    if (grid[row, col] != 0 && grid[row, col] > number)
                    {
                        return false;
                    }
                    if (number == 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static bool IsGreaterThanNeighbourColUpSafe(int[,] grid, int row, int col, int elemRow, int elemCol, int number)
        {
            if (SudokuRules.AreIndexesInBound(grid.GetLength(0), row, col))
            {
                if (SudokuRules.IsCharacterElem(elemRow, elemCol, SudokuElementType.GreaterThanUp, ElementLocationType.Column))
                {
                    if (grid[row, col] != 0 && grid[row, col] > number)
                    {
                        return false;
                    }
                    if (number == 1)
                    {
                        return false;
                    }
                }
                else if (SudokuRules.IsCharacterElem(elemRow, elemCol, SudokuElementType.GreaterThanDown, ElementLocationType.Column))
                {
                    if (grid[row, col] != 0 && grid[row, col] < number)
                    {
                        return false;
                    }
                    if (number == 9)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static bool IsGreaterThanNeighbourColDownSafe(int[,] grid, int row, int col, int elemRow, int elemCol, int number)
        {
            if (SudokuRules.AreIndexesInBound(grid.GetLength(0), row, col))
            {
                if (SudokuRules.IsCharacterElem(elemRow, elemCol, SudokuElementType.GreaterThanUp, ElementLocationType.Column))
                {
                    if (grid[row, col] != 0 && grid[row, col] < number)
                    {
                        return false;
                    }
                    if (number == 9)
                    {
                        return false;
                    }
                }
                else if (SudokuRules.IsCharacterElem(elemRow, elemCol, SudokuElementType.GreaterThanDown, ElementLocationType.Column))
                {
                    if (grid[row, col] != 0 && grid[row, col] > number)
                    {
                        return false;
                    }
                    if (number == 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
