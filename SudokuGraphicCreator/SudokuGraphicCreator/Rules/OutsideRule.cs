using SudokuGraphicCreator.Stores;
using System;
using System.Collections.Generic;

namespace SudokuGraphicCreator.Rules
{
    /// <summary>
    /// This class deside if number can be placed in given row and col by outside rules.
    /// </summary>
    public class OutsideRule
    {
        private static int XBox = 3;
        private static int YBox = 3;
        private static int Size = 9;
        
        /// <summary>
        /// Deside if <paramref name="number"/> can be placed in given <paramref name="row"/> and <paramref name="col"/> in <paramref name="grid"/>.
        /// </summary>
        /// <param name="grid">Grid of sudoku.</param>
        /// <param name="row">Row in which is <paramref name="number"/> placing.</param>
        /// <param name="col">Col in which is <paramref name="number"/> placing.</param>
        /// <param name="number">Value which is placing in <paramref name="grid"/>.</param>
        /// <returns>true if <paramref name="number"/> can be placed in <paramref name="grid"/> by outside rules.</returns>
        public static bool IsOutsideSafe(int[,] grid, int row, int col, int number)
        {
            XBox = SudokuStore.Instance.Sudoku.Grid.XBoxCells;
            YBox = SudokuStore.Instance.Sudoku.Grid.YBoxCells;
            Size = SudokuStore.Instance.Sudoku.Grid.Size;
            List<int> left = GetOutsideNumberInRow(SudokuStore.Instance.Sudoku.LeftNumbers, row);
            List<int> right = GetOutsideNumberInRow(SudokuStore.Instance.Sudoku.RightNumbers, row);
            List<int> up = GetOutsideNumbersInCol(SudokuStore.Instance.Sudoku.UpNumbers, col);
            List<int> down = GetOutsideNumbersInCol(SudokuStore.Instance.Sudoku.BottomNumbers, col);

            if (!IsInFirstThreeCells(row, col))
            {
                // placing number in central box
                return CanPlaceInCentralBox(number, left, right, up, down);
            }

            return CanBePlaceInAroundBoxes(grid, row, col, number, left, right, up, down);
        }

        private static bool CanBePlaceInAroundBoxes(int[,] grid, int row, int col, int number, List<int> left, List<int> right, List<int> up, List<int> down)
        {
            if (0 <= row && row <= YBox - 1 && !IsUpSafe(grid, row, col, number, up, left, right, down))
            {
                return false;
            }
            else if (0 <= col && col <= XBox - 1 && !IsLeftSafe(grid, row, col, number, left, up, down, right))
            {
                return false;
            }
            else if (2 * YBox <= row && row <= Size - 1 && !IsDownSafe(grid, row, col, number, down, left, right, up))
            {
                return false;
            }

            return !(2 * XBox <= col && col <= Size - 1 && !IsRightSafeOpt(grid, row, col, number, right, up, down, left));
        }

        private static List<int> GetOutsideNumbersInCol(int[,] collection, int col)
        {
            List<int> numbersOutside = new List<int>();
            for (int i = 0; i < XBox; i++)
            {
                if (collection[i, col] != 0)
                {
                    numbersOutside.Add(collection[i, col]);
                }
            }
            return numbersOutside;
        }

        private static List<int> GetOutsideNumberInRow(int[,] collection, int row)
        {
            List<int> numbersOutside = new List<int>();
            for (int i = 0; i < YBox; i++)
            {
                if (collection[row, i] != 0)
                {
                    numbersOutside.Add(collection[row, i]);
                }
            }
            return numbersOutside;
        }

        private static List<int> GetOutsideNumbersUp(int col)
        {
            List<int> numbersOutside = new List<int>();
            for (int i = 0; i < XBox; i++)
            {
                if (SudokuStore.Instance.Sudoku.UpNumbers[i, col] != 0)
                {
                    numbersOutside.Add(SudokuStore.Instance.Sudoku.UpNumbers[i, col]);
                }
            }
            return numbersOutside;
        }

        private static List<int> GetOutsideNumberLeft(int row)
        {
            List<int> numbersOutside = new List<int>();
            for (int i = 0; i < YBox; i++)
            {
                if (SudokuStore.Instance.Sudoku.LeftNumbers[row, i] != 0)
                {
                    numbersOutside.Add(SudokuStore.Instance.Sudoku.LeftNumbers[row, i]);
                }
            }
            return numbersOutside;
        }

        private static List<int> GetOutsideNumberRight(int row)
        {
            List<int> numbersOutside = new List<int>();
            for (int i = 0; i < YBox; i++)
            {
                if (SudokuStore.Instance.Sudoku.RightNumbers[row, i] != 0)
                {
                    numbersOutside.Add(SudokuStore.Instance.Sudoku.RightNumbers[row, i]);
                }
            }
            return numbersOutside;
        }

        private static List<int> GetOutsideNumberDown(int col)
        {
            List<int> numbersOutside = new List<int>();
            for (int i = 0; i < XBox; i++)
            {
                if (SudokuStore.Instance.Sudoku.BottomNumbers[i, col] != 0)
                {
                    numbersOutside.Add(SudokuStore.Instance.Sudoku.BottomNumbers[i, col]);
                }
            }
            return numbersOutside;
        }

        private static bool CanPlaceInCentralBox(int number, List<int> left, List<int> right,
            List<int> up, List<int> down)
        {
            // rule: if placing number is around the grid in correcponding row/col, cant be placed
            return !left.Contains(number) && !right.Contains(number) && !up.Contains(number) && !down.Contains(number);
        }

        private static bool IsOneFromThreeAround(List<int> arounds, int number)
        {
            // rule: if there are around three numbers, number have to one of them
            if (arounds.Count == 3)
            {
                return arounds.Contains(number);
            }
            return true;
        }

        private static bool CanBeEliminateByAroundNumbers(int index, List<int> opposite, List<int> upper,
            List<int> lower, int number)
        {
            // rule: cant place number in row/col if in correponding row/col must be place same number
            return CanBeEliminateInLowerIndexBox(index, upper, opposite, number) ||
                CanBeEliminateInMiddleIndexBox(index, upper, lower, opposite, number) ||
                CanBeEliminateInUpperIndexBox(index, lower, opposite, number);
        }

        private static bool CanBeEliminateInLowerIndexBox(int index, List<int> upper, List<int> opposite, int number)
        {
            return 0 <= index && index <= XBox - 1 && (upper.Contains(number) || opposite.Contains(number));
        }

        private static bool CanBeEliminateInMiddleIndexBox(int index, List<int> upper, List<int> lower, List<int> opposite , int number)
        {
            return XBox - 1 < index && index < 2 * XBox && (upper.Contains(number) || lower.Contains(number) || opposite.Contains(number));
        }

        private static bool CanBeEliminateInUpperIndexBox(int index, List<int> lower, List<int> opposite, int number)
        {
            return 2 * XBox <= index && index <= Size - 1 && (lower.Contains(number) || opposite.Contains(number));
        }

        private static bool CanEliminateInBoxUpBoxes(int row, int col, int number)
        {
            // rule: if there is same number around box, cant be this number place in another row/col
            if (0 <= col && col <= XBox - 1)
            {
                // left up box
                if (CanEliminateInBoxByOneSide(GetOutsideNumberLeft, row, 0, YBox, number) ||
                    CanEliminateInBoxByOneSide(GetOutsideNumbersUp, col, 0, XBox, number))
                {
                    return true;
                }
            }
            else if (XBox - 1 < col && col < 2 * XBox)
            {
                // up middle box
                if (CanEliminateInBoxByOneSide(GetOutsideNumbersUp, col, XBox, 2 * XBox, number))
                {
                    return true;
                }
            }
            else
            {
                // right up box
                if (CanEliminateInBoxByOneSide(GetOutsideNumberRight, row, 0, YBox, number) ||
                    CanEliminateInBoxByOneSide(GetOutsideNumbersUp, col, 2 * XBox, Size, number))
                {
                    return true;
                }
            }
            return false;

        }

        private static bool CanEliminateInBoxByOneSide(Func<int, List<int>> getAround, int index, int start,
            int end, int number)
        {
            for (int i = start; i < end; i++)
            {
                if (i != index && getAround(i).Contains(number))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool CanPlaceOnOnePlaceInBox(int index, List<int> around, List<int> lower, List<int> upper,
            int number)
        {
            // rule: in corner box can be by numbers around one correct place for this number
            return CanBePlaceInLowerBox(index, around, lower, number) ||
                CanBePlaceInUpperBox(index, around, upper, number);
        }

        private static bool CanBePlaceInLowerBox(int index, List<int> around, List<int> lower, int number)
        {
            return 0 <= index && index <= XBox && lower.Contains(number) && around.Contains(number);
        }

        private static bool CanBePlaceInUpperBox(int index, List<int> around, List<int> upper, int number)
        {
            return 2 * XBox <= index && index <= Size - 1 && upper.Contains(number) && around.Contains(number);
        }

        private static bool CanBePlacedThisDirectionRow(int[,] grid, int index, int number, List<int> around, int start, int end)
        {
            List<int> numberInGrid = new List<int>();
            int missing = 0;
            for (int i = start; i < end; i++)
            {
                int actualNumber = grid[i, index];
                if (actualNumber == 0)
                {
                    actualNumber = number;
                    missing++;
                }
                numberInGrid.Add(actualNumber);
            }
            if (missing != 1)
            {
                return true;
            }
            return ContainsOutsideNumbers(numberInGrid, around);
        }

        private static bool CanBePlacedThisDirectionCol(int[,] grid, int index, int number, List<int> around, int start, int end)
        {
            List<int> numberInGrid = new List<int>();
            int missing = 0;
            for (int i = start; i < end; i++)
            {
                int actualNumber = grid[index, i];
                if (actualNumber == 0)
                {
                    actualNumber = number;
                    missing++;
                }
                numberInGrid.Add(actualNumber);
            }
            if (missing != 1)
            {
                return true;
            }
            return ContainsOutsideNumbers(numberInGrid, around);
        }

        private static bool IsUpSafe(int[,] grid, int row, int col, int number, List<int> up, List<int> left,
            List<int> right, List<int> down)
        {
            if (!IsOneFromThreeAround(up, number) || CanEliminateInBoxUpBoxes(row, col, number) ||
               CanBeEliminateByAroundNumbers(col, down, right, left, number))
            {
                return false;
            }

            if (CanPlaceOnOnePlaceInBox(col, up, left, right, number))
            {
                return true;
            }

            return CanBePlacedThisDirectionRow(grid, col, number, up, 0, XBox);
        }

        private static bool CanEliminateInBoxLeftBoxes(int row, int col, int number)
        {
            if (0 <= row && row <= YBox - 1)
            {
                if (CanEliminateInBoxByOneSide(GetOutsideNumberLeft, row, 0, YBox, number) ||
                    CanEliminateInBoxByOneSide(GetOutsideNumbersUp, col, 0, XBox, number))
                {
                    return true;
                }
            }
            else if (YBox - 1 < row && row < 2 * YBox)
            {
                // left middle box
                if (CanEliminateInBoxByOneSide(GetOutsideNumberLeft, row, YBox, 2 * YBox, number))
                {
                    return true;
                }
            }
            else
            {
                // left down box
                if (CanEliminateInBoxByOneSide(GetOutsideNumberLeft, row, 2 * YBox, Size, number) ||
                    CanEliminateInBoxByOneSide(GetOutsideNumberDown, col, 0, XBox, number))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool IsLeftSafe(int[,] grid, int row, int col, int number, List<int> left, List<int> up,
            List<int> down, List<int> right)
        {
            if (!IsOneFromThreeAround(left, number)|| CanEliminateInBoxLeftBoxes(row, col, number) ||
                CanBeEliminateByAroundNumbers(row, right, down, up, number))
            {
                return false;
            }

            if (CanPlaceOnOnePlaceInBox(row, left, up, down, number))
            {
                return true;
            }

            return CanBePlacedThisDirectionCol(grid, col, number, left, 0, XBox);
        }

        private static bool CanEliminateInBoxDownBox(int row, int col, int number)
        {
            // number can not be placed in another row/col if is outside
            if (0 <= col && col <= XBox - 1)
            {
                // down left box
                if (CanEliminateInBoxByOneSide(GetOutsideNumberLeft, row, 2 * YBox, Size, number) ||
                    CanEliminateInBoxByOneSide(GetOutsideNumberDown, col, 0, XBox, number))
                {
                    return true;
                }
            }
            else if (XBox - 1 < col && col < 2 * XBox)
            {
                // middle down box
                if (CanEliminateInBoxByOneSide(GetOutsideNumberDown, col, XBox, 2 * XBox, number))
                {
                    return true;
                }
            }
            else if (2 * XBox <= col && col <= Size - 1)
            {
                // down right
                if (CanEliminateInBoxByOneSide(GetOutsideNumberRight, row, 2 * XBox, Size, number) ||
                    CanEliminateInBoxByOneSide(GetOutsideNumberDown, col, 2 * YBox, Size, number))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool IsDownSafe(int[,] grid, int row, int col, int number, List<int> down, List<int> left,
            List<int> right, List<int> up)
        {
            if (!IsOneFromThreeAround(down, number) || CanEliminateInBoxDownBox(row, col, number) ||
                CanBeEliminateByAroundNumbers(col, up, right, left, number))
            {
                return false;
            }

            if (CanPlaceOnOnePlaceInBox(col, down, left, right, number))
            {
                return true;
            }

            return CanBePlacedThisDirectionRow(grid, col, number, down, SudokuStore.Instance.Sudoku.Grid.Size - 3,
                SudokuStore.Instance.Sudoku.Grid.Size);
        }

        private static bool CanEliminateInBoxRightBox(int row, int col, int number)
        {
            // number can not be placed in another row/col if is outside
            if (0 <= row && row <= XBox - 1)
            {
                // right up box
                if (CanEliminateInBoxByOneSide(GetOutsideNumberRight, row, 0, YBox, number) ||
                    CanEliminateInBoxByOneSide(GetOutsideNumbersUp, col, XBox * 2, Size, number))
                {
                    return true;
                }
            }
            else if (XBox - 1 < row && row < 2 * XBox)
            {
                // middle right box
                if (CanEliminateInBoxByOneSide(GetOutsideNumberRight, row, YBox, 2 * YBox, number))
                {
                    return true;
                }
            }
            else if (2 * XBox <= row && row <= Size - 1)
            {
                // right down
                if (CanEliminateInBoxByOneSide(GetOutsideNumberRight, row, YBox * 2, Size, number) ||
                    CanEliminateInBoxByOneSide(GetOutsideNumberDown, col, 2 * XBox, Size, number))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool IsRightSafeOpt(int[,] grid, int row, int col, int number, List<int> right, List<int> up,
            List<int> down, List<int> left)
        {
            if (!IsOneFromThreeAround(right, number) || CanEliminateInBoxRightBox(row, col, number) ||
                CanBeEliminateByAroundNumbers(row, left, down, up, number))
            {
                return false;
            }

            if (CanPlaceOnOnePlaceInBox(row, right, up, down, number))
            {
                return true;
            }

            return CanBePlacedThisDirectionCol(grid, col, number, right, SudokuStore.Instance.Sudoku.Grid.Size - 3,
                SudokuStore.Instance.Sudoku.Grid.Size);
        }

        private static bool IsInFirstThreeCells(int row, int col)
        {
            return (0 <= row && row <= YBox - 1) || (0 <= col && col <= XBox - 1) || (2 * YBox <= row && row <= Size - 1) ||
                (2 * XBox <= col && col <= Size - 1);
        }

        private static bool ContainsOutsideNumbers(List<int> numbersInGrid, List<int> numbersOutside)
        {
            foreach (var outside in numbersOutside)
            {
                if (!numbersInGrid.Contains(outside))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
