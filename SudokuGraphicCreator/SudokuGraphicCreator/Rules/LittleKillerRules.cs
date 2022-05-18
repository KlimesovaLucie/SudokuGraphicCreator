using SudokuGraphicCreator.Model;

namespace SudokuGraphicCreator.Rules
{
    /// <summary>
    /// This class deside if number can be placed in given row and col by little killer rules.
    /// Rules: number around grid is sum of numbers on diagonal in corresponding direction.
    /// </summary>
    public class LittleKillerRules
    {
        /// <summary>
        /// Deside if <paramref name="number"/> can be placed in given <paramref name="row"/> and <paramref name="col"/> in <paramref name="grid"/> by little killer rules.
        /// </summary>
        /// <param name="grid">Grid of sudoku.</param>
        /// <param name="row">Row in which is <paramref name="number"/> placing.</param>
        /// <param name="col">Col in which is <paramref name="number"/> placing.</param>
        /// <param name="number">Value which is placing in <paramref name="grid"/>.</param>
        /// <returns>true if <paramref name="number"/> can be placed in <paramref name="grid"/> by little killer rules.</returns>
        public static bool IsLittleKillerSafe(int[,] grid, int row, int col, int number)
        {
            if (!DiagonalRules.IsDiagonalSafe(grid, row, col, number))
            {
                return false;
            }
            grid[row, col] = number;
            bool result = IsLittleKillerLeftSafe(grid) && IsLittleKillerUpSafe(grid) &&
                IsLittleKillerRightSafe(grid) && IsLittleKillerDownSafe(grid);
            grid[row, col] = 0;
            return result;
        }

        private static bool IsLittleKillerLeftSafe(int[,] grid)
        {
            for (int rowAround = 0; rowAround < Stores.SudokuStore.Instance.Sudoku.Grid.Size - 1; rowAround++)
            {
                int sum = Stores.SudokuStore.Instance.Sudoku.LeftNumbers[rowAround, 2];
                if (sum != 0)
                {
                    SmallArrow arrow = FindSmallArrow(rowAround * 3, ElementLocationType.GridLeft);
                    if (arrow == null)
                    {
                        return false;
                    }
                    if (arrow.GraphicType == GraphicElementType.RightDown)
                    {
                        if (!IsLeftDownDiagonalSafe(grid, rowAround + 1, 0, sum))
                        {
                            return false;
                        }
                    }
                    else if (arrow.GraphicType == GraphicElementType.RightUp)
                    {
                        if (!IsLeftUpDiagonalSafe(grid, rowAround - 1, 0, sum))
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
            return true;
        }

        private static bool IsLeftDownDiagonalSafe(int[,] grid, int row, int col, int totalSum)
        {
            int gridSize = Stores.SudokuStore.Instance.Sudoku.Grid.Size;
            int i = 0;
            int rowIndex = row + i;
            int colIndex = col + i;
            int actualSum = 0;
            while (rowIndex != gridSize && colIndex != gridSize)
            {
                int actualNumber = grid[rowIndex, colIndex];
                if (actualNumber == 0)
                {
                    return true;
                }
                actualSum += actualNumber;

                i++;
                rowIndex = row + i;
                colIndex = col + i;
            }
            return actualSum == totalSum;
        }

        private static bool IsLeftUpDiagonalSafe(int[,] grid, int row, int col, int totalSum)
        {
            int gridSize = Stores.SudokuStore.Instance.Sudoku.Grid.Size;
            int i = 0;
            int rowIndex = row - i;
            int colIndex = col + i;
            int actualSum = 0;
            while (rowIndex != -1 && colIndex != gridSize)
            {
                int actualNumber = grid[rowIndex, colIndex];
                if (actualNumber == 0)
                {
                    return true;
                }
                actualSum += actualNumber;

                i++;
                rowIndex = row - i;
                colIndex = col + i;
            }
            return actualSum == totalSum;
        }

        private static bool IsLittleKillerUpSafe(int[,] grid)
        {
            for (int colAround = 0; colAround < Stores.SudokuStore.Instance.Sudoku.Grid.Size - 1; colAround++)
            {
                int sum = Stores.SudokuStore.Instance.Sudoku.UpNumbers[2, colAround];
                if (sum != 0)
                {
                    SmallArrow arrow = FindSmallArrow(colAround, ElementLocationType.GridUp);
                    if (arrow == null)
                    {
                        return false;
                    }
                    if (arrow.GraphicType == GraphicElementType.LeftDown)
                    {
                        if (!IsUpLeftDiagonalSafe(grid, 0, colAround - 1, sum))
                        {
                            return false;
                        }
                    }
                    else if (arrow.GraphicType == GraphicElementType.RightDown)
                    {
                        if (!IsUpRightDiagonalSafe(grid, 0, colAround + 1, sum))
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
            return true;
        }

        private static bool IsUpLeftDiagonalSafe(int[,] grid, int row, int col, int totalSum)
        {
            int gridSize = Stores.SudokuStore.Instance.Sudoku.Grid.Size;
            int i = 0;
            int rowIndex = row + i;
            int colIndex = col - i;
            int actualSum = 0;
            while (rowIndex != gridSize && colIndex != -1)
            {
                int actualNumber = grid[rowIndex, colIndex];
                if (actualNumber == 0)
                {
                    return true;
                }
                actualSum += actualNumber;

                i++;
                rowIndex = row + i;
                colIndex = col - i;
            }
            return actualSum == totalSum;
        }

        private static bool IsUpRightDiagonalSafe(int[,] grid, int row, int col, int totalSum)
        {
            int gridSize = Stores.SudokuStore.Instance.Sudoku.Grid.Size;
            int i = 0;
            int rowIndex = row + i;
            int colIndex = col + i;
            int actualSum = 0;
            while (rowIndex != gridSize && colIndex != gridSize)
            {
                int actualNumber = grid[rowIndex, colIndex];
                if (actualNumber == 0)
                {
                    return true;
                }
                actualSum += actualNumber;

                i++;
                rowIndex = row + i;
                colIndex = col + i;
            }
            return actualSum == totalSum;
        }

        private static bool IsLittleKillerRightSafe(int[,] grid)
        {
            for (int rowAround = 0; rowAround < Stores.SudokuStore.Instance.Sudoku.Grid.Size; rowAround++)
            {
                int sum = Stores.SudokuStore.Instance.Sudoku.RightNumbers[rowAround, 0];
                if (sum != 0)
                {
                    SmallArrow arrow = FindSmallArrow(rowAround * 3, ElementLocationType.GridRight);
                    if (arrow == null)
                    {
                        return false;
                    }
                    if (arrow.GraphicType == GraphicElementType.LeftDown)
                    {
                        if (!IsRightDownDiagonalSafe(grid, rowAround + 1, Stores.SudokuStore.Instance.Sudoku.Grid.Size - 1, sum))
                        {
                            return false;
                        }
                    }
                    else if (arrow.GraphicType == GraphicElementType.LeftUp)
                    {
                        if (!IsRightUpDiagonalSafe(grid, rowAround - 1, Stores.SudokuStore.Instance.Sudoku.Grid.Size - 1, sum))
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
            return true;
        }

        private static bool IsRightDownDiagonalSafe(int[,] grid, int row, int col, int totalSum)
        {
            int gridSize = Stores.SudokuStore.Instance.Sudoku.Grid.Size;
            int i = 0;
            int rowIndex = row + i;
            int colIndex = col - i;
            int actualSum = 0;
            while (rowIndex != gridSize && colIndex != -1)
            {
                int actualNumber = grid[rowIndex, colIndex];
                if (actualNumber == 0)
                {
                    return true;
                }
                actualSum += actualNumber;

                i++;
                rowIndex = row + i;
                colIndex = col - i;
            }
            return actualSum == totalSum;
        }

        private static bool IsRightUpDiagonalSafe(int[,] grid, int row, int col, int totalSum)
        {
            int gridSize = Stores.SudokuStore.Instance.Sudoku.Grid.Size;
            int i = 0;
            int rowIndex = row - i;
            int colIndex = col - i;
            int actualSum = 0;
            while (rowIndex != -1 && colIndex != -1)
            {
                int actualNumber = grid[rowIndex, colIndex];
                if (actualNumber == 0)
                {
                    return true;
                }
                actualSum += actualNumber;

                i++;
                rowIndex = row - i;
                colIndex = col - i;
            }
            return actualSum == totalSum;
        }

        private static bool IsLittleKillerDownSafe(int[,] grid)
        {
            for (int colAround = 0; colAround < Stores.SudokuStore.Instance.Sudoku.Grid.Size; colAround++)
            {
                int sum = Stores.SudokuStore.Instance.Sudoku.BottomNumbers[0, colAround];
                if (sum != 0)
                {
                    SmallArrow arrow = FindSmallArrow(colAround, ElementLocationType.GridDown);
                    if (arrow == null)
                    {
                        return false;
                    }
                    if (arrow.GraphicType == GraphicElementType.RightUp)
                    {
                        if (!IsDownRightDiagonalSafe(grid, Stores.SudokuStore.Instance.Sudoku.Grid.Size - 1, colAround + 1, sum))
                        {
                            return false;
                        }
                    }
                    else if (arrow.GraphicType == GraphicElementType.LeftUp)
                    {
                        if (!IsDownLeftDiagonalSafe(grid, Stores.SudokuStore.Instance.Sudoku.Grid.Size - 1, colAround - 1, sum))
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
            return true;
        }

        private static bool IsDownLeftDiagonalSafe(int[,] grid, int row, int col, int totalSum)
        {
            int gridSize = Stores.SudokuStore.Instance.Sudoku.Grid.Size;
            int i = 0;
            int rowIndex = row - i;
            int colIndex = col - i;
            int actualSum = 0;
            while (rowIndex != -1 && colIndex != -1)
            {
                int actualNumber = grid[rowIndex, colIndex];
                if (actualNumber == 0)
                {
                    return true;
                }
                actualSum += actualNumber;

                i++;
                rowIndex = row - i;
                colIndex = col - i;
            }
            return actualSum == totalSum;
        }

        private static bool IsDownRightDiagonalSafe(int[,] grid, int row, int col, int totalSum)
        {
            int gridSize = Stores.SudokuStore.Instance.Sudoku.Grid.Size;
            int i = 0;
            int rowIndex = row - i;
            int colIndex = col + i;
            int actualSum = 0;
            while (rowIndex != -1 && colIndex != gridSize)
            {
                int actualNumber = grid[rowIndex, colIndex];
                if (actualNumber == 0)
                {
                    return true;
                }
                actualSum += actualNumber;

                i++;
                rowIndex = row - i;
                colIndex = col + i;
            }
            return actualSum == totalSum;
        }

        private static SmallArrow FindSmallArrow(int order, ElementLocationType location)
        {
            foreach (var elem in Stores.SudokuStore.Instance.Sudoku.SudokuVariants)
            {
                SmallArrow arrow = elem as SmallArrow;
                if (arrow != null && arrow.Order == order && arrow.Location == location)
                {
                    return arrow;
                }
            }
            return null;
        }
    }
}
