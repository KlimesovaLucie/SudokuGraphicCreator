using SudokuGraphicCreator.Model;

namespace SudokuGraphicCreator.Rules
{
    /// <summary>
    /// This class deside if number can be placed in given row and col by search nine rules.
    /// Rules: number on arrow is equal to steps to nine in corresponding direction.
    /// </summary>
    public class SearchNineRules
    {
        /// <summary>
        /// Deside if <paramref name="number"/> can be placed in given <paramref name="row"/> and <paramref name="col"/> in <paramref name="grid"/> by search nine rules.
        /// </summary>
        /// <param name="grid">Grid of sudoku.</param>
        /// <param name="row">Row in which is <paramref name="number"/> placing.</param>
        /// <param name="col">Col in which is <paramref name="number"/> placing.</param>
        /// <param name="number">Value which is placing in <paramref name="grid"/>.</param>
        /// <returns>true if <paramref name="number"/> can be placed in <paramref name="grid"/> by search nine rules.</returns>
        public static bool IsSearchNineSafe(int[,] grid, int row, int col, int number)
        {
            grid[row, col] = number;
            bool result = ValidateAllArrows(grid);
            grid[row, col] = 0;
            if (number == 9)
            {
                if (!AreNinesValid(row, col))
                {
                    return false;
                }
            }
            return result;
        }

        private static bool AreNinesValid(int row, int col)
        {
            foreach (var elem in Stores.SudokuStore.Instance.Sudoku.SudokuVariants)
            {
                BoldArrow arrow = elem as BoldArrow;
                if (arrow != null)
                {
                    if ((arrow.SudokuElemType == SudokuElementType.SearchNineLeft && row == arrow.RowIndex && col > arrow.ColIndex) ||
                        (arrow.SudokuElemType == SudokuElementType.SearchNineRight && row == arrow.RowIndex && col < arrow.ColIndex) ||
                        (arrow.SudokuElemType == SudokuElementType.SearchNineUp && row > arrow.RowIndex && col == arrow.ColIndex) ||
                        (arrow.SudokuElemType == SudokuElementType.SearchNineDown && row < arrow.RowIndex && col == arrow.ColIndex))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static bool ValidateAllArrows(int[,] grid)
        {
            foreach (var elem in Stores.SudokuStore.Instance.Sudoku.SudokuVariants)
            {
                BoldArrow arrow = elem as BoldArrow;
                if (arrow != null)
                {
                    int numberOnArrow = grid[arrow.RowIndex, arrow.ColIndex];
                    if (numberOnArrow == 0)
                    {
                        continue;
                    }

                    if (arrow.SudokuElemType == SudokuElementType.SearchNineLeft)
                    {
                        int colIndex = arrow.ColIndex - numberOnArrow;
                        if (colIndex >= 0)
                        {
                            int numberInGrid = grid[arrow.RowIndex, colIndex];
                            if (numberInGrid == 0)
                            {

                            }
                            else if (numberInGrid != 9)
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (arrow.SudokuElemType == SudokuElementType.SearchNineRight)
                    {
                        int colIndex = arrow.ColIndex + numberOnArrow;
                        if (colIndex < Stores.SudokuStore.Instance.Sudoku.Grid.Size)
                        {
                            int numberInGrid = grid[arrow.RowIndex, colIndex];
                            if (numberInGrid == 0)
                            {

                            }
                            else if (numberInGrid != 9)
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (arrow.SudokuElemType == SudokuElementType.SearchNineUp)
                    {
                        int rowIndex = arrow.RowIndex - numberOnArrow;
                        if (rowIndex >= 0)
                        {
                            int numberInGrid = grid[rowIndex, arrow.ColIndex];
                            if (numberInGrid == 0)
                            {

                            }
                            else if (numberInGrid != 9)
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (arrow.SudokuElemType == SudokuElementType.SearchNineDown)
                    {
                        int rowIndex = arrow.RowIndex + numberOnArrow;
                        if (rowIndex < Stores.SudokuStore.Instance.Sudoku.Grid.Size)
                        {
                            int numberInGrid = grid[rowIndex, arrow.ColIndex];
                            if (numberInGrid == 0)
                            {

                            }
                            else if (numberInGrid != 9)
                            {
                                return false;
                            }
                        }
                        else
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
