using SudokuGraphicCreator.Model;

namespace SudokuGraphicCreator.Rules
{
    /// <summary>
    /// This class deside if number can be placed in given row and col by star products rules.
    /// Rules: number around grid have to be product of all number in correspoding row / col which are on stars.
    /// </summary>
    public class StarProductRules
    {
        /// <summary>
        /// Deside if <paramref name="number"/> can be placed in given <paramref name="row"/> and <paramref name="col"/> in <paramref name="grid"/> by star products rules.
        /// </summary>
        /// <param name="grid">Grid of sudoku.</param>
        /// <param name="row">Row in which is <paramref name="number"/> placing.</param>
        /// <param name="col">Col in which is <paramref name="number"/> placing.</param>
        /// <param name="number">Value which is placing in <paramref name="grid"/>.</param>
        /// <returns>true if <paramref name="number"/> can be placed in <paramref name="grid"/> by star products rules.</returns>
        public static bool IsStarProductSafe(int[,] grid, int row, int col, int number)
        {
            if (!IsStarElem(row, col))
            {
                return true;
            }
            return IsStarInRowSafe(grid, row, number) && IsStarInColumnSafe(grid, col, number);
        }

        private static bool IsStarElem(int row, int col)
        {
            foreach (var element in Stores.SudokuStore.Instance.Sudoku.SudokuVariants)
            {
                Star circle = element as Star;
                if (circle != null && circle.RowIndex == row && circle.ColIndex == col && circle.SudokuElemType == SudokuElementType.StarProduct)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool IsStarInRowSafe(int[,] grid, int row, int number)
        {
            int productResult = FindStarProductInRow(row);
            if (productResult == 0)
            {
                return true;
            }

            int product;
            if (!IsLastStarAddingInRow(grid, row, number, out product))
            {
                return true;
            }
            return product == productResult;
        }

        private static bool IsLastStarAddingInRow(int[,] grid, int row, int number, out int product)
        {
            product = number;
            int withoutNumber = 0;
            foreach (var element in Stores.SudokuStore.Instance.Sudoku.SudokuVariants)
            {
                Star circle = element as Star;
                if (circle != null && circle.RowIndex == row && circle.SudokuElemType == SudokuElementType.StarProduct)
                {
                    int actualNumber = grid[row, circle.ColIndex];
                    if (actualNumber != 0)
                    {
                        product *= actualNumber;
                    }
                    else
                    {
                        withoutNumber++;
                    }
                }
            }
            return withoutNumber == 1;
        }

        private static int FindStarProductInRow(int row)
        {
            int product = Stores.SudokuStore.Instance.Sudoku.LeftNumbers[row, 2];
            if (product != 0)
            {
                return product;
            }
            return Stores.SudokuStore.Instance.Sudoku.RightNumbers[row, 2];
        }

        private static bool IsStarInColumnSafe(int[,] grid, int col, int number)
        {
            int productResult = FindStarProductInCol(col);
            if (productResult == 0)
            {
                return true;
            }

            int product;
            if (!IsLastStarAddingInCol(grid, col, number, out product))
            {
                return true;
            }
            return product == productResult;
        }

        private static bool IsLastStarAddingInCol(int[,] grid, int col, int number, out int product)
        {
            product = number;
            int withoutNumber = 0;
            foreach (var element in Stores.SudokuStore.Instance.Sudoku.SudokuVariants)
            {
                Star circle = element as Star;
                if (circle != null && circle.ColIndex == col && circle.SudokuElemType == SudokuElementType.StarProduct)
                {
                    int actualNumber = grid[circle.RowIndex, col];
                    if (actualNumber != 0)
                    {
                        product *= actualNumber;
                    }
                    else
                    {
                        withoutNumber++;
                    }
                }
            }
            return withoutNumber == 1;
        }

        private static int FindStarProductInCol(int col)
        {
            int product = Stores.SudokuStore.Instance.Sudoku.UpNumbers[2, col];
            if (product != 0)
            {
                return product;
            }
            return Stores.SudokuStore.Instance.Sudoku.BottomNumbers[0, col];
        }
    }
}
