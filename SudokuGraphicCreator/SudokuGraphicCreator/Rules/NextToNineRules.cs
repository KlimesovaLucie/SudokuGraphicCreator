using SudokuGraphicCreator.Model;
using System.Collections.Generic;

namespace SudokuGraphicCreator.Rules
{
    /// <summary>
    /// This class deside if number can be placed in given row and col by next to nine rules.
    /// Rules: number around the grid lies next to nine in corresponding row / column.
    /// </summary>
    public class NextToNineRules
    {
        /// <summary>
        /// Deside if <paramref name="number"/> can be placed in given <paramref name="row"/> and <paramref name="col"/> in <paramref name="grid"/> by next to nine rules.
        /// </summary>
        /// <param name="grid">Grid of sudoku.</param>
        /// <param name="row">Row in which is <paramref name="number"/> placing.</param>
        /// <param name="col">Col in which is <paramref name="number"/> placing.</param>
        /// <param name="number">Value which is placing in <paramref name="grid"/>.</param>
        /// <returns>true if <paramref name="number"/> can be placed in <paramref name="grid"/> by next to nine rules.</returns>
        public static bool IsNextToNineSafe(int[,] grid, int row, int col, int number)
        {
            if (number == 9)
            {
                return IsNextToNineNinePlacedCorrect(grid, row, col);
            }
            return NextToNineNumberAround(grid, row, col, number);
        }

        private static bool NextToNineNumberAround(int[,] grid, int row, int col, int number)
        {
            List<int> aroundRow = GetNumberAroundNextToNineInRow(row);
            List<int> aroundCol = GetNumberAroundNextToNineInCol(col);

            if (!aroundRow.TrueForAll(n => n == 0))
            {
                if (aroundRow.Contains(number))
                {
                    if (!CanBePlacedInRowNextToNine(aroundRow, grid, row, col, number))
                    {
                        return false;
                    }
                }
                else
                {
                    int nineIndexCol = -1;
                    for (int actualCol = 0; actualCol < Stores.SudokuStore.Instance.Sudoku.Grid.Size; actualCol++)
                    {
                        if (grid[row, actualCol] == grid.GetLength(0))
                        {
                            nineIndexCol = actualCol;
                            break;
                        }
                    }
                    if (nineIndexCol != -1)
                    {
                        if (col == nineIndexCol - 1 || col == nineIndexCol + 1)
                        {
                            return false;
                        }
                    }
                }
            }

            if (!aroundCol.TrueForAll(n => n == 0))
            {
                if (aroundCol.Contains(number))
                {
                    if (!CanBePlacedInColNextToNine(aroundCol, grid, row, col, number))
                    {
                        return false;
                    }
                }
                else
                {
                    int nineIndexRow = -1;
                    for (int actualRow = 0; actualRow < Stores.SudokuStore.Instance.Sudoku.Grid.Size; actualRow++)
                    {
                        if (grid[actualRow, col] == grid.GetLength(0))
                        {
                            nineIndexRow = actualRow;
                            break;
                        }
                    }
                    if (nineIndexRow != -1)
                    {
                        if (row == nineIndexRow - 1 || row == nineIndexRow + 1)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private static bool CanBePlacedInRowNextToNine(List<int> numbersAround, int[,] grid, int row, int col, int number)
        {
            if (numbersAround.Count == 1)
            {
                if (col != 1 && col != Stores.SudokuStore.Instance.Sudoku.Grid.Size - 2)
                {
                    return false;
                }
                int left = grid[row, 0];
                int right = grid[row, Stores.SudokuStore.Instance.Sudoku.Grid.Size - 1];
                if (left != 0)
                {
                    if (col == 1 && left != grid.GetLength(0))
                    {
                        return false;
                    }
                }
                if (right != 0)
                {
                    if (col == Stores.SudokuStore.Instance.Sudoku.Grid.Size - 2 && right != grid.GetLength(0))
                    {
                        return false;
                    }
                }
            }
            else if (numbersAround.Count == 2)
            {
                int secondNumber = numbersAround[0];
                if (secondNumber == number)
                {
                    secondNumber = numbersAround[1];
                }
                int nineIndexCol = -1;
                for (int actualCol = 0; actualCol < Stores.SudokuStore.Instance.Sudoku.Grid.Size; actualCol++)
                {
                    if (grid[row, actualCol] == grid.GetLength(0))
                    {
                        nineIndexCol = actualCol;
                        break;
                    }
                }

                if (nineIndexCol != -1)
                {
                    if (nineIndexCol - 1 != col && nineIndexCol + 1 != col)
                    {
                        return false;
                    }

                    if (nineIndexCol - 1 == col && nineIndexCol + 1 < Stores.SudokuStore.Instance.Sudoku.Grid.Size)
                    {
                        int secondInGrid = grid[row, nineIndexCol + 1];
                        if (secondInGrid != 0)
                        {
                            if (secondInGrid != secondNumber)
                            {
                                return false;
                            }
                        }
                    }
                    else if (nineIndexCol + 1 == col && nineIndexCol - 1 >= 0)
                    {
                        int secondInGrid = grid[row, nineIndexCol - 1];
                        if (secondInGrid != 0)
                        {
                            if (secondInGrid != secondNumber)
                            {
                                return false;
                            }
                        }
                    }
                }

            }
            return true;
        }

        private static bool CanBePlacedInColNextToNine(List<int> numbersAround, int[,] grid, int row, int col, int number)
        {
            if (numbersAround.Count == 1)
            {
                if (row != 1 && row != Stores.SudokuStore.Instance.Sudoku.Grid.Size - 2)
                {
                    return false;
                }
                int left = grid[0, col];
                int right = grid[Stores.SudokuStore.Instance.Sudoku.Grid.Size - 1, col];
                if (left != 0)
                {
                    if (row == 1 && left != grid.GetLength(0))
                    {
                        return false;
                    }
                }
                if (right != 0)
                {
                    if (row == Stores.SudokuStore.Instance.Sudoku.Grid.Size - 2 && right != grid.GetLength(0))
                    {
                        return false;
                    }
                }
            }
            else if (numbersAround.Count == 2)
            {
                int secondNumber = numbersAround[0];
                if (secondNumber == number)
                {
                    secondNumber = numbersAround[1];
                }
                int nineIndexRow = -1;
                for (int actualRow = 0; actualRow < Stores.SudokuStore.Instance.Sudoku.Grid.Size; actualRow++)
                {
                    if (grid[actualRow, col] == grid.GetLength(0))
                    {
                        nineIndexRow = actualRow;
                        break;
                    }
                }

                if (nineIndexRow != -1)
                {
                    if (nineIndexRow - 1 != row && nineIndexRow + 1 != row)
                    {
                        return false;
                    }

                    if (nineIndexRow - 1 == row && nineIndexRow + 1 < Stores.SudokuStore.Instance.Sudoku.Grid.Size)
                    {
                        int secondInGrid = grid[nineIndexRow + 1, col];
                        if (secondInGrid != 0)
                        {
                            if (secondInGrid != secondNumber)
                            {
                                return false;
                            }
                        }
                    }
                    else if (nineIndexRow + 1 == row && nineIndexRow - 1 >= 0)
                    {
                        int secondInGrid = grid[nineIndexRow - 1, col];
                        if (secondInGrid != 0)
                        {
                            if (secondInGrid != secondNumber)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        private static bool IsNextToNineNinePlacedCorrect(int[,] grid, int row, int col)
        {
            return CanBePlaceInRowNextToNine(grid, row, col) && CanBePlaceInColNextToNine(grid, row, col);
        }

        private static bool CanBePlaceInRowNextToNine(int[,] grid, int row, int col)
        {
            List<int> aroundRow = GetNumberAroundNextToNineInRow(row);
            if (aroundRow.TrueForAll(n => n == 0))
            {
                return true;
            }


            List<int> numberNext = GetNumberAroundNextToNineInRow(row);

            if (numberNext.Count == 1)
            {
                if (col != 0 && col != Stores.SudokuStore.Instance.Sudoku.Grid.Size - 1)
                {
                    return false;
                }

                if (col == 0)
                {
                    if (grid[row, col + 1] != 0)
                    {
                        if (grid[row, col + 1] != numberNext[0])
                        {
                            return false;
                        }
                    }
                }
                else if (col == Stores.SudokuStore.Instance.Sudoku.Grid.Size - 1)
                {
                    if (grid[row, col - 1] != 0)
                    {
                        if (grid[row, col - 1] != numberNext[0])
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {
                if (col == 0 || col == Stores.SudokuStore.Instance.Sudoku.Grid.Size - 1)
                {
                    return false;
                }

                int left = grid[row, col - 1];
                int right = grid[row, col + 1];
                if (left != 0 && right != 0)
                {
                    if (!numberNext.Contains(left) || !numberNext.Contains(right))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static List<int> GetNumberAroundNextToNineInRow(int row)
        {
            List<int> numberNext = new List<int>();
            if (Stores.SudokuStore.Instance.Sudoku.LeftNumbers[row, 2] != 0 && Stores.SudokuStore.Instance.Sudoku.LeftNumbersType[row, 2] == SudokuElementType.NextToNine)
            {
                numberNext.Add(Stores.SudokuStore.Instance.Sudoku.LeftNumbers[row, 2]);
            }
            if (Stores.SudokuStore.Instance.Sudoku.LeftNumbers[row, 1] != 0 && Stores.SudokuStore.Instance.Sudoku.LeftNumbersType[row, 1] == SudokuElementType.NextToNine)
            {
                numberNext.Add(Stores.SudokuStore.Instance.Sudoku.LeftNumbers[row, 1]);
            }

            if (Stores.SudokuStore.Instance.Sudoku.RightNumbers[row, 0] != 0 && Stores.SudokuStore.Instance.Sudoku.RightNumbersType[row, 0] == SudokuElementType.NextToNine)
            {
                numberNext.Add(Stores.SudokuStore.Instance.Sudoku.RightNumbers[row, 0]);
            }
            if (Stores.SudokuStore.Instance.Sudoku.RightNumbers[row, 1] != 0 && Stores.SudokuStore.Instance.Sudoku.RightNumbersType[row, 1] == SudokuElementType.NextToNine)
            {
                numberNext.Add(Stores.SudokuStore.Instance.Sudoku.RightNumbers[row, 1]);
            }

            return numberNext;
        }

        private static bool CanBePlaceInColNextToNine(int[,] grid, int row, int col)
        {
            List<int> aroundCol = GetNumberAroundNextToNineInCol(col);
            if (aroundCol.TrueForAll(n => n == 0))
            {
                return true;
            }

            List<int> numberNext = GetNumberAroundNextToNineInCol(col);

            if (numberNext.Count == 1)
            {
                if (row != 0 && row != Stores.SudokuStore.Instance.Sudoku.Grid.Size - 1)
                {
                    return false;
                }

                if (row == 0)
                {
                    if (grid[row + 1, col] != 0)
                    {
                        if (grid[row + 1, col] != numberNext[0])
                        {
                            return false;
                        }
                    }
                }
                else if (row == Stores.SudokuStore.Instance.Sudoku.Grid.Size - 1)
                {
                    if (grid[row - 1, col] != 0)
                    {
                        if (grid[row - 1, col] != numberNext[0])
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {
                if (row == 0 || row == Stores.SudokuStore.Instance.Sudoku.Grid.Size - 1)
                {
                    return false;
                }

                int up = grid[row + 1, col];
                int down = grid[row - 1, col];
                if (down != 0 && up != 0)
                {
                    if (!numberNext.Contains(down) || !numberNext.Contains(up))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static List<int> GetNumberAroundNextToNineInCol(int col)
        {
            List<int> numberNext = new List<int>();
            if (Stores.SudokuStore.Instance.Sudoku.UpNumbers[2, col] != 0 && Stores.SudokuStore.Instance.Sudoku.UpNumbersType[2, col] == SudokuElementType.NextToNine)
            {
                numberNext.Add(Stores.SudokuStore.Instance.Sudoku.UpNumbers[2, col]);
            }
            if (Stores.SudokuStore.Instance.Sudoku.UpNumbers[1, col] != 0 && Stores.SudokuStore.Instance.Sudoku.UpNumbersType[1, col] == SudokuElementType.NextToNine)
            {
                numberNext.Add(Stores.SudokuStore.Instance.Sudoku.UpNumbers[1, col]);
            }

            if (Stores.SudokuStore.Instance.Sudoku.BottomNumbers[0, col] != 0 && Stores.SudokuStore.Instance.Sudoku.BottomNumbersType[0, col] == SudokuElementType.NextToNine)
            {
                numberNext.Add(Stores.SudokuStore.Instance.Sudoku.BottomNumbers[0, col]);
            }
            if (Stores.SudokuStore.Instance.Sudoku.BottomNumbers[1, col] != 0 && Stores.SudokuStore.Instance.Sudoku.BottomNumbersType[1, col] == SudokuElementType.NextToNine)
            {
                numberNext.Add(Stores.SudokuStore.Instance.Sudoku.BottomNumbers[1, col]);
            }

            return numberNext;
        }
    }
}
